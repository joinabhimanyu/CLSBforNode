using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CLSBforNode
{
    class SampleLib
    {

        public async Task<object> Invoke(object input)
        {
            // Edge marshalls data to .NET using an IDictionary<string, object>
            var payload = (IDictionary<string, object>)input;
            //var pageNumber = (int)payload["pageNumber"];
            //var pageSize = (int)payload["pageSize"];
            //return await GetWindows();
            return await GetData();
        }

        public async Task<List<WindowsDtls>> GetWindows()
        {
            var constring = @"Data Source='c:\users\u3696174\documents\visual studio 2012\Projects\CLSBforNode\CLSBforNode\App_Data\Database1.sdf'";
            List<WindowsDtls> lst_result = new List<WindowsDtls>();
            using (SqlConnection con=new SqlConnection(constring))
            {
                await con.OpenAsync();
                using (SqlCommand cmd=new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"select * from Windows";
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    adapter.Dispose();
                    con.Close();
                   
                    WindowsDtls obj_single = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        obj_single = new WindowsDtls();
                        obj_single.ID = Convert.ToInt32( dr[0].ToString());
                        obj_single.Name = dr[1].ToString();
                        obj_single.Quantity = dr[2].ToString();
                        obj_single.Price = dr[3].ToString();
                        obj_single.Image = dr[4].ToString();
                        lst_result.Add(obj_single);
                        obj_single = null;
                    }
                }
                
            }
            return lst_result;
        }

        public async Task<object> GetData()
        {
            return await GetWindows();
            //return "Hello World";
        }

    }
}
