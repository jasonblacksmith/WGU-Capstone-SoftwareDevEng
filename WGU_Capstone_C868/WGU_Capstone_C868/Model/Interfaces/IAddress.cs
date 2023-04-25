namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IAddress
    {
        int AddressId { get; set; }
        string StreetAddress { get; set; }
        string SuiteNumber { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        string Country { get; set; }
    }
}