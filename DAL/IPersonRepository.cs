using Database.Models;

namespace DAL;

public interface IPersonRepository
{
    Task<List<Person>> ListAllAsync();
    Task<int> PersonCountAsync();
    Task<Person> GetByIdAsync(int personId);
    Task SaveAsync(Person person);
}