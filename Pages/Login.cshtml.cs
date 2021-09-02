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
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet=true)]
        public string  errMsg { get; set; }
        [BindProperty]
        public User user { get; set; }
        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {

            string Hashed = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-","");

            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IRobot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = "SELECT * FROM [IRobot].[dbo].[Users] WHERE email='" + user.email + "' AND password= '" + Hashed+ "'";
            SqlCommand cmdRead = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmdRead.ExecuteReader();

            if (reader.Read())
            {
                user.fname = string.Format("{0}", reader[1]);// first user will come from the query wit hthe second index
                user.lname = string.Format("{0}", reader[2]);//first user and third index 

                return RedirectToPage("/Profile", new { user.fname, user.lname, user.email});

            }
            else
            {
                errMsg = "User is already exist!";
                return RedirectToPage("/Signup", new { errMsg });

            }



        }
    }
}