using Database.Models;

namespace FullStackTechTest.Models.Import;

public class ImportJsonModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int GMC { get; set; }

    public List<Address> Address { get; set; }
}
