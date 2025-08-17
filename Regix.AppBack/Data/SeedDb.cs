using Microsoft.EntityFrameworkCore;
using Regix.AppBack.LoadCountries;
using Regix.AppInfra;
using Regix.AppInfra.UserHelper;
using Regix.Domain.Entities;
using Regix.Domain.EntitiesGen;
using Regix.Domain.Enum;
using Regix.DomainLogic.TrialResponse;

namespace Regix.AppBack.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IApiService _apiService;
    private readonly IUserHelper _userHelper;

    public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
    {
        _context = context;
        _apiService = apiService;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckRolesAsync();
        await CheckSoftPlan();
        await CheckCountries();
        await CheckCorporationAsync();
        await CheckDocumentType();
        await CheckEstadoCivil();
        await CheckIdentidadGenero();
        await CheckSexoAsignado();
        await CheckPharmacy();
        await CheckLenguage();
        await CheckAnticonception();
        await CheckRegularPeriod();
        await CheckUserAsync("Optimus U", "TrialPro", "hebalmert", "optimusu.soft@gmail.com", "+1 786 503", UserType.Admin);
    }

    private async Task CheckCorporationAsync()
    {
        if (!_context.Corporations.Any())
        {
            Corporation corporation = new()
            {
                Name = "Optimus U",
                TypeDocument = "ITIN",
                NroDocument = "3445645645",
                Phone = "786",
                Address = "Street 45",
                CountryId = 1,
                SoftPlanId = 3,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now.AddYears(10),
                Active = true
            };
            _context.Corporations.Add(corporation);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<User> CheckUserAsync(string firstName, string lastName, string username, string email,
    string phone, UserType userType)
    {
        User user = await _userHelper.GetUserByUserNameAsync(username);
        if (user == null)
        {
            user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = $"{firstName} {lastName}",
                Email = email,
                UserName = username,
                PhoneNumber = phone,
                JobPosition = "Administrador",
                UserFrom = "SeedDb",
                UserRoleDetails = new List<UserRoleDetails> { new UserRoleDetails { UserType = userType } },
                Active = true,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            //Para Confirmar automaticamente el Usuario y activar la cuenta
            string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
            await _userHelper.AddUserClaims(userType, username);
        }
        return user;
    }

    private async Task CheckRegularPeriod()
    {
        if (!_context.Regulars.Any())
        {
            _context.Regulars.Add(new Regular { Name = "Regular Cycle", Active = true });
            _context.Regulars.Add(new Regular { Name = "Irregular Cycle", Active = true });

            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        await _userHelper.CheckRoleAsync(UserType.Administrator.ToString());
        await _userHelper.CheckRoleAsync(UserType.Coordinator.ToString());
        await _userHelper.CheckRoleAsync(UserType.Patient.ToString());
    }

    private async Task CheckPharmacy()
    {
        if (!_context.Pharmacies.Any())
        {
            _context.Pharmacies.Add(new Pharmacy { Name = "Express Scripts", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "CVS Pharmacy", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Walgreens", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Health Mart", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Good Neighbor Pharmacy", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Genoa Healthcare", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "The Medicine Shoppe Pharmacy", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Ascension Health Pharmacy", Active = true });
            _context.Pharmacies.Add(new Pharmacy { Name = "Medicap Pharmacy", Active = true });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckLenguage()
    {
        if (!_context.Idiomas.Any())
        {
            _context.Idiomas.Add(new Idioma { Name = "English", Active = true });
            _context.Idiomas.Add(new Idioma { Name = "Spanish", Active = true });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckAnticonception()
    {
        if (!_context.Anticonceptions.Any())
        {
            _context.Anticonceptions.AddRange(new[]
            {
                new Anticonception { Name = "Oral contraceptive pills", Active = true },
                new Anticonception { Name = "Contraceptive patch", Active = true },
                new Anticonception { Name = "Vaginal ring", Active = true },
                new Anticonception { Name = "Injectable contraceptive", Active = true },
                new Anticonception { Name = "Implant", Active = true },
                new Anticonception { Name = "Hormonal IUD", Active = true },
                new Anticonception { Name = "Copper IUD", Active = true },
                new Anticonception { Name = "Male condom", Active = true },
                new Anticonception { Name = "Female condom", Active = true },
                new Anticonception { Name = "Diaphragm", Active = true },
                new Anticonception { Name = "Cervical cap", Active = true },
                new Anticonception { Name = "Spermicide", Active = true },
                new Anticonception { Name = "Male sterilization (Vasectomy)", Active = true },
                new Anticonception { Name = "Female sterilization (Tubal ligation)", Active = true },
                new Anticonception { Name = "Fertility awareness methods", Active = true },
                new Anticonception { Name = "Withdrawal method", Active = true },
                new Anticonception { Name = "No contraception", Active = true }
            });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckDocumentType()
    {
        if (!_context.DocumentTypes.Any())
        {
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "Passport", Active = true });
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "Driver’s License", Active = true });
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "State ID Card", Active = true });
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "Social Security Card", Active = true });
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "Green Card", Active = true });
            _context.DocumentTypes.Add(new DocumentType { DocumentName = "Work Permit (EAD)", Active = true });

            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckEstadoCivil()
    {
        if (!_context.EstadoCivils.Any())
        {
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Single", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Married", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Divorced", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Widowed", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Separated", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Cohabiting", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Domestic Partnership", Active = true });
            _context.EstadoCivils.Add(new EstadoCivil { Name = "Prefer not to say", Active = true });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckIdentidadGenero()
    {
        if (!_context.IdentidadGeneros.Any())
        {
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Man", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Woman", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Non-binary", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Transgender", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Cisgender", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Gender fluid", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Agender", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Bigender", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Other", Active = true });
            _context.IdentidadGeneros.Add(new IdentidadGenero { Name = "Prefer not to say", Active = true });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckSexoAsignado()
    {
        if (!_context.SexoAsignados.Any())
        {
            _context.SexoAsignados.Add(new SexoAsignado { Name = "Male", Active = true });
            _context.SexoAsignados.Add(new SexoAsignado { Name = "Female", Active = true });
            _context.SexoAsignados.Add(new SexoAsignado { Name = "Intersex", Active = true });
            _context.SexoAsignados.Add(new SexoAsignado { Name = "Prefer not to say", Active = true });

            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckSoftPlan()
    {
        if (!_context.DocumentTypes.Any())
        {
            //Alimentando Planes
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 1 Mes",
                Price = 50,
                Meses = 1,
                TrialsCount = 2,
                Active = true
            });
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 6 Mes",
                Price = 300,
                Meses = 6,
                TrialsCount = 10,
                Active = true
            });
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 12 Mes",
                Price = 600,
                Meses = 12,
                TrialsCount = 100,
                Active = true
            });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCountries()
    {
        Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
        if (responseCountries.IsSuccess)
        {
            List<CountryResponse> NlistCountry = (List<CountryResponse>)responseCountries.Result!;
            List<CountryResponse> countries = NlistCountry.Where(x => x.Name == "United States").ToList();

            foreach (CountryResponse item in countries)
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == item.Name);
                if (country == null)
                {
                    country = new() { Name = item.Name!, States = new List<State>() };
                    Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{item.Iso2}/states");
                    if (responseStates.IsSuccess)
                    {
                        List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                        foreach (StateResponse stateResponse in states!)
                        {
                            State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                            if (state == null)
                            {
                                state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{item.Iso2}/states/{stateResponse.Iso2}/cities");
                                if (responseCities.IsSuccess)
                                {
                                    List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                    foreach (CityResponse cityResponse in cities)
                                    {
                                        if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                        {
                                            continue;
                                        }
                                        City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                        if (city == null)
                                        {
                                            state.Cities.Add(new City() { Name = cityResponse.Name! });
                                        }
                                    }
                                }
                                if (state.CitiesNumber > 0)
                                {
                                    country.States.Add(state);
                                }
                            }
                        }
                    }
                    if (country.StatesNumber > 0)
                    {
                        _context.Countries.Add(country);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}