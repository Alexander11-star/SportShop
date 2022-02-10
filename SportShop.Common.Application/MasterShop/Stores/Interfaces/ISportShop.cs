using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SportShop.Model.Common;
using SCSE.Framework.Security.Model;

namespace SportShop.Common.Application.MasterShop.Stores
{
    public interface ISportShop
    {
        Task<List<Sales>> GetSales();
        Task<List<Sales>> GetSalesGroup();        
        Task<List<Users>> GetUsers();
        Task<List<Products>> GetProducts();
        Task<Sales> Save(Sales sales);
        Task<Products> SaveProducts(Products products);
        Task<int> DeleteSales(int id);
        Task<int> DeleteProduct(int id);

    }
}
