﻿using System;
using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> ProductsSold { get; set; } = new List<Product>();

        public ICollection<Product> ProductsBought { get; set; } = new List<Product>();
    }
}
