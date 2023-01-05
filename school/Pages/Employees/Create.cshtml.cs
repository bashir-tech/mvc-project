using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace school.Pages.Employees
{
    public class CreateModel : PageModel
    {

        public St_info st = new St_info();
        public string errorMessage = " ";
        public string successMessage = " ";
        public void OnGet()
        {


        }
        public void  OnPost()
            {

            
            st.FirstName = Request.Form["FirstName"];
            st.LastName = Request.Form["LastName"];
          

            if( st.FirstName.Length==0 || st.LastName.Length == 0)

            {
              errorMessage = "all the fields are required";
                return;
            }


            // save date base

            try
            {


                string connectionstring = "Data Source=BASHIR\\SQLEXPRESS;" +
                    "Initial Catalog=Northwind;Integrated Security=True;" +
                    "Connect Timeout=30;Encrypt=False;" +
                    "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection con=new SqlConnection(connectionstring))
                {
                    con.Open();


                    string sql = "sp_insert";

                    using (SqlCommand com=new SqlCommand(sql,con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@FirstName", st.FirstName);
                        com.Parameters.AddWithValue("@LastName", st.LastName);
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }


            st.FirstName = ""; st.LastName = "";
            
            

            successMessage = "has been added";


            Response.Redirect("/Employees/Index");
        }

    }
}
