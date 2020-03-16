using AtmaAPI.Models;
using AtmaAPI.Repository.Interface;
using AtmaAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Dictionary<DateTime, int> GetSoldArticlesPerDay()
        {
            var sales = _salesRepository.GetSales();
            
            // Group sales by date, count of sales
            return sales
                .GroupBy(x => x.Date)
                .ToDictionary(x => x.Key, x => x.Count());
        }

        public Dictionary<DateTime, double> GetTotalRevenuePerDay()
        {
            var sales = _salesRepository.GetSales();

            // Group sales by date, sum of sales
            return sales
                .GroupBy(x => x.Date)
                .ToDictionary(x => x.Key, x => x.ToList().Sum(x => x.SalesPrice));
        }

        public Dictionary<string, double> GetTotalRevenuePerArticle()
        {
            var sales = _salesRepository.GetSales();

            // Group sales by article name, sum of sales
            return sales
                .GroupBy(x => x.ArticleNumber)
                .ToDictionary(x => x.Key, x => x.ToList().Sum(x => x.SalesPrice));
        }

        public void RecordSale(Sale sale)
        {
            _salesRepository.AddSale(sale);
        }
    }
}
