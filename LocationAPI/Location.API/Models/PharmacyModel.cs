using System.ComponentModel;

namespace Location.API.Models;

public class PharmacyModel
{
    public string Name { get; set; }
    [DefaultValue("HH:MM:SS")]
    public string OpenningTime { get; set; }
    [DefaultValue("HH:MM:SS")]
    public string ClosingTime { get; set; }
}
