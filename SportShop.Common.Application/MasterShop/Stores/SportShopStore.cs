using SportShop.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using SCSE.Framework.Common;
using SportShop.Common.Application.MasterShop.Stores;
using Newtonsoft.Json;
using Microsoft.Xrm.Sdk.Messages;

namespace SportShop.Common.Application
{
    public class SportShopStore: ISportShop
    {
        private readonly HttpClient _http;

        #region urls
        public string URL = "https://localhost:44314";

        public string UrlSales = "/api/Sales/getSales";
        public string UrlUser = "/api/User/getUsers";
        public string UrlProducts = "/api/Products/getProducts";
        public string UrlcreateSales = "https://localhost:44314/api/Sales/CreateSales";
        public string UrldeleteSaless = "https://localhost:44314/api/Sales/DeleteSales/";
        public string UrldeleteProducts = "https://localhost:44314/api/Products/DeleteProduct/";
        public string UrlgroupSaless = "https://localhost:44314/api/Sales/getSalesGroup/";
        public string UrlCreateProduct = "https://localhost:44314/api/Products/CreateProduct";
        

        #endregion

        public SportShopStore(HttpClient Http)
        {
            _http = Http;
        }
      
        /// <summary>
        /// Busca las ventas totales
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Sales>> GetSales()
        {
            using (WebApiHelper<List<Sales>> webHelper = new WebApiHelper<List<Sales>>())
            {              
                try
                {
                    var request = await webHelper.GetDataFromServiceAsync(URL, UrlSales);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Busca las ventas agrupadas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Sales>> GetSalesGroup()
        {
            using (WebApiHelper<List<Sales>> webHelper = new WebApiHelper<List<Sales>>())
            {               
                try
                {
                    var request = await webHelper.GetDataFromServiceAsync(URL, UrlgroupSaless);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Busca usuarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Users>> GetUsers()
        {
            using (WebApiHelper<List<Users>> webHelper = new WebApiHelper<List<Users>>())
            {               
                try
                {
                    var request = await webHelper.GetDataFromServiceAsync(URL, UrlUser);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Busca los productos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Products>> GetProducts()
        {
            using (WebApiHelper<List<Products>> webHelper = new WebApiHelper<List<Products>>())
            {
                try
                {
                    var request = await webHelper.GetDataFromServiceAsync(URL, UrlProducts);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }


        /// <summary>
        /// guardar las ventas
        /// </summary>
        /// <param name="costCenterRelationship"></param>
        /// <returns></returns>

        public async Task<Sales> Save(Sales sales)
        {
            using (WebApiHelper<Sales> webHelper = new WebApiHelper<Sales>())
            {
                try
                {
                    Sales request = await webHelper.PostAsync(UrlcreateSales, sales);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }

            }
        }

        /// <summary>
        /// guardar los productos
        /// </summary>
        /// <param name="costCenterRelationship"></param>
        /// <returns></returns>

        public async Task<Products> SaveProducts(Products products)
        {
            using (WebApiHelper<Products> webHelper = new WebApiHelper<Products>())
            {
                try
                {
                    var request = await webHelper.PostAsync(UrlCreateProduct, products);
                    return request;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }


        /// <summary>
        /// borrar registro de maestro
        /// </summary>
        /// <param name="deleteCode"></param>
        /// <returns></returns>
        public async Task<int> DeleteSales(int id)
        {
            int result;

            using (WebApiHelper<int> webHelper = new WebApiHelper<int>(null))
            {
                try
                {
                    result = await webHelper.DeleteAsync(UrldeleteSaless, $"{id}");
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

        }

        /// <summary>
        /// borrar registro de maestro
        /// </summary>
        /// <param name="deleteCode"></param>
        /// <returns></returns>
        public async Task<int> DeleteProduct(int id)
        {
            int result;

            using (WebApiHelper<int> webHelper = new WebApiHelper<int>(null))
            {
                try
                {
                    result = await webHelper.DeleteAsync(UrldeleteProducts, $"{id}");
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

        }

    }
}
