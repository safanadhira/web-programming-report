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
    public class Delete : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(int id){
            deleteCustomer(id);
            Response.Redirect("/Customers/Index");
        }

        private void deleteCustomer(int id){
            try{
            string connectionString = "Server=SAFAAA\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                     //delete customer from databas
                     string sql = "DELETE FROM customer WHERE user_id=@user_id";
                     using(SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@user_id", id);
                        command.ExecuteNonQuery();
                     }
                }
            }
            catch(Exception ex){
                Console.WriteLine("Canncot delete customer: " + ex.Message);
            }
        }
    }
}