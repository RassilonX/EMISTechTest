using Database.Models;

namespace FullStackTechTest.Models.Home;

public class AdminViewModel
{
    public List<Specialty> SpecialtyList { get; set; }

    public bool IsEditing { get; set; }
}
