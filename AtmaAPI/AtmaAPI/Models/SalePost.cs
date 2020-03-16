using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Models
{
    public class SalePost
    {
        public string ArticleNumber { get; set; }
        public double? SalesPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
