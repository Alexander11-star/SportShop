using Microsoft.Extensions.Configuration;
using API_RestFull_Prueba.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SportShop.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace API_RestFull_Prueba.Models.User
{
    public class UsersDAL
    {
        private readonly string _connectionStrings;
        CredencialesDeAcceso acceso = new CredencialesDeAcceso();

        public UsersDAL(IConfiguration configuration)
        {




            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Crea o  actualiza usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task StoreOrUpdateUser(Users user)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateUser", sql))
                {
                    CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                    RijndaelManaged myRijndael = new RijndaelManaged();
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    Byte[] contrasenaEncriptada = acceso.EncryptStringToBytes(user.Password, myRijndael.Key, myRijndael.IV);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidUser", user.IdUser));
                    cmd.Parameters.Add(new SqlParameter("@perspro", user.IdProfile));
                    cmd.Parameters.Add(new SqlParameter("@pFirstName", user.Name));
                    cmd.Parameters.Add(new SqlParameter("@pPassword", contrasenaEncriptada));
                    cmd.Parameters.Add(new SqlParameter("@pkey", myRijndael.Key));
                    cmd.Parameters.Add(new SqlParameter("@pIv", myRijndael.IV));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                    
                }
            }
        }

        /// <summary>
        /// Crea o  actualiza usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task StoreOrUpdateSales(Sales sales)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateSales", sql))
                {
                    CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                    RijndaelManaged myRijndael = new RijndaelManaged();
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pIdSales", sales.IdSales));
                    cmd.Parameters.Add(new SqlParameter("@pIdSProduct", sales.IdSProduct));
                    cmd.Parameters.Add(new SqlParameter("@pIdUser", sales.IdUser));
                    cmd.Parameters.Add(new SqlParameter("@pQuantity", sales.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@pDateSales", sales.DateSales));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;

                }
            }
        }

        /// <summary>
        /// Elimina venta por Id
        /// </summary>
        /// <param name="pidUser"></param>
        /// <returns></returns>
        public async Task<int> DeleteSales(int pidSales)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteSales", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidSales", pidSales));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return 1;
                }
            }

        }

        /// <summary>
        /// Elimina producto por Id
        /// </summary>
        /// <param name="pidUser"></param>
        /// <returns></returns>
        public async Task<int> DeleteProduct(int pidProduct)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteProduct", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidproduct", pidProduct));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return 1;
                }
            }

        }

        /// <summary>
        /// Trae la lista de perfiles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Profile>> getProfiles()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getProfiles", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Profile>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToProfile(reader));
                        }
                    }
                    return response;
                }
            }
        }

        /// <summary>
        /// Trae la lista de perfiles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sales>> getSales()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getSalesAll", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Sales>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToSales(reader));
                        }
                    }
                    return response;
                }
            }
        }

        /// <summary>
        /// Trae la lista de perfiles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sales>> getSalesGroup()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getSalesgroup", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Sales>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToSalesgroup(reader));
                        }
                    }
                    return response;
                }
            }
        }

        /// <summary>
        /// trae la lista de usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<List<Users>> getUsers()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getUsers", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Users>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapUserDTO(reader));
                        }

                    }
                    return response;
                }
            }
        }

        /// <summary>
        /// trae la lista de productos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Products>> GetProducts()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getProducts", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Products>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapProductDTO(reader));
                        }
                    }

                    return response;
                }
            }
        }

        /// <summary>
        /// crea o actualiza los productos
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task StoreOrUpdateProducts(Products products)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateProducts", sql))
                {
                    CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                    RijndaelManaged myRijndael = new RijndaelManaged();
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pOductsreg", products.IdProduct));
                    cmd.Parameters.Add(new SqlParameter("@pOductsnom", products.NameProduct));
                    cmd.Parameters.Add(new SqlParameter("@pOductsdes", products.DescriptionProduct));
                    cmd.Parameters.Add(new SqlParameter("@pOductscos", products.CostProduct));
                    cmd.Parameters.Add(new SqlParameter("@pOductsimg", products.ImageProduct));
                    cmd.Parameters.Add(new SqlParameter("@pOductscod", products.CodProduct));                   
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;                    
                }
            }
        }

        /// <summary>
        /// Mapping usuario
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Users MapUserDTO(SqlDataReader reader)
        {
            return new Users()
            {
                IdUser = (int)reader["ersreg"],
                IdProfile = (int)reader["erspro"],
                Name = reader["ersnom"].ToString(),
                Password = reader["erspass"].ToString()
            };
        }

        /// <summary>
        /// Maping producto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Products MapProductDTO(SqlDataReader reader)
        {
            return new Products()
            {
                IdProduct = (int)reader["oductsreg"],
                NameProduct = reader["oductsnom"].ToString(),
                DescriptionProduct = reader["oductsdes"].ToString(),
                CostProduct = (decimal)reader["oductscos"],
                ImageProduct = reader["oductsimg"].ToString(),
                CodProduct = reader["oductscod"].ToString()
            };
        }

        /// <summary>
        /// Mapping perfiles de usuario
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Profile MapToProfile(SqlDataReader reader)
        {
            return new Profile()
            {
                IdProfile = (int)reader["ofilereg"],
                NameProfile = reader["ofilename"].ToString(),
            };
        }


        /// <summary>
        /// Mapping perfiles de usuario
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Sales MapToSales(SqlDataReader reader)
        {
            return new Sales()
            {
                IdSales = (int)reader["lesreg"],
                IdSProduct = (int)reader["lespro"],
                IdUser = (int)reader["lesuse"],
                IdProfile = (int)reader["ofilereg"],
                Quantity = (int)reader["lescan"],
                DateSales = (DateTime)reader["lesdat"],
                NameSProduct = reader["oductsnom"].ToString(),
                DescriptionSProduct = reader["oductsdes"].ToString(),
                CostSProduct = (decimal)reader["oductscos"],
                ImageSProduct = reader["oductsimg"].ToString(),
                NameUser = reader["ersnom"].ToString(),
                Password = reader["erspass"].ToString(),
                NameProfile = reader["ofilename"].ToString()
            };
        }

        /// <summary>
        /// Mapping perfiles de usuario
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Sales MapToSalesgroup(SqlDataReader reader)
        {
            return new Sales()
            {
                IdSProduct = (int)reader["lespro"],
                Quantity = (int)reader["lescan"]
            };
        }
    }
}
