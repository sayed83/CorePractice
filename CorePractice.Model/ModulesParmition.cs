using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CorePractice.Models
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCaption { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleLink { get; set; }
        public byte[] ModuleImage { get; set; }
        public string ModuleImagePath { get; set; }
        public string ModuleImageExtension { get; set; }
        public int IsInActive { get; set; }
        public int? SLNO { get; set; }

        public virtual ICollection<ModuleGroup> ModuleGroups { get; set; }
    }

    public class ModuleGroup
    {
        [Key]
        public int ModuleGroupId { get; set; }
        public string ModuleGroupName { get; set; }
        public string ModuleGroupCaption { get; set; }
        public byte[] ModuleGroupImage { get; set; }
        public string ImagePath { get; set; }
        public string ImageExtension { get; set; }
        public int? SLNO { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Modules { get; set; }

        public virtual ICollection<ModuleMenu> ModuleMenu { get; set; }
    }

    public class ModuleMenu
    {
        [Key]
        public int ModuleMenuId { get; set; }
        public string ModuleMenuName { get; set; }
        public string ModuleMenuCaption { get; set; }
        public byte[] ModuleMenuImage { get; set; }
        public string ImagePath { get; set; }
        public string ModuleImageExtension { get; set; }
        public string ModuleMenuController { get; set; }
        public string ModuleMenuLink { get; set; }
        public int IsInActive { get; set; }
        public int IsParent { get; set; }
        public bool Active { get; set; }
        public int? SLNO { get; set; }


        public int ModuleId { get; set; }
        public virtual Module Modules { get; set; }
        public int ModuleGroupId { get; set; }
        public virtual ModuleGroup ModuleGroup { get; set; }
        public int ImageCriteriaId { get; set; }
        public virtual ImageCriteria ImageCriteria { get; set; }

        [ForeignKey("ModuleMenuId")]
        public int ParentId { get; set; }
        public virtual ModuleMenu ParentModuleMenu { get; set; }
        public virtual ICollection<ModuleMenu> ModuleMenuChildren { get; set; }

    }

    public class ImageCriteria
    {
        [Key]
        public int ImageCriteriaId { get; set; }
        public string ImageCriteriaCaption { get; set; }
    }

    public class MenuPermission_Master
    {
        [Key]
        public int MenuPermissionMasterId { get; set; }
        public string useridPermission { get; set; }
        public int comid { get; set; }
        public string userid { get; set; }
        public string useridUpdate { get; set; }
        public bool Active { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }

        public virtual ICollection<MenuPermission_Details> MenuPermission_Details { get; set; }
    }

    public class MenuPermission_Details
    {
        [Key]
        public int MenuPermissionDetailsId { get; set; }
        public bool IsCreated { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsView { get; set; }
        public bool IsReport { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public int MenuPermissionMasterId { get; set; }
        public virtual MenuPermission_Master MenuPermission_Master { get; set; }
        public int ModuleMenuId { get; set; }
        public virtual ModuleMenu ModuleMenu { get; set; }

    }

    
}
