using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace crmapp.Pages.Customers
{
    public class Create : PageModel
    {
        
            [BindProperty, Required(ErrorMessage = "Name is required")]
            public string user_name { get; set;} = "";

            [BindProperty, EmailAddress]
            public string email { get; set;} = "";

            [BindProperty, Required(ErrorMessage = "Password is required")]
            public string password { get; set;} = "";
        public void OnGet()
        {

        }

        public string ErrorMessage {get; set;} = "";
        public void OnPost()
        {
            if (ModelState.IsValid){
                return;
            }

            if(email == null) email = "";
            
            // create new customer
            try{
                string connectionString = "Server=SAFAAA\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "INSERT INTO customer " + 
                        "(user_name, email, password) VALUES " + 
                        "(@user_name, @email, @password);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@user_name", user_name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex){
                ErrorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Customers/Index");
        }
    }
}