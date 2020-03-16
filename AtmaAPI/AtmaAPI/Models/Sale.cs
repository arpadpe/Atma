using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public double SalesPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
