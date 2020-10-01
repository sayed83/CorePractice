using System;

namespace CorePractice.Models
{
    public class AspnetUserList
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class CompanyList
    {
        public int isChecked { get; set; }

        public Guid ComId { get; set; }
        public string UserId { get; set; }

        public int CompanyPermissionId { get; set; }

        public string CompanyName { get; set; }
        public int isDefault { get; set; }

    }
    public class GetUserModel
    {
        public Guid AppKey { get; set; }

    }

    public class CompanyUser
    {
        public Guid ComId { get; set; }
        public string CompanyName { get; set; }
        public bool isDefault { get; set; }
    }

    public class UserMenuPermission
    {
        public int MenuPermissionId { get; set; }
        public int MenuPermissionDetailsId { get; set; }
        public int ModuleMenuId { get; set; }
        public string ModuleMenuLink { get; set; }
        public string ModuleMenuCaption { get; set; }
        public string ModuleMenuController { get; set; }
        public int isInactive { get; set; }
        public int isParent { get; set; }
        public int ParentId { get; set; }
        public bool Active { get; set; }

        public bool Visible { get; set; }
        public int ModuleId { get; set; }


        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }
        public bool IsReport { get; set; }
    }
}
