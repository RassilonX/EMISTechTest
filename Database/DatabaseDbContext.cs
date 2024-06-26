using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseDbContext : DbContext
{
    public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<DoctorSpecialty> PersonSpecialties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Person>()
            .Property(p => p.FirstName)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Person>()
            .Property(p => p.LastName)
            .HasColumnType("varchar(50)");

        modelBuilder.Entity<Address>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Address>()
            .Property(a => a.Line1)
            .HasColumnType("varchar(200)");

        modelBuilder.Entity<Address>()
            .Property(a => a.City)
            .HasColumnType("varchar(100)");

        modelBuilder.Entity<Address>()
            .Property(a => a.Postcode)
            .HasColumnType("varchar(8)");

        modelBuilder.Entity<Specialty>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Karilynn", LastName = "Rock", GMC = 1169492 },
            new Person { Id = 2, FirstName = "Arlyn", LastName = "Coetzee", GMC = 8836597 },
            new Person { Id = 3, FirstName = "Carly", LastName = "Heeran", GMC = 9548968 },
            new Person { Id = 4, FirstName = "Rae", LastName = "Tuft", GMC = 1807233 },
            new Person { Id = 5, FirstName = "Eugenius", LastName = "Uden", GMC = 3229745 },
            new Person { Id = 6, FirstName = "Constancia", LastName = "Salerno", GMC = 7208570 },
            new Person { Id = 7, FirstName = "Harmon", LastName = "Stent", GMC = 1215810 },
            new Person { Id = 8, FirstName = "Adriano", LastName = "Jull", GMC = 6428880 },
            new Person { Id = 9, FirstName = "Garrek", LastName = "Frenzl", GMC = 9279644 },
            new Person { Id = 10, FirstName = "Letitia", LastName = "Grabert", GMC = 3699271 },
            new Person { Id = 11, FirstName = "Dulcia", LastName = "Chadbourn", GMC = 4793921 },
            new Person { Id = 12, FirstName = "Hilliard", LastName = "Glendza", GMC = 8854603 },
            new Person { Id = 13, FirstName = "Rena", LastName = "McCarl", GMC = 9442324 },
            new Person { Id = 14, FirstName = "Tobiah", LastName = "Wheldon", GMC = 9488055 },
            new Person { Id = 15, FirstName = "Morgun", LastName = "Downe", GMC = 2520979 },
            new Person { Id = 16, FirstName = "Cullan", LastName = "Weiser", GMC = 7708262 },
            new Person { Id = 17, FirstName = "Niles", LastName = "Kief", GMC = 7046029 },
            new Person { Id = 18, FirstName = "Randi", LastName = "McLorinan", GMC = 3086851 },
            new Person { Id = 19, FirstName = "Virgil", LastName = "Sperry", GMC = 3165201 },
            new Person { Id = 20, FirstName = "Hoyt", LastName = "Turbern", GMC = 1795805 }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, PersonId = 1, Line1 = "17 Cae Rhos", City = "Brynteg", Postcode = "LL78 8JF" },
            new Address { Id = 2, PersonId = 2, Line1 = "8 Lonsdale Road", City = "Exeter", Postcode = "EX1 3DW" },
            new Address { Id = 3, PersonId = 3, Line1 = "36 Close Lea", City = "Brighouse", Postcode = "HD6 3AR" },
            new Address { Id = 4, PersonId = 4, Line1 = "14 The Avenue", City = "Bournemouth", Postcode = "BH2 6LT" },
            new Address { Id = 5, PersonId = 5, Line1 = "22 Oakdale Road", City = "Plymouth", Postcode = "PL3 6QN" },
            new Address { Id = 6, PersonId = 6, Line1 = "11 The Crescent", City = "Newcastle", Postcode = "NE2 4AU" },
            new Address { Id = 7, PersonId = 7, Line1 = "19 The Green", City = "Leeds", Postcode = "LS12 1AQ" },
            new Address { Id = 8, PersonId = 8, Line1 = "25 The Drive", City = "Sheffield", Postcode = "S10 2DF" },
            new Address { Id = 9, PersonId = 9, Line1 = "30 The Avenue", City = "Bristol", Postcode = "BS6 5BX" },
            new Address { Id = 10, PersonId = 10, Line1 = "42 The Close", City = "Nottingham", Postcode = "NG1 4BH" },
            new Address { Id = 11, PersonId = 11, Line1 = "15 The Square", City = "Oxford", Postcode = "OX1 1AQ" },
            new Address { Id = 12, PersonId = 12, Line1 = "28 The Street", City = "Cambridge", Postcode = "CB2 3TN" },
            new Address { Id = 13, PersonId = 13, Line1 = "31 The Lane", City = "London", Postcode = "SW1A 1AA" },
            new Address { Id = 14, PersonId = 14, Line1 = "18 The Grove", City = "Manchester", Postcode = "M1 5FN" },
            new Address { Id = 15, PersonId = 15, Line1 = "24 The Rise", City = "Birmingham", Postcode = "B1 2EA" },
            new Address { Id = 16, PersonId = 16, Line1 = "39 The Hill", City = "Leicester", Postcode = "LE1 5DE" },
            new Address { Id = 17, PersonId = 17, Line1 = "45 The View", City = "Cardiff", Postcode = "CF10 1BH" },
            new Address { Id = 18, PersonId = 18, Line1 = "50 The Way", City = "Edinburgh", Postcode = "EH1 3YL" },
            new Address { Id = 19, PersonId = 19, Line1 = "55 The Place", City = "Glasgow", Postcode = "G1 1XE" },
            new Address { Id = 20, PersonId = 20, Line1 = "60 The Street", City = "Belfast", Postcode = "BT1 5GS" }
        );

        modelBuilder.Entity<Specialty>().HasData(
            new Specialty { Id = 1, SpecialtyName = "Anaesthetics" },
            new Specialty { Id = 2, SpecialtyName = "Cardiology" },
            new Specialty { Id = 3, SpecialtyName = "Dermatology" },
            new Specialty { Id = 4, SpecialtyName = "Emergency Medicine" },
            new Specialty { Id = 5, SpecialtyName = "General Practice (GP)" },
            new Specialty { Id = 6, SpecialtyName = "Neurology" },
            new Specialty { Id = 7, SpecialtyName = "Obstetrics and Gynaecology" },
            new Specialty { Id = 8, SpecialtyName = "Ophthalmology" },
            new Specialty { Id = 9, SpecialtyName = "Orthopaedic Surgery" },
            new Specialty { Id = 10, SpecialtyName = "Psychiatry" }
        );
    }
}
