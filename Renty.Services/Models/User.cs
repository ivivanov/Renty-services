using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Renty.Services.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public virtual ICollection<Item> ItemsToReturn { get; set; }

        public virtual ICollection<Item> ItemsToReceive { get; set; }

        public User()
        {
            this.ItemsToReceive = new HashSet<Item>();
            this.ItemsToReturn = new HashSet<Item>();
        }
    }
}
