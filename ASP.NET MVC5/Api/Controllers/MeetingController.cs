using ASP.NET_MVC5.Api.Errors;
using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Mappers;
using ASP.NET_MVC5.Models;
using ASP.NET_MVC5.Repositories;
using ASP.NET_MVC5.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    [ApiAuthorize]
    public class MeetingController : SuperApiController
    {
        // GET: api/Meeting
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Meeting/5
        [AllowAnonymous]
        public Response Get(int id)
        {
            var meeting = new MeetingRepository().Meetings.First(x => x.Id == id);
            meeting.checkPermissions(GetUserId());
            var meetingApi = Mappers.CommonMapper.Map(meeting);
            return Response(meetingApi);
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("acceptslist")]
        public Response AcceptsList(int id)
        {
            var meetingRep = new MeetingRepository();
            var meeting = meetingRep.Meetings.FirstOrDefault(x=> x.Id == id);
            if(meeting ==null){
                return new BadRequestError("");
            }
            meeting.checkPermissions(GetUserId());
            if(!meeting.isOwner){
                return new UnauthorizedError("Not owner");
            }
            var acceptsList = CommonMapper.Map(meeting.MeetingAccepts.AsQueryable());
            return Response(acceptsList);
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("list")]
        public Response List()
        {
            var meetingRep = new MeetingRepository();
            var meetings = CommonMapper.Map(meetingRep.Meetings);
            return Response(meetings);
        }

        [HttpPost]
        [ActionName("create")]
        public Response Create(float latitude, float longitude, int time, string description)
        {
            var meeting = new Meeting()
            {
                latitude = latitude.ToString(),
                longitude = longitude.ToString(),
                time = time,
                description = description
            };
            if (ModelState.IsValid)
            {
                int id = -1;
                var userId = GetUserId();
                meeting.creator = userId;
                var meetingRep = new MeetingRepository();

                id = meetingRep.Create(meeting);
                if (id > -1)
                {
                    Notify.NewMeeting(meeting);
                    return Response(new { id = id });
                }
            }
            else
            {
                return new BadRequestError();
            }
            return new InternalError();
        }


        [HttpPost]
        [ActionName("accept")]
        public Response Accept(int id, string message)
        {
            var accept = new MeetingAccept() { meetingId = id, acceptorId = GetUserId(), message = message };
            var meetingRep = new MeetingRepository();
            var meeting = meetingRep.Meetings.FirstOrDefault(x => x.Id == accept.meetingId);
            if (meeting == null)
            {
                return new BadRequestError("No such meeting");
            }
            meeting.checkPermissions(GetUserId());
            if (meeting.isOwner)
            {
                return new UnauthorizedError("Not for owner");
            }
            if (meeting.isAccepter)
            {
                return new BadRequestError("Already accepted");
            }
            if (meeting.confirmer != null)
            {
                return new BadRequestError("Already confirmed");
            }
            

            int acceptId = meetingRep.Create(accept);
            if (acceptId > 0)
            {
                Notify.NewAccept(accept);
                return Response(acceptId);
            }

            return new InternalError();
        }

        [HttpPost]
        [ActionName("confirm")]
        public Response Confirm(int id)
        {
            var meetingRep = new MeetingRepository();

            var meetingAccept = meetingRep.MeetingAccepts.FirstOrDefault(x => x.Id == id);

            if (meetingAccept == null)
            {
                return new BadRequestError("No such accept");
            }
            var meeting = meetingRep.Meetings.FirstOrDefault(x => x.Id == meetingAccept.meetingId);

            if (meeting == null )
            {
                return new InternalError("No such meeting");
            }
            meeting.checkPermissions(GetUserId());

            if (meeting.isOwner)
            {
                meeting.confirmer = meetingAccept.acceptorId;
                meetingRep.Update(meeting);
            }
            else
            {
                return new UnauthorizedError("Not owner");
            }

            Notify.Confirm(meetingAccept);
            return Response(true);
            // todo: return 
        }
    }
}
