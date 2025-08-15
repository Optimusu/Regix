using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Regix.AppInfra;
using Regix.AppInfra.EmailHelper;
using Regix.AppInfra.FileHelper;
using Regix.AppInfra.Transactions;
using Regix.AppInfra.UserHelper;
using Regix.Domain.Entities;
using Regix.Domain.EntitiesSoft;
using Regix.Domain.Enum;
using Regix.DomainLogic.ResponsesSec;
using Regix.DomainLogic.TrialResponse;
using Regix.Services.InterfacesSecure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Regix.Services.ImplementSecure;

public class AccountService : IAccountService
{
    private readonly DataContext _context;
    private readonly IUserHelper _userHelper;
    private readonly IEmailHelper _emailHelper;
    private readonly ITransactionManager _transactionManager;
    private readonly IStringLocalizer _localizer;
    private readonly IFileStorage _fileStorage;
    private readonly JwtKeySetting _jwtOption;
    private readonly ImgSetting _imgOption;

    public AccountService(DataContext context, IUserHelper userHelper,
        IEmailHelper emailHelper, IOptions<ImgSetting> ImgOption, ITransactionManager transactionManager,
        IOptions<JwtKeySetting> jwtOption, IStringLocalizer localizer, IFileStorage fileStorage)
    {
        _context = context;
        _userHelper = userHelper;
        _emailHelper = emailHelper;
        _transactionManager = transactionManager;
        _localizer = localizer;
        _fileStorage = fileStorage;
        _jwtOption = jwtOption.Value;
        _imgOption = ImgOption.Value;
    }

    public async Task<ActionResponse<TokenDTO>> LoginAsync(LoginDTO modelo)
    {
        string? imgUsuario = string.Empty;
        string? ImagenDefault = _imgOption.ImgNoImage;
        string BaseUrl = _imgOption.ImgBaseUrl;

        var result = await _userHelper.LoginAsync(modelo);
        if (result.Succeeded)
        {
            //Consulto User de IdentityUser
            var user = await _userHelper.GetUserByUserNameAsync(modelo.UserName);
            if (!user.Active)
            {
                return new ActionResponse<TokenDTO>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_UserInactive"]
                };
            }
            var RolesUsuario = _context.UserRoleDetails.Where(c => c.UserId == user.Id).ToList();
            if (RolesUsuario.Count == 0)
            {
                return new ActionResponse<TokenDTO>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_UserNoRoleAssigned"]
                };
            }
            var RolUsuario = RolesUsuario.FirstOrDefault(c => c.UserType == UserType.Admin);
            if (RolUsuario == null)
            {
                var CheckCorporation = await _context.Corporations.FirstOrDefaultAsync(x => x.CorporationId == user.CorporationId);
                DateTime hoy = DateTime.Today;
                DateTime current = CheckCorporation!.DateEnd;
                if (!CheckCorporation.Active)
                {
                    return new ActionResponse<TokenDTO>
                    {
                        WasSuccess = false,
                        Message = _localizer["Generic_CorporationInactive"]
                    };
                }
                if (current <= hoy)
                {
                    return new ActionResponse<TokenDTO>
                    {
                        WasSuccess = false,
                        Message = _localizer["Generic_PlanExpired"]
                    };
                }

                switch (user.UserFrom)
                {
                    case "Manager":
                        if (!string.IsNullOrWhiteSpace(user.PhotoUser))
                        {
                            var FileResult = await _fileStorage.GetFileBase64Async(user.PhotoUser, _imgOption.ImgManager);
                            imgUsuario = FileResult!.Base64;
                        }
                        else
                        {
                            imgUsuario = ImagenDefault;
                        }
                        //imgUsuario = !string.IsNullOrWhiteSpace(user.PhotoUser)
                        //    ? await _fileStorage.GetFileBase64Async(user.PhotoUser, _imgOption.ImgManager)
                        //    : ImagenDefault;
                        break;

                    case "UsuarioSoftware":
                        if (!string.IsNullOrWhiteSpace(user.PhotoUser))
                        {
                            var FileResult = await _fileStorage.GetFileBase64Async(user.PhotoUser, _imgOption.ImgUsuario);
                            imgUsuario = FileResult!.Base64;
                        }
                        else
                        {
                            imgUsuario = ImagenDefault;
                        }
                        break;

                    case "Patient":
                        imgUsuario = ImagenDefault;
                        break;
                }
            }
            return new ActionResponse<TokenDTO>
            {
                WasSuccess = true,
                Result = await BuildToken(user, imgUsuario!)
            };
        }

        if (result.IsLockedOut)
        {
            return new ActionResponse<TokenDTO>
            {
                WasSuccess = false,
                Message = _localizer["Generic_UserBlocked"]
            };
        }

        if (result.IsNotAllowed)
        {
            return new ActionResponse<TokenDTO>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AccessDenied"]
            };
        }

        return new ActionResponse<TokenDTO>
        {
            WasSuccess = false,
            Message = _localizer["Generic_InvalidCredentials"]
        };
    }

    public async Task<ActionResponse<TokenDTO>> RegisterAsync(RegisterDTO registerDTO)
    {
        UserType usertype = UserType.Patient;
        //Verificamos si existe el UserName
        User Newuser = await _userHelper.GetUserByUserNameAsync(registerDTO.UserName);
        if (Newuser != null)
        {
            return new ActionResponse<TokenDTO>
            {
                WasSuccess = false,
                Message = _localizer["Generic_UserNameAlreadyUsed"]
            };
        }
        Newuser = new()
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            FullName = $"{registerDTO.FirstName} {registerDTO.LastName}",
            UserName = registerDTO.UserName,
            JobPosition = "Patient",
            UserFrom = "Register",
            CorporationId = registerDTO.CorporationId,
            UserRoleDetails = new List<UserRoleDetails> { new UserRoleDetails { UserType = usertype } },
            Active = true,
        };

        await _transactionManager.BeginTransactionAsync();

        await _userHelper.AddUserAsync(Newuser, registerDTO.NewPassword);
        await _userHelper.AddUserToRoleAsync(Newuser, usertype.ToString());
        await _userHelper.AddUserClaims(usertype, registerDTO.UserName);

        //Crear PatientControl
        PatientControl patientControl = new()
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            DOB = registerDTO.DOB,
            UserName = registerDTO.UserName,
            CorporationId = registerDTO.CorporationId,
        };
        _context.PatientControls.Add(patientControl);

        LoginDTO modelo = new() { UserName = registerDTO.UserName, Password = registerDTO.NewPassword };

        var loginResult = await LoginAsync(modelo);
        if (loginResult.WasSuccess)
        {
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();
        }
        else
        {
            await _transactionManager.RollbackTransactionAsync();
        }
        return loginResult;
    }

    public async Task<ActionResponse<bool>> RecoverPasswordAsync(EmailDTO modelo, string frontUrl)
    {
        var user = await _userHelper.GetUserByUserNameAsync(modelo.UserName);
        if (user == null)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = _localizer["Generic_IdNotFound"]
            };
        }

        Response response = await SendRecoverEmailAsync(user, frontUrl);
        if (response.IsSuccess)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = true,
                Message = response.Message
            };
        }

        return new ActionResponse<bool>
        {
            WasSuccess = false,
            Message = response.Message
        };
    }

    public async Task<ActionResponse<bool>> ResetPasswordAsync(ResetPasswordDTO modelo)
    {
        var user = await _userHelper.GetUserByUserNameAsync(modelo.UserName);
        if (user == null)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = _localizer["Generic_UserFail"]
            };
        }

        var result = await _userHelper.ResetPasswordAsync(user, modelo.Token, modelo.NewPassword);
        if (result.Succeeded)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = true,
                Message = _localizer["Generic_Success"]
            };
        }
        return new ActionResponse<bool>
        {
            WasSuccess = false,
            Message = result.Errors.FirstOrDefault()!.Description
        };
    }

    public async Task<ActionResponse<bool>> ChangePasswordAsync(ChangePasswordDTO modelo, string UserName)
    {
        var user = await _userHelper.GetUserByUserNameAsync(UserName);
        if (user == null)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = _localizer["Generic_UserFail"]
            };
        }

        var result = await _userHelper.ChangePasswordAsync(user, modelo.CurrentPassword, modelo.NewPassword);
        if (!result.Succeeded)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = result.Errors.FirstOrDefault()!.Description
            };
        }

        return new ActionResponse<bool>
        {
            WasSuccess = true,
            Message = _localizer["Generic_Success"]
        };
    }

    public async Task<ActionResponse<bool>> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userHelper.GetUserByIdAsync(new Guid(userId));
        if (user == null)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = _localizer["Generic_UserFail"]
            };
        }

        var result = await _userHelper.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return new ActionResponse<bool>
            {
                WasSuccess = false,
                Message = result.Errors.FirstOrDefault()!.Description
            };
        }

        return new ActionResponse<bool>
        {
            WasSuccess = true,
            Message = _localizer["Generic_Success"]
        };
    }

    private async Task<Response> SendRecoverEmailAsync(User user, string frontUrl)
    {
        var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

        // Construir la URL sin `Url.Action`
        string tokenLink = $"{frontUrl}/api/accounts/ResetPassword?userid={user.Id}&token={myToken}";

        string subject = "Recuperacion de Clave";
        string body = ($"De: NexxtPlanet" +
            $"<h1>Para Recuperar su Clave</h1>" +
            $"<p>" +
            $"Para Crear una clave nueva " +
            $"Has Click en el siguiente Link:</br></br><strong><a href = \"{tokenLink}\">Cambiar Clave</a></strong>");

        Response response = await _emailHelper.ConfirmarCuenta(user.UserName!, user.FullName!, subject, body);
        if (response.IsSuccess == false)
        {
            return response;
        }
        return response;
    }

    private async Task<TokenDTO> BuildToken(User user, string? imgUsuario)
    {
        string NomCompa;
        string? LogoCompa;
        var RolesUsuario = _context.UserRoleDetails.Where(c => c.UserId == user.Id).ToList();
        var RolUsuario = RolesUsuario.Where(c => c.UserType == UserType.Admin).FirstOrDefault();
        if (RolUsuario != null)
        {
            //TODO: Cambio de Path para Imagenes
            NomCompa = "Optimus U";
            LogoCompa = _imgOption.LogoSoftware;
            imgUsuario = _imgOption.LogoSoftware;
        }
        else
        {
            var compname = _context.Corporations.FirstOrDefault(x => x.CorporationId == user.CorporationId);
            NomCompa = compname!.Name!;
            if (!string.IsNullOrWhiteSpace(compname.Imagen))
            {
                var FileResult = await _fileStorage.GetFileBase64Async(compname.Imagen, _imgOption.ImgCorporation);
                LogoCompa = FileResult!.Base64;
            }
            else
            {
                LogoCompa = _imgOption.LogoSoftware;
            }
            //LogoCompa = !string.IsNullOrWhiteSpace(compname.ImageFullPath)
            //                ? await _fileStorage.GetImageBase64Async(compname.ImageFullPath, _imgOption.ImgCorporation)
            //                : _imgOption.LogoSoftware;
        }
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("CorpName", NomCompa)
            };
        // Solo agregar el CorporateId si el usuario NO es Admin
        if (RolUsuario == null && user.CorporationId.HasValue)
        {
            claims.Add(new Claim("CorporateId", user.CorporationId.Value.ToString()));
        }

        // Agregar los roles del usuario a los claims
        foreach (var item in RolesUsuario)
        {
            claims.Add(new Claim(ClaimTypes.Role, item.UserType.ToString()!));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.jwtKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddDays(30);
        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            PhotoBase64 = imgUsuario,
            LogoBase64 = LogoCompa
        };
    }
}