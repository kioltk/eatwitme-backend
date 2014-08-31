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
        public static String SendCommandToPhone(String message, string registrationid)
        {
            String DeviceID = "";

            DeviceID = registrationid;

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/x-www-form-urlencoded";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            String collaspeKey = Guid.NewGuid().ToString("n");
            //String postData=string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, "Pickup Message", collaspeKey);
            String postData = string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, message, collaspeKey);

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

        public static void NewMeeting(Models.Meeting meeting)
        {
            var usersRep = new UserRepository();
            var seekers = usersRep.AspNetUsers.Where(x => x.registrationId != null);
            foreach(var seeker in seekers){
                SendCommandToPhone("new meeting", seeker.registrationId);
            }
        }

        public static void NewAccept(Models.MeetingAccept accept)
        {
            var owner = accept.Meeting.AspNetUser;
            SendCommandToPhone("new accept", owner.registrationId);
        }
        
        public static void Confirm(Models.MeetingAccept meetingAccept)
        {

            var accepter = meetingAccept.AspNetUser;
            SendCommandToPhone("confirmed", accepter.registrationId);
        }
    }
}