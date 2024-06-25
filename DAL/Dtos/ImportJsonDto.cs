using Database.Models;

namespace DAL.Dtos;

public class ImportJsonDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int GMC { get; set; }

    public List<Address> Address { get; set; } = new List<Address>();
}
