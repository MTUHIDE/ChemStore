using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ChemStoreWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        protected void createDb(object sender, EventArgs e)
        {
            try
            {
                String str = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(str))
                {
                    //String query = "select * from table1 where str1 like '" + maskedTextBox1.Text + "%'";
                    String query = "select * from chemical";
                    MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
                    con.Open();
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Console.WriteLine("yo");
                    //MessageBox.Show("connect with sql server");
                    //chemicalGrid.DataSource = ds;
                    //DataGrid1.DataBind();
                    //con.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
