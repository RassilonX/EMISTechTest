using Database.Models;

namespace FullStackTechTest.Models.Home;

public class AdminViewModel
{
    public List<Specialty> SpecialtyList { get; set; } = new List<Specialty>();

    public bool IsEditing { get; set; } = false;

    public bool AddNewSpecialty { get; set; } = false;

    public string NewSpecialty { get; set; } = string.Empty;
}
