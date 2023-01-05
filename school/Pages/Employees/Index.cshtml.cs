using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace school.Pages.Employees

{
    public class IndexModel : PageModel
    {
        public List<St_info> list=new List<St_info>();

        public void OnGet()
        {

            try
            {
                string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection conn=new SqlConnection(connectionstring))
                {
                    conn.Open();

                    string sql = "sp_select";

                    using (SqlCommand com=new SqlCommand(sql,conn))
                    {
                        using (SqlDataReader reader=com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                St_info st_Info = new St_info();

                                st_Info.EmployeeID = "" + reader.GetInt32 (0);

                                st_Info.FirstName =  reader.GetString(1);

                                st_Info.LastName = reader.GetString(2);



                            
                                list.Add(st_Info);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);   
            
            }

        }
    }


    public class St_info
    {
        public string EmployeeID;
        public string FirstName;
        public string LastName;
     
        
    }
}
