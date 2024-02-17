using Location.API.Common;
using Location.API.Entities;

namespace Location.API.Repositories;

public interface IPharmacyRepository
{
    Task<List<Pharmacy>?> GetPharmaciesAsync();

    Task<Pharmacy?> AddPharmacyAsync(Pharmacy pharmacy);
    Task<List<Pharmacy>?> GetPharmaciesAsync(TimeSpan start, TimeSpan end);
}
