using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        DBNoiThat db= new DBNoiThat();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Contacts()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Contacts(EmailModel obj)
        {
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "sam.deeplearning123@gmail.com";
                WebMail.Password = "dangvansam98";

                //Sender email address.  
                WebMail.From = "SenderGamilId@gmail.com";

                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, bcc: obj.EmailBCC, isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
                var model = new Contact();
                model.Content = obj.EMailBody;
                model.Status = true;
                model.EmailCC = obj.EmailCC;
                db.Contacts.Add(model);
                db.SaveChanges();

                
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return View();
            
        }
    }
}