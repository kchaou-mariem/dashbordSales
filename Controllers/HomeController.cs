using Microsoft.AspNetCore.Mvc;
using dashbordSales.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // ✅
using System.Collections.Generic;
using System.Data;

namespace dashbordSales.Controllers
{
    //[Route("Dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                SalesPerMonth = GetSalesPerMonth(),
                SalesQuantityPerMonth = GetSalesQuantityPerMonth(),
                TopCustomersBySales = GetTopCustomersBySales(),
                TopProductsByQuantity = GetTopProductsByQuantitySold(),
                SalesByCountry = GetSalesByCountry(),
                SalesBySalesperson = GetSalesBySalesperson(),
                SalesByProductCategory = GetSalesByProductCategory()
            };

            return View(vm);
        }

        private readonly IConfiguration _configuration;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
      

        [Route("Dashboard/SalesPerMonth")]

        private List<SalesPerMonthModel> GetSalesPerMonth()
        {
            var list = new List<SalesPerMonthModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetTotalSalesPerMonth", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesPerMonthModel
                            {
                                Month = reader["Month"].ToString(),
                                TotalSales = reader["TotalSales"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSales"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/SalesQuantityPerMonth")]
        private List<SalesQuantityPerMonthModel> GetSalesQuantityPerMonth()
        {
            var list = new List<SalesQuantityPerMonthModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetTotalSalesQuantityPerMonth", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesQuantityPerMonthModel
                            {
                                Month = reader["Month"].ToString(),
                                TotalSalesQuantity = reader["TotalSalesQuantity"] != DBNull.Value ? Convert.ToInt32(reader["TotalSalesQuantity"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/TopCustomersBySales")]
        private List<TopCustomersModel> GetTopCustomersBySales()
        {
            var list = new List<TopCustomersModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetTop5CustomersBySales", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TopCustomersModel
                            {
                                CustomerName = reader["CustomerName"].ToString(),
                                TotalSales = reader["TotalSales"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSales"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/TopProductsByQuantity")]
        private List<TopProductsModel> GetTopProductsByQuantitySold()
        {
            var list = new List<TopProductsModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetTop5ProductsByQuantitySold", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TopProductsModel
                            {
                                StockItemName = reader["StockItemName"].ToString(),
                                TotalQuantitySold = reader["TotalQuantitySold"] != DBNull.Value ? Convert.ToInt32(reader["TotalQuantitySold"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/SalesByCountry")]
        private List<SalesByCountryModel> GetSalesByCountry()
        {
            var list = new List<SalesByCountryModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetSalesByCountry", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesByCountryModel
                            {
                                CountryName = reader["CountryName"].ToString(),
                                TotalSalesRevenue = reader["TotalSalesRevenue"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSalesRevenue"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/SalesBySalesperson")]
        private List<SalesBySalespersonModel> GetSalesBySalesperson()
        {
            var list = new List<SalesBySalespersonModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetSalesBySalesperson", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesBySalespersonModel
                            {
                                SalespersonName = reader["SalespersonName"].ToString(),
                                TotalSalesRevenue = reader["TotalSalesRevenue"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSalesRevenue"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


        [Route("Dashboard/SalesByProductCategory")]
        private List<SalesByProductCategoryModel> GetSalesByProductCategory()
        {
            var list = new List<SalesByProductCategoryModel>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DwhConnection")))
            {
                conn.Open();
                using (var cmd = new SqlCommand("GetSalesByProductCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SalesByProductCategoryModel
                            {
                                ProductCategory = reader["ProductCategory"].ToString(),
                                TotalSalesRevenue = reader["TotalSalesRevenue"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSalesRevenue"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }


    }
}
