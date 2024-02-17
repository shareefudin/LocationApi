using AutoMapper;
using Location.API.Entities;
using Location.API.Models;
using Location.API.Services;
using Location.API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Location.API.Controllers;

[ApiController]
[Route(Constants.ApiEndPoints.Pharmacy.BaseRoute)]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyService _pharmacyService;
    private readonly IMapper _mapper;

    public PharmacyController(IPharmacyService pharmacyService, IMapper mapper)
    {
        _pharmacyService = pharmacyService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route(Constants.ApiEndPoints.Pharmacy.GetAvailablePharmacies)]
    public async Task<IActionResult> GetAvailablePharmacies()
    {
        var response = await _pharmacyService.GetPharmaciesAsync();

        if (response.IsError)
        {
            return NotFound(response.Errors.FirstOrDefault());
        }

        return Ok(response.Payload);
    }

    [HttpPost]
    [Route(Constants.ApiEndPoints.Pharmacy.AddPharmacy)]
    public async Task<IActionResult> AddPharmacy([FromBody] PharmacyModel pharmacy)
    {
        var validator = new PharmacyModelValidator();
        var validationResult = validator.Validate(pharmacy);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.FirstOrDefault());
        }

        var pharmacyMapped = _mapper.Map<Pharmacy>(pharmacy);

        var response = await _pharmacyService.AddPharmacyAsync(pharmacyMapped);

        if (response.IsError)
        {
            return NotFound(response.Errors.FirstOrDefault());
        }

        return Ok(response.Payload);
    }

    [HttpGet]
    [Route(Constants.ApiEndPoints.Pharmacy.GetAvailablePharmacyIntervals)]
    public async Task<IActionResult> GetAvailablePharmacyIntervals([FromQuery] string startTime, string closingTime)
    {
        if(startTime == null || !TimeSpan.TryParse(startTime, out TimeSpan start))
        {
            return BadRequest("Invalid Start Time");
        }

        if (closingTime == null || !TimeSpan.TryParse(closingTime, out TimeSpan end))
        {
            return BadRequest("Invalid Closing Time");
        }

        if (start > end)
        {
            return BadRequest("Start Time is Greater than End Time");
        }

        var response = await _pharmacyService.GetPharmacyOpenBetweenIntervalAsync(start, end);

        if (response.IsError)
        {
            return NotFound(response.Errors.FirstOrDefault());
        }

        return Ok(response.Payload);
    }
}
