using AutoMapper;
using Location.API.Entities;
using Location.API.Models;

namespace Location.API.Mappings;

public class PharmacyMap : Profile
{
    public PharmacyMap()
    {
        CreateMap<PharmacyModel, Pharmacy>();
    }
}
