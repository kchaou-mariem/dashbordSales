namespace dashbordSales.Models
{
    
        public class DashboardViewModel
        {
            public List<SalesPerMonthModel> SalesPerMonth { get; set; }
            public List<SalesQuantityPerMonthModel> SalesQuantityPerMonth { get; set; }
            public List<TopCustomersModel> TopCustomersBySales { get; set; }
            public List<TopProductsModel> TopProductsByQuantity { get; set; }
            public List<SalesByCountryModel> SalesByCountry { get; set; }
            public List<SalesBySalespersonModel> SalesBySalesperson { get; set; }
            public List<SalesByProductCategoryModel> SalesByProductCategory { get; set; }
        }
    

}
