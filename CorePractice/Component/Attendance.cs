using GTERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using GTERP.Models.Common;
using Microsoft.AspNetCore.Mvc;
using CorePractice.Data;

namespace GTERP.BLL
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        private  IHttpContextAccessor _httpContextAccessor;
        

        public AttendanceRepository(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        //public  List<HR_AttFixed> PrcGetEmployeeInfo(string comid)
        //{
        //    var AllEmployee = _context.HR_AttFixed.Where(e => e.ComId == comid).ToList();
        //    return AllEmployee;
        //}

        //public List<MenuPermission_Details> PrcGetUserMenuPermission(string comid)
        //{
        //    var MenuPermission_Details = _context.MenuPermissionDetails.Include(x => x.MenuPermissionMasters).Where(e => e.MenuPermissionMasters.comid == comid).ToList();
        //    return MenuPermission_Details;
        //}
    }
}
