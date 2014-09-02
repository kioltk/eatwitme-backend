using ASP.NET_MVC5.Mappers;
using ASP.NET_MVC5.Models;
using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ASP.NET_MVC5.Models;
using ASP.NET_MVC5.Service;

namespace ASP.NET_MVC5.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            var meetingRep = new MeetingRepository();
            return View(meetingRep.Meetings);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MeetingViewModel meetingModel)
        {
            if (ModelState.IsValid)
            {
                int id = -1;
                var userId = User.Identity.GetUserId();
                var meeting =  CommonMapper.Map(meetingModel);
                meeting.creator = userId;
                var meetingRep = new MeetingRepository();
                
                id = meetingRep.Create(meeting);
                if (id > -1)
                {
                    Notify.NewMeeting(meeting);
                    return RedirectToAction("Get", new { id = id });
                }
                throw new Exception();
            }
            else 
                return View(meetingModel);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var meeting = new MeetingRepository().Meetings.First(x=>x.Id == id);
            meeting.checkPermissions(User.Identity.GetUserId());
            return View(meeting);
        }

        [HttpPost]
        public ActionResult Accept(MeetingAccept accept)
        {

            var meetingRep = new MeetingRepository();
            var meeting = meetingRep.Meetings.FirstOrDefault(x => x.Id == accept.meetingId);
            if (meeting == null)
            {
                throw new Exception();
            }
            if (meeting.confirmer != null)
            {
                throw new Exception();
            }

            int id = meetingRep.Create(accept);
            if (id > 0)
            {

                Notify.NewAccept(accept);
                return View(true);
            }

            throw new Exception();
        }
        [HttpPost]
        public ActionResult Confirm(int id)
        {
            var meetingRep = new MeetingRepository();

            var meetingAccept = meetingRep.MeetingAccepts.FirstOrDefault(x => x.Id == id);

            var meeting = meetingRep.Meetings.FirstOrDefault(x => x.Id == meetingAccept.meetingId);

            if (meeting == null || meetingAccept == null)
            {
                throw new Exception();
            }
            meeting.checkPermissions(User.Identity.GetUserId());

            if (meeting.isOwner)
            {
                meeting.confirmer = meetingAccept.acceptorId;
                meetingRep.Update(meeting);

            }
            else
            {
                throw new Exception();
            }

                Notify.Confirm(meetingAccept);
            return PartialView();
        }
        [AllowAnonymous]
        public ActionResult History(string userid="")
        {

            return View();
        }
    }
}