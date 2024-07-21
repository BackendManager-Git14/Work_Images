using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_Images.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Work_Images.Controllers
{
    public class HomeController : Controller

    {
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(data model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.imgfile.FileName);

            /*above statement will store the image name in the string called file name 
              by using system.io class method called "Path" see the txt doc*/

            string extension = Path.GetExtension(model.imgfile.FileName);

            /*to store the extension of the image file in string ex: jpg , png , jpeg etc*/

            filename = filename + DateTime.Now.ToString("dd-MM-yyyy-ss");
            string valid_filename = filename.Replace(" ", "_");
            valid_filename = valid_filename + extension;

            // filename would be : image07/11/2024/35.jpeg

            model.image_path = "~/Visual/" + valid_filename;
            valid_filename = Path.Combine(Server.MapPath("~/Visual/"), valid_filename);
            model.imgfile.SaveAs(valid_filename);

            con.Open();
            string qry = "insert into user_data values (@u_name,@b_date,@image_path,@email_add)";
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.Parameters.Add(new SqlParameter("@u_name", model.u_name));
            cmd.Parameters.Add(new SqlParameter("b_date", model.b_date));
            cmd.Parameters.Add(new SqlParameter("@image_path", model.image_path));
            cmd.Parameters.Add(new SqlParameter("@email_add", model.email_add));



            cmd.ExecuteNonQuery();
            con.Close();
            return View("Index");
        }

        public ActionResult Display(int id) 
        {
            con.Open();
            string qry = "select * from user_data where id = @id";
            SqlCommand cmd = new SqlCommand(qry,con);

            cmd.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader dr = cmd.ExecuteReader();

          
            dr.Read();
            data rec = new data
            {
                id = Convert.ToInt32(dr["id"]),
                u_name = dr["u_name"].ToString(),
                b_date = dr["b_date"].ToString(),
                image_path = dr["image_path"].ToString(),
                email_add = dr["email_add"].ToString()
            };

            dr.Close();
            con.Close();

            return View(rec);

        }

        

        //[HttpPost]
        //public ActionResult Download(string filepath)
        //{

        //    return View();
        //}
    }
}