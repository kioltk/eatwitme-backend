using ASP.NET_MVC5.Api.Errors;
using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Mappers;
using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    public class UserController : SuperApiController
    {
        [HttpGet]
        public Response Get(string id)
        {
            var userRep = new UserRepository();
            var user = userRep.AspNetUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new BadRequestError("No such user");
            }
            var apiUser = CommonMapper.Map(user);
            return Response(apiUser);
        }
        [HttpGet]
        public Response List()
        {
            var userRep = new UserRepository();
            var userList = CommonMapper.Map(userRep.AspNetUsers);

            return Response(userList);
        }
        [HttpGet]
        public Response Meetings(string userid)
        {
            var userRep = new UserRepository();
            var user =  userRep.AspNetUsers.FirstOrDefault(x=>x.Id == userid);
            if(user==null){
                return new BadRequestError("No such user");
            }
            var apiUser = CommonMapper.Map(user);
            return Response(apiUser);
        }
    }
}
