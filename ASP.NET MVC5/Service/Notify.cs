using ASP.NET_MVC5.Models;
using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ASP.NET_MVC5.Service
{
    public class Notify
    {
        private static string GoogleAppID = "AIzaSyAqLQopxscmV8MLvqoPvCOlIKckyOoBo5E";
        public static String SendCommandToPhone(String message,String key, string registrationid)
        {
            if (registrationid == null)
            {
                return "Regid is empty";
            }
            String DeviceID = "";

            DeviceID = registrationid;

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/x-www-form-urlencoded";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            //String collaspeKey = Guid.NewGuid().ToString("n");
            String postData=string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, message, key);
            

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
        public static String SendCommandToPhone(String message,String key, List<String> registrationIds)
        {

            if (!registrationIds.Any())
            {
                return "Regid is empty";
            }

            String regIds = "";
            regIds += "\"" + registrationIds[0] + "\"";
            for (int i = 1; i < registrationIds.Count(); i++)
            {
                regIds += ",\"" + registrationIds[i]+"\"";
            }
            regIds = string.Join("\",\"", registrationIds);

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            String collaspeKey = Guid.NewGuid().ToString("n");
            String postData
                = "";
            postData = "{\""+
                "collapse_key\":\""+key+"\","+
                "\"time_to_live\":108,"+
                "\"delay_while_idle\":true,"+
                "\"data\":"+
                " { \"json\" : " + 
                    "\"" + message + "\""+
                    "},\"registration_ids\":[\"" + regIds + "\"]}";
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();
            // todo: Иногда возвращается новый Regid.
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }

        public static String PushThemAll()
        {
            var usersRep = new UserRepository();
            var seekers = usersRep.AspNetUsers.Where(x => x.registrationId != null);
            return SendCommandToPhone("{ }","test", seekers.Select(x => x.registrationId).ToList());

        }

        public static void NewMeeting(Models.Meeting meeting)
        {
            var usersRep = new UserRepository();
            var seekers = usersRep.AspNetUsers.Where(x => x.registrationId != null);
                SendCommandToPhone("{ }","meeting", seekers.Select(x=>x.registrationId).ToList());
            
        }

        public static void NewAccept(Models.MeetingAccept accept)
        {
            var owner = accept.Meeting.Owner;
            SendCommandToPhone("{ }","accept", owner.registrationId);
            //NewMeeting(null);
        }
        
        public static void Confirm(Models.MeetingAccept meetingAccept)
        {

            var accepter = meetingAccept.Acceptor;
            SendCommandToPhone("{ }","confirm", accepter.registrationId);
            //NewMeeting(null);
        }
    }
}