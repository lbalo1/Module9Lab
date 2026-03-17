using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyApp.Namespace
{
    public class CustomersModel : PageModel
    { 
        // Create a list to hold the list of customers that we retrievee from the database
        public List<Customers> Customers { get; set; }
        public void OnGet()
        {
            Customers = new List<Customer>();

            string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

            //Open our database connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT CustomerID, CompanyName, ContactName, ContactName, Country FROM Customers";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers.Add(new Customer
                            {
                                CustomerID = reader.GetString(0),
                                CompanyName = reader.GetString(1),
                                ContactName = reader.GetString(2),
                                Country = reader.GetString (3)
                            });
                        }
                    }
                }
            }
        }
    }
}

// Class for representing a customer from Northwind database

public class Customer
{
    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Country { get; set;}
}