using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renty.Services.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageBase64 { get; set; }

        [Required]
        public DateTime DateBorrowed { get; set; }

        public DateTime ReturnDeadline { get; set; }

        public string Notes { get; set; }

        public string Owner { get; set; }

        public string Renter { get; set; }

        public bool IsReturned { get; set; }

        public Item()
        {
            this.IsReturned = false;
        }
    }
}
