using Location.API.Common;
using Location.API.Entities;

namespace Location.API.Services;

public interface IPharmacyService
{
    Task<OperationResult<List<Pharmacy>>> GetPharmaciesAsync();

    Task<OperationResult<Pharmacy>> AddPharmacyAsync(Pharmacy pharmacy);
    Task<OperationResult<List<Pharmacy>>> GetPharmacyOpenBetweenIntervalAsync(TimeSpan start = default, TimeSpan end = default);
}