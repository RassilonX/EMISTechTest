﻿using System.ComponentModel.DataAnnotations;

namespace Database.Models;

public class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int GMC { get; set; }
}