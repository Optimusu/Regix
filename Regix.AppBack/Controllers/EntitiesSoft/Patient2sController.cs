using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Regix.AppBack.Helper;
using Regix.AppInfra.ErrorHandling;
using Regix.Domain.EntitiesSoft;
using Regix.DomainLogic.Pagination;
using Regix.DomainLogic.ResponsesSec;
using Regix.UnitOfWork.InterfaceSoft;

namespace Regix.AppBack.Controllers.EntitiesSoft
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/regpatient2s")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Patient, Administrator")]
    [ApiController]
    public class Patient2sController : ControllerBase
    {
        private readonly IPatient2UnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public Patient2sController(IPatient2UnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            try
            {
                ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
                var response = await _unitOfWork.GetAsync(pagination, userClaimsInfo.UserName);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var response = await _unitOfWork.GetAsync(id);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Patient2 modelo)
        {
            try
            {
                var response = await _unitOfWork.UpdateAsync(modelo);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Patient2 modelo)
        {
            try
            {
                ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
                var response = await _unitOfWork.AddAsync(modelo, userClaimsInfo.UserName);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _unitOfWork.DeleteAsync(id);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }
    }
}