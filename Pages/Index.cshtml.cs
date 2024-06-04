using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1GUID.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=personDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly ILogger<IndexModel> _logger;

        public DataSet Persons;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string sqlQuery = "select * from Person";

            SqlConnection SqlConnectioncon = new SqlConnection(_connectionString);
            SqlConnectioncon.Open();
            SqlCommand sc = new SqlCommand(sqlQuery, SqlConnectioncon);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sc);

            Persons = new DataSet();

            dataAdapter.Fill(Persons);

            SqlConnectioncon.Close();

            //var _data = Persons.Tables[0].Rows[0]["FirstName"];
        }

        public void OnPost()
        {
            var ssss = Request.Form;
            string firstName = Request.Form["FirstName"];
            string lastName = Request.Form["LastName"];
            string PhoneNumber = Request.Form["PhoneNumber"];
            string Address = Request.Form["Address"];
            string PersonID = Request.Form["PersoneID"];
            bool deletePersone = false;
            var DeletePersone = bool.TryParse(Request.Form["DeletePersone"], out deletePersone);

            SqlConnection SqlConnectioncon = new SqlConnection(_connectionString);
            SqlConnectioncon.Open();

            if (DeletePersone)
            {
                string sqlQueryDelete = $"DELETE FROM dbo.Person WHERE ID = {PersonID}";
                SqlCommand scDelete = new SqlCommand(sqlQueryDelete, SqlConnectioncon);
                scDelete.ExecuteNonQuery();
            }
            else
            {
                string sqlQuery = $"INSERT INTO dbo.Person(FirstName,LastName,PhoneNumber,Address) VALUES('{firstName}','{lastName}',{PhoneNumber},'{Address}')";
                SqlCommand sc = new SqlCommand(sqlQuery, SqlConnectioncon);
                sc.ExecuteNonQuery();
            }
            
            SqlConnectioncon.Close();

            OnGet();
        }
    }
}
