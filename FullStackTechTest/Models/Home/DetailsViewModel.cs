using DAL.Interfaces;
using Database.Models;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; }
    public Address Address { get; set; }

    public Dictionary<string, bool> SpecialtyList { get; set; } = new Dictionary<string, bool>();
    public bool IsEditing { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(
        int personId, 
        bool isEditing, 
        IPersonRepository personRepository, 
        IAddressRepository addressRepository, 
        IDoctorSpecialtyRepository personSpecialtyRepository)
    {
        var person = await personRepository.GetByIdAsync(personId);
        var model = new DetailsViewModel
        {
            Person = person,
            Address = await addressRepository.GetForPersonIdAsync(personId),
            SpecialtyList = await personSpecialtyRepository.ListAllDoctorSpecialtiesByPersonAsync(person),
            IsEditing = isEditing
        };
        return model;
    }
}