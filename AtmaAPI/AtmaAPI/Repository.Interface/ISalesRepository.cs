using AtmaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmaAPI.Repository.Interface
{
    public interface ISalesRepository
    {
        void AddSale(Sale sale);
        IEnumerable<Sale> GetSales();
    }
}
