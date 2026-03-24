using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace


{
    public class ProductsModel : PageModel
    {
        public List<Product> Products { get; set; }

        // This runs when the page loads 
        public void OnGet()
       {
            Products = new List<Product>();
            string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
       
            using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Open the connection to the database
            connection.Open();

            // SQL query to select product name, category name, and price 
            string sql = @"SELECT p.ProductName, c.CategoryName, p.UnitPrice
                           FROM Products p
                           JOIN Categories c ON p.CategoryID = c.CategoryID";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        // Makes a new product object and fill it with data 
                        Products.Add(new Product
                        {
                            ProductName = reader.GetString(0),
                            CategoryName = reader.GetString(1),
                            UnitPrice = reader.GetDecimal(2)
                        });
                    }
                }
            }
        }
    }
}

public class Product
{
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public decimal UnitPrice { get; set; }
}
    }

