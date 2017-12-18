using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(15,80)]
        public int Age { get; set; }

        public int PositionId { get; set; }
        [Required]
        public Position Position { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}