using Location.API.Entities;
using System.Text.Json;

namespace Location.API.Repositories;

public sealed class PharmacyRepository : IPharmacyRepository
{
    private readonly string _dbFilePath;
    private readonly Serilog.ILogger _logger;

    public PharmacyRepository(Serilog.ILogger logger)
    {
        _logger = logger;
        _dbFilePath = @"./AppData/DB.json";
    }

    public async Task<Pharmacy?> AddPharmacyAsync(Pharmacy pharmacy)
    {
        try
        {
            var pharmacies = await GetPharmaciesAsync();
            if (pharmacies != null)
            {
                var exists = pharmacies.Any(p => p.Name.Equals(pharmacy.Name, StringComparison.OrdinalIgnoreCase));
                if (!exists)
                {
                    var maxId = pharmacies.Count() > 0 ? pharmacies.Max(p => p.Id) : 0;
                    pharmacy.Id = maxId + 1;
                    pharmacy.OpenningTime = TimeSpan.Parse(pharmacy.OpenningTime).ToString();
                    pharmacy.ClosingTime = TimeSpan.Parse(pharmacy.ClosingTime).ToString();
                    pharmacies.Add(pharmacy);
                    await WritePharmaciesToFileAsync(pharmacies);
                }
            }

            return pharmacy;
        }
        catch (Exception ex)
        {
            _logger.Error($"{nameof(GetPharmaciesAsync)}, Exception while getting the list of Pharmacies, message = {ex.Message}", ex);
        }

        return null;
    }

    public async Task<List<Pharmacy>?> GetPharmaciesAsync()
    {
        var listOfPharmacies = new List<Pharmacy>();
        try
        {
            using (StreamReader file = File.OpenText(_dbFilePath))
            {
                var json = await file.ReadToEndAsync();
                listOfPharmacies = JsonSerializer.Deserialize<List<Pharmacy>>(json);
            }

            return listOfPharmacies;
        }
        catch (Exception ex)
        {
            _logger.Error($"{nameof(GetPharmaciesAsync)}, Exception while getting the list of Pharmacies, message = {ex.Message}", ex);
        }

        return null;
    }

    public async Task<List<Pharmacy>?> GetPharmaciesAsync(TimeSpan start, TimeSpan end)
    {
        var listOfPharmacies = new List<Pharmacy>();
        try
        {
            using (StreamReader file = File.OpenText(_dbFilePath))
            {
                var json = await file.ReadToEndAsync();
                listOfPharmacies = JsonSerializer.Deserialize<List<Pharmacy>>(json);
            }

            if (listOfPharmacies != null && listOfPharmacies.Any())
            {
                return listOfPharmacies.Where(p => TimeSpan.Parse(p.OpenningTime) <= start
                && TimeSpan.Parse(p.ClosingTime) >= end).ToList();
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"{nameof(GetPharmaciesAsync)}, Exception while getting the list of Pharmacies, message = {ex.Message}", ex);
        }

        return null;
    }

    private async Task WritePharmaciesToFileAsync(List<Pharmacy> pharmacies)
    {
        var json = JsonSerializer.Serialize(pharmacies);
        await File.WriteAllTextAsync(_dbFilePath, json);
    }
}
