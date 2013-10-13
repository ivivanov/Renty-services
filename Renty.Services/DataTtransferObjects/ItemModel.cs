using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Renty.Services.DataTtransferObjects
{
    public class ItemModel
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string ImageBase64 { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime ReturnDeadline { get; set; }

        public string Notes { get; set; }

        public string Owner { get; set; }

        public string Renter { get; set; }
    }
}
