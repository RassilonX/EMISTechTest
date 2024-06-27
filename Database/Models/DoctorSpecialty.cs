using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;

public class DoctorSpecialty
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int SpecialtyId { get; set; }
}
