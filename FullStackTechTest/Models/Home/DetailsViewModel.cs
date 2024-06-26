using DAL.Interfaces;
using Database.Models;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; }
    public Address Address { get; set; }
    public List<DoctorSpecialty> DoctorSpecialties { get; set; }
    public List<Specialty> Specialties { get; set; }
    public bool IsEditing { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(
        int personId, 
        bool isEditing, 
        IPersonRepository personRepository, 
        IAddressRepository addressRepository, 
        IPersonSpecialtyRepository personSpecialtyRepository)
    {
        var person = await personRepository.GetByIdAsync(personId);
        var model = new DetailsViewModel
        {
            Person = person,
            Address = await addressRepository.GetForPersonIdAsync(personId),
            DoctorSpecialties = await personSpecialtyRepository.ListDoctorSpecialtyAsync(person),
            Specialties = await personSpecialtyRepository.ListAllSpecialtiesAsync(),
            IsEditing = isEditing
        };
        return model;
    }
}