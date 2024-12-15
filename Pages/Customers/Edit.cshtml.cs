using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crmapp.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
            public int user_id { get; set;}

            [BindProperty, Required(ErrorMessage = "Name is required")]
            public string user_name { get; set;} = "";

            [BindProperty, EmailAddress]
            public string email { get; set;} = "";

            [BindProperty, Required(ErrorMessage = "Password is required")]
            public string password { get; set;} = "";

            public string ErrorMessage {get; set;} = "";
        public void OnGet(int id)
        {
            try {
                string connectionString = "Server=SAFAAA\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM customer WHERE user_id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@id", user_id);

                        using (SqlDataReader reader = command.ExecuteReader()){
                            if (reader.Read()){
                                user_id = reader.GetInt32(0);
                                user_name = reader.GetString(1);
                                email = reader.GetString(2);
                                password = reader.GetString(3);
                            }
                            else{
                                Response.Redirect("/Customers/Index");
                            }
                        }
                    }
                }
            }

            catch (Exception ex) {
                ErrorMessage = ex.Message;
            }
        }
        public void OnPost(){
            if(!ModelState.IsValid){
                return;
            }

            if(email == null) email = "";

            //update customer details
            try{
                string connectionString = "Server=SAFAAA\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "UPDATE customer SET user_name=@user_name, email=@email, password=@password";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
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