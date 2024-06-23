using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos;

public class ImportJsonDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int GMC { get; set; }

    public List<Address> Address { get; set; }
}
