using Regix.DomainLogic.TrialResponse;

namespace Regix.AppBack.LoadCountries;

public interface IApiService
{
    Task<Response> GetListAsync<T>(string servicePrefix, string controller);
}