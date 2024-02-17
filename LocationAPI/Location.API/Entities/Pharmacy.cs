namespace Location.API.Entities;

public sealed class Pharmacy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OpenningTime { get; set; }

    public string ClosingTime { get; set; }
}
