using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Page_Count { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public bool IsCont { get; set; }
        public int Quantity { get; set; }
    }
}
