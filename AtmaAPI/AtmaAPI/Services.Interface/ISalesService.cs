using AtmaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Services.Interface
{
    public interface ISalesService
    {
        void RecordSale(Sale sale);
        Dictionary<DateTime, int> GetSoldArticlesPerDay();
        Dictionary<DateTime, double> GetTotalRevenuePerDay();
        Dictionary<string, double> GetTotalRevenuePerArticle();
    }
}
