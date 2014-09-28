using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC5.Controllers
{
    public class HomeController : BaseController
    {
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
        public ActionResult Test()
        {
            ViewBag.Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try{
                var meetingRep = new MeetingRepository();
                ViewBag.Message = meetingRep.Meetings.Count();
                
            }catch(Exception exp){
                ViewBag.Message = exp.Message;
            }

            ViewBag.Message += " \r\n "+ Request.ServerVariables["LOCAL_ADDR"];
            ViewBag.Message += " \r\n " + Environment.MachineName;
            ViewBag.Message += " \r\n " + Environment.UserDomainName.ToString();
            ViewBag.Message += " \r\n " + Environment.UserName;
            ViewBag.Message += " \r\n " + Environment.OSVersion.ToString();
            ViewBag.Message += " \r\n " + (Environment.TickCount / (1000 * 60 * 60)) + "Hours";
            ViewBag.Message += " \r\n " + DateTime.Now.ToLongDateString();
            ViewBag.Message += " \r\n " + Request.ServerVariables["SERVER_SOFTWARE"];
            ViewBag.Message += " \r\n " + Request.ServerVariables["HTTPS"];
            ViewBag.Message += " \r\n " + Request.ServerVariables["PATH_INFO"];
            ViewBag.Message += " \r\n " + Request.ServerVariables["PATH_TRANSLATED"];
            ViewBag.Message += " \r\n " + Request.ServerVariables["SERVER_PORT"];
            ViewBag.Message += " \r\n " + Session.SessionID;
            return View();
        }
        public ActionResult PingGator()
        {
            WebRequest webRequest = WebRequest.Create("http://eatwithme.agcy.co.il/main");
            // Get the response so that we don't leave this request hanging around
            WebResponse response = webRequest.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                ViewBag.Message += "\n" + responseString;
            }
            response.Close();

            return View();
        }
        public ActionResult PushThemAll()
        {

            ViewBag.Pushed = Service.Notify.PushThemAll();
            return View();
        }
    }
}