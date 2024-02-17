using Location.API.Common;
using Location.API.Entities;
using Location.API.Repositories;

namespace Location.API.Services;

public class PharmacyService : IPharmacyService
{
    private readonly Serilog.ILogger _logger;
    private readonly IPharmacyRepository _pharmacyRepository;

    public PharmacyService(Serilog.ILogger logger, IPharmacyRepository pharmacyRepository)
    {
        _logger = logger;
        _pharmacyRepository = pharmacyRepository;
    }

    public async Task<OperationResult<Pharmacy>> AddPharmacyAsync(Pharmacy pharmacy)
    {
        var result = new OperationResult<Pharmacy>();
        try
        {
            var response = await _pharmacyRepository.AddPharmacyAsync(pharmacy);

            if (response == null)
            {
                result.AddError($"Cannot add Pharmacy");
                return result;
            }

            result.Payload = response;
        }
        catch (Exception ex)
        {
            result.AddError($"Exception while adding the Pharmacy, message = {ex.Message}");
            _logger.Error($"Exception while adding the Pharmacy, message = {ex.Message}", ex);
        }

        return result;
    }

    public async Task<OperationResult<List<Pharmacy>>> GetPharmaciesAsync()
    {
        var result = new OperationResult<List<Pharmacy>>();
        try
        {
            var response = await _pharmacyRepository.GetPharmaciesAsync();

            if (response == null)
            {
                result.AddError($"Cannot Get List Of Pharmacies");
                return result;
            }

            result.Payload = response;
        }
        catch (Exception ex)
        {
            result.AddError($"Exception while fetching the Pharmacies, message = {ex.Message}");
            _logger.Error($"Exception while fetching the Pharmacies, message = {ex.Message}", ex);
        }

        return result;
    }

    public async Task<OperationResult<List<Pharmacy>>> GetPharmacyOpenBetweenIntervalAsync(TimeSpan start = default, TimeSpan end = default)
    {
        var result = new OperationResult<List<Pharmacy>>();
        try
        {
            if (start == default) start = TimeSpan.FromHours(10);
            if (end == default) end = TimeSpan.FromHours(13);
            var response = await _pharmacyRepository.GetPharmaciesAsync(start, end);

            if (response == null)
            {
                result.AddError($"Cannot Get List Of Pharmacies");
                return result;
            }

            result.Payload = response;
        }
        catch (Exception ex)
        {
            result.AddError($"Exception while fetching the Pharmacies, message = {ex.Message}");
            _logger.Error($"Exception while fetching the Pharmacies, message = {ex.Message}", ex);
        }

        return result;
    }
}
