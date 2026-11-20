namespace BlazorShop.Models.DTOs.Address;

public class AddressUpdateRequestDto
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? PostalCode { get; set; }
    public bool? IsPrimary { get; set; }
}
