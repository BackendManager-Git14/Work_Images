using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Configuration;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Data;
using System.Net.Mail;

namespace Work_Images.Models
{
    public class script
    {
        static void Main(string [] args)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
            string date_today = DateTime.Now.ToString("yyyy-MM-dd");

            con.Open();

            string qry = "select * from user_data where b_date = @date_today";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.Add(new SqlParameter("@date_today", date_today));

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            data data = new data
            {
                id = Convert.ToInt32(dr["id"]),
                u_name = dr["u_name"].ToString(),
                b_date = dr["b_date"].ToString(),
                image_path = dr["image_path"].ToString(),
                email_add = dr["email_add"].ToString()
            };

            if(date_today == data.b_date)
            {
                string from_email_addr = "aksharpp2910@gmail.com"; //input sender's gmail add 
                string p_num = "yntf tbup vehl hnar"; //input pswd here 
                string url = "https://raw.githubusercontent.com/backendmanager-git14/web-work/main/visual%20resources/";
                string filename = "abtbg.jpg";
                string source = url + filename;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from_email_addr);
                mailMessage.Subject = "Birthday Post";
                mailMessage.To.Add(new MailAddress(data.email_add));
                mailMessage.Body = $@"
                                          <html>
                                            <body> 
                                                your are hack <br>
                                                <img src='{source}' height='200' width='200' />
                                            </body>
                                       </html>
                                       ";
                mailMessage.IsBodyHtml = true;
                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from_email_addr, p_num),
                    EnableSsl = true
                };
                smtpclient.Send(mailMessage);


            }
        }
    }
}

/*
  mailmessage mailmessage = new mailmessage();
                mailmessage.from = new mailaddress(from_email_addr);
                mailmessage.subject = "idk";
                mailmessage.to.add(new mailaddress("misushubham@gmail.com"));
                mailmessage.body = $@"
                                          <html>
                                            <body> 
                                                your are hack <br>
                                                <img src='{source}' height='200' width='200' />
                                            </body>
                                       </html>
                                       ";
                mailmessage.isbodyhtml = true;

                var smtpclient = new smtpclient("smtp.gmail.com")
                {
                    port = 587,
                    usedefaultcredentials = false,
                    credentials = new networkcredential(from_email_addr, p_num),
                    enablessl = true,
                };
                smtpclient.send(mailmessage);
 */