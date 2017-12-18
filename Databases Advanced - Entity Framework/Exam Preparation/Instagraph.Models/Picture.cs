using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class Picture
    {
        public Picture()
        {
            Users = new List<User>();
            Posts = new List<Post>();
        }

        
        public int Id { get; set; }
        
        public string Path { get; set; }
        
        public decimal Size { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
