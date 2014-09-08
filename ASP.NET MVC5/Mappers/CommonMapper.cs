using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Mappers
{
    public class CommonMapper : IMapper
    {
        static CommonMapper()
        {

            Mapper.CreateMap<MeetingViewModel, Meeting>();

            Mapper.CreateMap<Meeting, MeetingApiModel>();
            Mapper.CreateMap<MeetingAccept, MeetingAcceptApiModel>();
            Mapper.CreateMap<AspNetUser, UserApiModel>();
            Mapper.CreateMap<ApplicationUser, UserApiModel>();

        }
        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }

        public static Meeting Map(MeetingViewModel viewModel)
        {
            return (Meeting)new CommonMapper().Map(viewModel, typeof(MeetingViewModel), typeof(Meeting));
        }

        public static MeetingApiModel Map(Meeting model)
        {
            return new MeetingApiModel()
            {
                id = model.Id.ToString(),
                description = model.description,
                latitude = model.latitude,
                longitude = model.longitude,
                time = model.time,
                owner = new UserApiModel()
                {
                    id = model.Owner.Id,
                    username = model.Owner.UserName,
                    photo = model.Owner.photo
                },
                accept = model.IsAccepter? new MeetingAcceptApiModel(){
                    confirmed = model.CurrentUserAccept.confirmed,
                    id = model.CurrentUserAccept.Id.ToString(),
                    meetingId = model.CurrentUserAccept.meetingId.ToString(),
                    message = model.CurrentUserAccept.message,
                    time = model.CurrentUserAccept.time
                }: null,
                acceptsCount = model.MeetingAccepts.Count,
                confirmer = model.confirmer == null ? null : new UserApiModel()
                {
                    id = model.Confirmer.Id,
                    username = model.Confirmer.UserName,
                    photo = model.Confirmer.photo
                }
            };
        }
        public static MeetingAcceptApiModel Map(MeetingAccept accept)
        {
            return new MeetingAcceptApiModel()
            {
                id = accept.Id.ToString(),
                acceptor = new UserApiModel()
                {
                    id = accept.AspNetUser.Id,
                    username = accept.AspNetUser.UserName,
                    photo = accept.AspNetUser.photo
                },
                confirmed = accept.confirmed,
                meetingId = accept.meetingId.ToString(),
                message = accept.message,
                time = accept.time
            };
        }
        public static UserApiModel Map(AspNetUser user)
        {
            return (UserApiModel) new CommonMapper().Map(user, typeof(AspNetUser), typeof(UserApiModel));
        }
        public static UserApiModel Map(ApplicationUser user)
        {
            return (UserApiModel)new CommonMapper().Map(user, typeof(ApplicationUser), typeof(UserApiModel));
        }
        public static IQueryable<MeetingApiModel> Map(IQueryable<Meeting> meetings)
        {
            var apiMeetings = new List<MeetingApiModel>();
            foreach (var meeting in meetings)
            {
                apiMeetings.Add(Map(meeting));
            }
            return apiMeetings.AsQueryable();

        }
        public static IQueryable<MeetingAcceptApiModel> Map(IQueryable<MeetingAccept> meetingAccepts)
        {
            var apiMeetingAccepts = new List<MeetingAcceptApiModel>();
            foreach (var meetingAccept in meetingAccepts)
            {
                apiMeetingAccepts.Add(Map(meetingAccept));
            }
            return apiMeetingAccepts.AsQueryable();
        }
        public static IQueryable<UserApiModel> Map(IQueryable<AspNetUser> userList)
        {
            var apiUserList = new List<UserApiModel>();
            foreach (var apiUser in userList)
            {
                apiUserList.Add(Map(apiUser));
            }
            return apiUserList.AsQueryable();
        }


        
    }
}