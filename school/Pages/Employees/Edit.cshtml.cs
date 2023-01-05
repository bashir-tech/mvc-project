using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace school.Pages.Employees
{
    public class EditModel : PageModel
    {

        public St_info st = new St_info();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string EmployeeID = Request.Query["EmployeeID"];
            try
            {

                string connectionstring = "Data Source=BASHIR\\SQLEXPRESS;" +
                    "Initial Catalog=Northwind;Integrated Security=True;" +
                    "Connect Timeout=30;Encrypt=False;" +
                    "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();

                    string sql = "SELECT EmployeeID, FirstName,LastName FROM Employees WHERE EmployeeID=@EmployeeID";

                    using (SqlCommand comm = new SqlCommand(sql, conn))
                    {

                        comm.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               
                                st.EmployeeID = "" + reader.GetInt32(0);

                                st.FirstName = reader.GetString(1);

                                st.LastName = reader.GetString(2);




                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }







        }




        public void Onpost()
        {
            st.EmployeeID = Request.Form["EmployeeID"];

            st.FirstName = Request.Form["FirstName"];
            st.LastName = Request.Form["LastName"];



            if (st.FirstName.Length == 0 || st.LastName.Length == 0)


            {
                errorMessage = "all the fields are required";
                return;
            }
            // save date base
            try
            {
                string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;" +
                    "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
                    "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
                using (SqlConnection conn = new SqlConnection(connectionstring))

                {
                    conn.Open();

                    string sql = "UPDATE Employees SET FirstName=@FirstName,LastName=@LastName WHERE EmployeeID=@EmployeeID";

                    using (SqlCommand com = new SqlCommand(sql, conn))
                    {

                   
                        com.Parameters.AddWithValue("@FirstName", st.FirstName);
                        com.Parameters.AddWithValue("@LastName", st.LastName);

                        com.Parameters.AddWithValue("@EmployeeID", st.EmployeeID);


                        com.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;

            }


            Response.Redirect("/Employees/Index");

        }


    }


}
