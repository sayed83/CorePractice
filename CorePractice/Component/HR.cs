using CorePractice.Data;
using GTERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorePractice.Component
{
    public class HrRepository
    {
        private ApplicationDbContext _context;

        public HrRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //public Task<List<HR_Emp_Info>> GetEmpAsync()
        //{
        //    var gTRDBContext = _context.HR_Emp_Info.Include(h => h.Cat_BloodGroup).Include(h => h.Cat_Skill).Include(h => h.Cat_Department).Include(h => h.Cat_Designation).Include(h => h.Cat_Floor).Include(h => h.Cat_Grade).Include(h => h.Cat_Line).Include(h => h.Cat_Religion).Include(h => h.Cat_Section).Include(h => h.Cat_Shift).Include(h => h.Cat_SubSection).Include(h => h.Cat_Unit);
        //    return  gTRDBContext.ToListAsync();
        //}
    }
}
