

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
//using UAParser;

namespace CorePractice.Models
{



    public class UserLogingInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserLogingInfoId { get; set; }


        [Required]
        [StringLengthAttribute(128)]
        public string userid { get; set; }


        [StringLength(300)]
        [Display(Name = "Web Link")]
        public string WebLink { get; set; }



        //[Required]
        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LoginDate { get; set; }

        public DateTime? LoginTime { get; set; }

        [StringLength(50)]

        public string PcName { get; set; }
        [StringLength(30)]

        public string MacAddress { get; set; }
        [StringLength(128)]


        public string IPAddress { get; set; }
        [StringLength(50)]


        public string DeviceType { get; set; }
        [StringLength(128)]


        public string Platform { get; set; }
        [StringLength(128)]

        public string WebBrowserName { get; set; }
        [StringLength(128)]

        public string comid { get; set; }

        [StringLength(128)]

        public string Latitude { get; set; }
        [StringLength(128)]

        public string Longitude { get; set; }


    }

    public class UserTransactionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTransactionLogId { get; set; }


        [Required]
        [StringLength(128)]
        public string userid { get; set; }


        [StringLength(300)]
        [Display(Name = "Web Link")]
        public string WebLink { get; set; }


        [StringLength(300)]
        [Display(Name = "Transaction Statement")]
        public string TransactionStatement { get; set; }
        [StringLength(60)]

        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }
        [StringLength(100)]

        [Display(Name = "Action")]
        public string ActionName { get; set; }
        [StringLength(300)]

        [Display(Name = "DocumentReferance")]
        public string DocumentReferance { get; set; }
        [StringLength(300)]

        public string CommandType { get; set; }

        [StringLength(200)]
        public string PcName { get; set; }

        //[Required]
        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDateTime { get; set; }

        public DateTime? ToDateTime { get; set; }
        [StringLength(100)]

        public string FlagValue { get; set; }

        [StringLength(100)]
        public string IPAddress { get; set; }

        [StringLength(128)]
        public string comid { get; set; }
    }

    //public class UserLog
    //{
    //    private GTRDBContext db { get; set; }
    //    //private UserLogingInfo userLoging { get; set; }
    //    //private UserTransactionLog userTranLog { get; set; }
    //    public IHttpContextAccessor Accessor { get; }

    //    //public object Request { get => request; set => request = value; }

    //    // private AuthorizationFilterContext filterContext;


    //    public UserLog(GTRDBContext _db, IHttpContextAccessor accessor)
    //    {
    //        db = _db;
    //        Accessor = accessor;
    //    }


    //    public void errorlog(Exception ex)
    //    {
    //        string filePath = @"F:\DAP_Erp_Error.txt";


    //        using (StreamWriter writer = new StreamWriter(filePath, true))
    //        {
    //            writer.WriteLine("-----------------------------------------------------------------------------");
    //            writer.WriteLine("Date : " + DateTime.Now.ToString());
    //            writer.WriteLine();

    //            while (ex != null)
    //            {
    //                writer.WriteLine(ex.GetType().FullName);
    //                writer.WriteLine("Message : " + ex.Message);
    //                writer.WriteLine("StackTrace : " + ex.StackTrace);

    //                ex = ex.InnerException;
    //            }
    //        }
    //    }

    //    public void SuccessLogin(AuthorizationFilterContext filterContext)
    //    {
    //        try
    //        {

    //            var userAgent = filterContext.HttpContext.Request.Headers["User-Agent"];


    //            var devicetype = "";
    //            if (userAgent == true)
    //            {
    //                devicetype = "Mobile";
    //            }
    //            else
    //            {
    //                devicetype = "Computer";
    //            }
    //            //string IP = request.UserHostName;


    //            string userIpAddress = filterContext.HttpContext.Request.Headers["REMOTE_ADDR"];
    //            //string userIpAddress = request.UserHostAddress;
    //            var userLoging = new UserLogingInfo();
    //            userLoging.userid = filterContext.HttpContext.Session.GetString("userid");
    //            //userLoging.comid = int.Parse(filterContext.HttpContext.Session.GetString("comid"));

    //            userLoging.LoginDate = DateTime.Now.Date;
    //            userLoging.LoginTime = DateTime.Now;
    //            userLoging.DeviceType = devicetype;
    //            userLoging.IPAddress = userIpAddress;
    //            //userLoging.Platform = request.Browser.Platform;
    //            //userLoging.Platform = "";// GetUserEnvironment(request);

    //            userLoging.PcName = "";// DetermineCompName(request.UserHostName);
    //            //userLoging.WebBrowserName = request.Browser.Browser;
    //            //userLoging.MacAddress = fnGetClientIP(request);//request.UserHostAddress;
    //            //userLoging.WebLink = request.Url.AbsoluteUri;


    //            db.UserLogingInfos.Add(userLoging);
    //            db.SaveChangesAsync();

    //        }
    //        catch (Exception ex)
    //        {
    //            errorlog(ex);
    //            throw ex;
    //        }
    //    }

        
    //    public String GetUserEnvironment()
    //    {

    //        var userAgent = Accessor.HttpContext.Request.Headers["User-Agent"];

    //        string uaString = Convert.ToString(userAgent[0]);

    //        var uaParser = Parser.GetDefault();

    //        ClientInfo c = uaParser.Parse(uaString);

    //        var browsr = c.UserAgent.Family; //IP Address from the request. 




    //        var platform = GetUserPlatform(Accessor.HttpContext.Request);
    //        return $"{ browsr},{ platform}";
    //    }

    //    public String GetUserPlatform(HttpRequest request)
    //    {

    //        var userAgent = request.Headers["User-Agent"];

    //        string uaString = Convert.ToString(userAgent[0]);

    //        var uaParser = Parser.GetDefault();

    //        ClientInfo c = uaParser.Parse(uaString);
    //        var ua = c.UserAgent.Family;

    //        if (ua.Contains("Android"))
    //            return string.Format("Android {0}", GetMobileVersion(ua, "Android"));

    //        if (ua.Contains("iPad"))
    //            return string.Format("iPad OS {0}", GetMobileVersion(ua, "OS"));

    //        if (ua.Contains("iPhone"))
    //            return string.Format("iPhone OS {0}", GetMobileVersion(ua, "OS"));

    //        if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
    //            return "Kindle Fire";

    //        if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
    //            return "Black Berry";

    //        if (ua.Contains("Windows Phone"))
    //            return string.Format("Windows Phone {0}", GetMobileVersion(ua, "Windows Phone"));

    //        if (ua.Contains("Mac OS"))
    //            return "Mac OS";

    //        if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
    //            return "Windows XP";

    //        if (ua.Contains("Windows NT 6.0"))
    //            return "Windows Vista";

    //        if (ua.Contains("Windows NT 6.1"))
    //            return "Windows 7";

    //        if (ua.Contains("Windows NT 6.2"))
    //            return "Windows 8";

    //        if (ua.Contains("Windows NT 6.3"))
    //            return "Windows 8.1";

    //        if (ua.Contains("Windows NT 10"))
    //            return "Windows 10";

    //        //fallback to basic platform:
    //        return c.UserAgent.Family + (ua.Contains("Mobile") ? " Mobile " : "");
    //    }

    //    public String GetMobileVersion(string userAgent, string device)
    //    {
    //        var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
    //        var version = string.Empty;

    //        foreach (var character in temp)
    //        {
    //            var validCharacter = false;
    //            int test = 0;

    //            if (Int32.TryParse(character.ToString(), out test))
    //            {
    //                version += character;
    //                validCharacter = true;
    //            }

    //            if (character == '.' || character == '_')
    //            {
    //                version += '.';
    //                validCharacter = true;
    //            }

    //            if (validCharacter == false)
    //                break;
    //        }

    //        return version;
    //    }


    //    public string fnGetClientIP()
    //    {

    //        // Note in localhost you will get ::1 but when it hosted you will get IP Address

    //        var ip = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
    //        string ClientIP = string.Empty;
    //        if (!string.IsNullOrEmpty(ip))
    //        {
    //            string[] ipRange = ip.Split(',');
    //            int le = ipRange.Length - 1;
    //            ClientIP = ipRange[0];
    //        }
    //        else
    //        {
    //            ClientIP = ip;
    //        }

    //        return ClientIP;
    //    }


    //    public static string DetermineCompName(string IP)
    //    {
    //        if (IP.Length > 6)
    //        {
    //            IPAddress myIP = IPAddress.Parse(IP);
    //            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
    //            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
    //            return compName.First();
    //        }
    //        else
    //        {
    //            return "Not Found";
    //        }

    //    }

    //    public void TransactionLog(string Controller, string Action, string TransactionStatement, string TransactionalId, string commandType, string comid)
    //    {

    //        try
    //        {


    //            var userAgent = Accessor.HttpContext.Request.Headers["User-Agent"];

    //            string uaString = Convert.ToString(userAgent[0]);

    //            var uaParser = Parser.GetDefault();

    //            ClientInfo c = uaParser.Parse(uaString);
    //            var devicetype = c.Device.Family;


    //            string userIpAddress = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();

    //            var userTranLog = new UserTransactionLog();
    //            userTranLog.userid = Accessor.HttpContext.Session.GetString("userid");
    //            //userLoging.userid = Accessor.HttpContext.Session.GetString("userid");

    //            userTranLog.WebLink = Accessor.HttpContext.Request.Headers["Url"];// .AbsolutePath;
    //            userTranLog.TransactionStatement = TransactionStatement;
    //            userTranLog.CommandType = commandType;
    //            userTranLog.FromDateTime = DateTime.Now;
    //            userTranLog.ToDateTime = DateTime.Now;
    //            //userTranLog.IPAddress = userIpAddress;//request.UserHostName;// request.UserHostAddress;
    //            userTranLog.comid = comid;//HttpContext.Session.GetString("comid");
    //            userTranLog.ControllerName = Controller;
    //            userTranLog.ActionName = Action;
    //            userTranLog.FlagValue = TransactionalId;

    //            userTranLog.PcName = "";// DetermineCompName(request.UserHostName);




    //            db.UserTransactionLogs.Add(userTranLog);
    //            db.SaveChangesAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            errorlog(ex);
    //            Console.WriteLine(ex.Message);
    //        }

    //    }

    //}


}