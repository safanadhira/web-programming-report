using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crmapp.Pages.Customers
{
    public class Index : PageModel
    {
        public List<CustomerInfo> CustomersList { get; set;} = [];
        public void OnGet()
        {
            try {
                string connectionString = "Server=SAFAAA\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM customer";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        using (SqlDataReader reader = command.ExecuteReader()){
                            while (reader.Read()){
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.user_id = reader.GetInt32(0);
                                customerInfo.user_name = reader.GetString(1);
                                customerInfo.email = reader.GetString(2);
                                customerInfo.password = reader.GetString(3);

                                CustomersList.Add(customerInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("We have an error: " + ex.Message);
            }
        }
    }

    public class CustomerInfo {
        public int user_id { get; set;}
        public string user_name { get; set;} = "";
        public string email { get; set;} = "";
        public string password { get; set;} = "";
    }
}