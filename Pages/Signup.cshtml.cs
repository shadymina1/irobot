using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using irobot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace irobot.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty]

        public User user { get; set; }
        [BindProperty(SupportsGet = true)]
        public string notAllowd { get; set; }
        [BindProperty(SupportsGet = true)]

        public string errMsg { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {


            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IRobot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = "SELECT * FROM [IRobot].[dbo].[Users] WHERE email='"+ user.email + "'";
            SqlCommand cmdRead = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmdRead.ExecuteReader();

            if (reader.Read())
            {
                 errMsg = "User is already exist!";
                return RedirectToPage("/Signup", new { errMsg });
            }
            else
            {
            con.Close();
            string Hashed = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
            string InsertQuery = "INSERT INTO [IRobot].[dbo].[Users]([FirstName],[LastName],[Email],[Password]) VALUES ( '" +  user.fname + " ' , ' " + user.lname +"','"+ user.email + "','" + Hashed + "') ";
            SqlCommand cmd = new SqlCommand(InsertQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Profile", new { user.fname, user.lname, user.email});
            }
           
        }
    }
}
