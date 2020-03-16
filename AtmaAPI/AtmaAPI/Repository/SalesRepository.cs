using AtmaAPI.Data;
using AtmaAPI.Models;
using AtmaAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly SalesContext _salesContext;

        public SalesRepository(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }

        public void AddSale(Sale sale)
        {
            _salesContext.Add(sale);
            _salesContext.SaveChanges();
        }

        public IEnumerable<Sale> GetSales()
        {
            return _salesContext.Sales;
        }
    }
}
