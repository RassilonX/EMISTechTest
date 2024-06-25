using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class Address
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string Line1 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Postcode { get; set; } = string.Empty;

    // assume all records will be UK, i.e. disregard "Country" field
}