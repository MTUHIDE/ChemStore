using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ChemStoreWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ChemStoreWebApp.Models.chemstoreContext _context;

        public IndexModel(ChemStoreWebApp.Models.chemstoreContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string chemName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string chemCAS { get; set; }
        [BindProperty(SupportsGet = true)]
        public string location { get; set; }
        [BindProperty(SupportsGet = true)]
        public string numLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string size { get; set; }
        [BindProperty(SupportsGet = true)]
        public string units { get; set; }

        public void OnGet()
        {
        
        }
        public IActionResult OnPost()
        {
            return new RedirectToPageResult("Search", new {
                searchEmail = email,
                searchCAS = chemCAS, 
                searchString = chemName,
                searchLocation = location,
                searchNumLocation = numLocation,
                searchSize = size,
                searchUnits = units
            });
            
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
