using CorePractice.Data;
using CorePractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace CorePractice.Services
{
    public class TransactionLog : Attribute, IAuthorizationFilter
    {
        public IHttpContextAccessor Accessor { get; set; }
        public RouteData RouteData { get; }
        public ApplicationDbContext db { get; set; }
        public TransactionLog()
        {

        }
        public TransactionLog(ApplicationDbContext _db, IHttpContextAccessor httpContextAccessor,RouteData routeData)
        {
            db = _db;
            Accessor = httpContextAccessor;
            RouteData = routeData;
            
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            try
            {


                var userAgent = Accessor.HttpContext.Request.Headers["User-Agent"];

                string uaString = Convert.ToString(userAgent[0]);

                var uaParser = Parser.GetDefault();

               ClientInfo c = uaParser.Parse(uaString);
                var devicetype = c.Device.Family;
                string userId = context.HttpContext.Session.GetString("userid");

                string userIpAddress = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var userTranLog = new UserTransactionLog();
                userTranLog.userid = Accessor.HttpContext.Session.GetString("userid");
                //userLoging.userid = Accessor.HttpContext.Session.GetString("userid");
                var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                string controller = controllerActionDescriptor?.ControllerName;
                //string CommandType = controllerActionDescriptor?.;
                //var action = filterContext.ActionDescriptor;
                var action = controllerActionDescriptor?.ActionName;
                userTranLog.WebLink = Accessor.HttpContext.Request.Headers["Url"];// .AbsolutePath;
                //userTranLog.TransactionStatement = TransactionStatement;
                //userTranLog.CommandType = commandType;
                userTranLog.FromDateTime = DateTime.Now;
                userTranLog.ToDateTime = DateTime.Now;
                userTranLog.IPAddress = userIpAddress;//request.UserHostName;// request.UserHostAddress;
                userTranLog.comid = Accessor.HttpContext.Session.GetString("comid");
                userTranLog.ControllerName = controller;
                userTranLog.ActionName = action;
                //userTranLog.FlagValue = TransactionalId;

                userTranLog.PcName = Environment.MachineName;


                db.Entry(userTranLog).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                //db.UserTransactionLogs.Add(userTranLog);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //errorlog(ex);
                Console.WriteLine(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}
