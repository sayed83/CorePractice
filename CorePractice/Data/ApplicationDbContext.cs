using System;
using System.Collections.Generic;
using System.Text;
using CorePractice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorePractice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleGroup> ModuleGroups { get; set; }
        public virtual DbSet<ImageCriteria> ImageCriterias { get; set; }
        public virtual DbSet<MenuPermission_Master> MenuPermission_Masters { get; set; }
        public virtual DbSet<MenuPermission_Details> MenuPermission_Details { get; set; }

        public virtual DbSet<Cat_Department> Cat_Department { get; set; }
        public virtual DbSet<Cat_Section> Cat_Section { get; set; }
        public virtual DbSet<Cat_SubSection> Cat_SubSection { get; set; }
        public virtual DbSet<Cat_Designation> Cat_Designation { get; set; }
        public DbSet<CorePractice.Models.ModuleMenu> ModuleMenu { get; set; }

        //public virtual DbSet<Cat_Religion> Cat_Religion { get; set; }
        //public virtual DbSet<Cat_BloodGroup> Cat_BloodGroup { get; set; }
        //public virtual DbSet<Cat_Grade> Cat_Grade { get; set; }
        //public virtual DbSet<Cat_Line> Cat_Line { get; set; }

        //public virtual DbSet<Cat_Shift> Cat_Shift { get; set; }
        //public virtual DbSet<Cat_Floor> Cat_Floor { get; set; }
        //public virtual DbSet<Cat_Unit> Cat_Unit { get; set; }
        //public virtual DbSet<Cat_District> Cat_District { get; set; }

        //public virtual DbSet<Cat_BusStop> Cat_BusStop { get; set; }
        //public virtual DbSet<Cat_ExchangeRate> Cat_ExchangeRate { get; set; }

        //public virtual DbSet<Cat_BuildingType> Cat_BuildingType { get; set; }

        //public virtual DbSet<Cat_PoliceStation> Cat_PoliceStation { get; set; }
        //public virtual DbSet<Cat_PostOffice> Cat_PostOffice { get; set; }
        //public virtual DbSet<Cat_IncenBand> Cat_IncenBand { get; set; }
        //public virtual DbSet<Cat_Catagory> Cat_Catagory { get; set; }
        //public virtual DbSet<Cat_InsurGrade> Cat_InsurGrade { get; set; }
        //public virtual DbSet<Cat_Bank> Cat_Bank { get; set; }
        //public virtual DbSet<Cat_BankBranch> Cat_BankBranch { get; set; }
        //public virtual DbSet<Cat_Gender> Cat_Gender { get; set; }
        //public virtual DbSet<Cat_Leave_Type> Cat_Leave_Type { get; set; }
        //public virtual DbSet<Cat_Emp_Type> Cat_Emp_Type { get; set; }
        //public virtual DbSet<Cat_AttStatus> Cat_AttStatus { get; set; }
        //public virtual DbSet<Cat_Location> Cat_Location { get; set; }
        //public virtual DbSet<Cat_AccountType> Cat_AccountType { get; set; }
        //public virtual DbSet<Cat_PayMode> Cat_PayMode { get; set; }
        //public virtual DbSet<Cat_Variable> Cat_Variable { get; set; }
    }
}
