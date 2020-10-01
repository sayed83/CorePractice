using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CorePractice.Services;
using CorePractice.Component;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using CorePractice.Models;
using GTERP.Models.Common;
using System.Data.SqlClient;
using CorePractice.Data;
using Company = CorePractice.Models.Company;
using Microsoft.EntityFrameworkCore;

namespace CorePractice.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext db;
        private object response;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }
        public bool ShowResend { get; set; }
        public string UserId { get; set; }


        [TempData]
        public string ErrorMessage { get; set; }
        public TransactionLogRepository LogRepository { get; }
        public IConfiguration Config { get; }
        public IConfiguration Configuration { get; }
        public AuthorizationFilterContext Accessor { get; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public Guid AppKey { get; set; } = Guid.Parse("00696C16-1B7D-91B9-783A-B789C9D15B2C");
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            string request = JsonConvert.SerializeObject(Input);

            LoginResponse res = new LoginResponse(request);

            if (res.Success == true)
            {
                AppData.dbdaperpconstring = Config.GetConnectionString("DefaultConnection");



                HttpContext.Session.SetString("userid", res.UserId);
                HttpContext.Session.SetString("username", res.UserName);
                //HttpContext.Session.SetString("comid", res.Companie.ComId);              
                HttpContext.Session.SetString("appkey", Input.AppKey.ToString());



                if (res.Companies != null)
                {



                    var companies = new List<CompanyUser>();
                    foreach (var item in res.Companies)
                    {
                        var company = new CompanyUser();
                        company.ComId = item.ComId;
                        company.CompanyName = item.CompanyName;
                        companies.Add(company);
                    }

                    HttpContext.Session.SetObject("company", companies);
                    SqlParameter[] sqlParameter1 = new SqlParameter[1];
                    sqlParameter1[0] = new SqlParameter("@userid", res.UserId);

                    var companys = Helper.ExecProcMapTList<CompanyUser>("prcgetCompany", sqlParameter1);
                    if (companys.Count > 0)
                    {


                        List<CompanyUser> CompanyUserList = new List<CompanyUser>();

                        for (int i = 0; i < companies.Count(); i++)
                        {
                            var apiCompany = companies.ElementAt(i);
                            foreach (var abc in companys)
                            {

                                CompanyUser com = new CompanyUser();
                                if (abc.ComId == apiCompany.ComId)
                                {
                                    com.ComId = apiCompany.ComId;
                                    com.CompanyName = apiCompany.CompanyName;
                                    CompanyUserList.Add(com);
                                }


                            }
                        }


                        if (CompanyUserList.Count > 0)
                        {
                            HttpContext.Session.SetObject("UserCompanys", CompanyUserList);

                        }

                        // _signInManager.IsSignedIn(res.UserId);

                        //var userId = res.UserId;

                        var comId = companys.Where(x => x.isDefault == true).FirstOrDefault().ComId;
                        HttpContext.Session.SetString("comid", comId.ToString());


                        var userId = res.UserId;
                        SqlParameter[] sqlParameter = new SqlParameter[2];
                        sqlParameter[0] = new SqlParameter("@comid", comId);
                        sqlParameter[1] = new SqlParameter("@userid", userId);
                        List<UserMenuPermission> userMenus = Helper.ExecProcMapTList<UserMenuPermission>("prcgetMenuPermission", sqlParameter);


                        // set session Usermenu

                        if (userMenus.Count > 0)
                        {
                            HttpContext.Session.SetObject("UserMenu", userMenus);

                            var ModuleMenuCaption = userMenus.Where(x => x.Visible == true).Distinct().Select(x => x.ModuleMenuCaption).FirstOrDefault();
                            var activemoduleid = userMenus.Where(x => x.Visible == true).Distinct().Select(x => x.ModuleMenuCaption).FirstOrDefault();

                            HttpContext.Session.SetObject("activemodulename", ModuleMenuCaption);
                            HttpContext.Session.SetObject("activemoduleid", activemoduleid);
                            HttpContext.Session.SetObject("Modules", db.Modules.ToList());
                            //    var x = db.ModuleMenus.Where(x => x.isParent == 1).ToList();
                            //HttpContext.Session.SetObject("ModuleMenuPrent",x);
                        }




                       MenuPermission_Master master = db.MenuPermission_Masters.Where(m => m.useridPermission == userId).FirstOrDefault();

                        if (master == null)
                        {
                            return LocalRedirect(returnUrl);
                        }
                        var userMenuPermission = db.MenuPermission_Details.Where(m => m.MenuPermissionMasterId == master.MenuPermissionMasterId)
                            .Select(m => new
                            {
                                MenuPermissionDetailsId = m.MenuPermissionDetailsId,
                                ModuleMenuName = m.ModuleMenu.ModuleMenuName,
                                ModuleMenuCaption = m.ModuleMenu.ModuleMenuCaption,
                                ModuleMenuLink = m.ModuleMenu.ModuleMenuLink,
                                IsCreate = m.IsCreated,
                                IsView = m.IsView,
                                IsEdit = m.IsUpdated,
                                IsDelete = m.IsDeleted,
                                IsReport = m.IsReport

                            }).ToList();//.Where(m => m.MenuPermission_Masters.useridPermission == user.Id).ToList();

                        //var menupermissions = db.MenuPermissionDetails.Where(m => m.MenuPermissionId == menuMaster.MenuPermissionId).ToList();
                        var menus = db.ModuleMenu.Select(m => new
                        {
                            ModuleMenuId = m.ModuleMenuId,
                            ModuleMenuName = m.ModuleMenuName,
                            ModuleMenuCaption = m.ModuleMenuCaption,
                            ModuleMenuLink = m.ModuleMenuLink,
                            isInactive = m.IsInActive,
                            isParent = m.IsParent,
                            Active = m.Active,
                            ParentId = m.ParentId
                        }).ToList();

                        if (userMenuPermission.Count > 0)
                        {
                            HttpContext.Session.SetObject("menupermission", userMenuPermission);
                            HttpContext.Session.SetObject("menu", menus);
                            HttpContext.Session.SetInt32("activemenuid", 1);
                        }


                        _logger.LogInformation("User logged in.");

                        Company abcd = db.Companys.Where(x => x.AppKey == Input.AppKey.ToString()).FirstOrDefault();
                        HttpContext.Session.SetString("isMultiDebitCredit", abcd.isMultiDebitCredit.ToString());
                        HttpContext.Session.SetString("isMultiCurrency", abcd.isMultiCurrency.ToString());
                        HttpContext.Session.SetString("isVoucherDistributionEntry", abcd.isVoucherDistributionEntry.ToString());
                        HttpContext.Session.SetString("isChequeDetails", abcd.isChequeDetails.ToString());

                        HttpContext.Session.SetInt32("defaultcurrencyid", abcd.CountryId);
                        //HttpContext.Session.SetString("defaultcurrencyname", abcd.CountryCompany.CurrencyShortName.ToString());


                        LogRepository.SuccessLogin(HttpContext.Session.GetString("Latitude"), HttpContext.Session.GetString("Longitude"));


                       // List<SalesSub> addtocart = new List<SalesSub>();

                        //HttpContext.Session.SetObject("cartlist", addtocart);
                    }
                }
                // }
                //}

                //var u = db.UserStates.Where(x => x.UserId == res.UserId).Select(x => x.LastVisited).FirstOrDefault();
                //if (u!=null)
                //{
                //    return Redirect(u);
                //}
                //else
                //{
                //    return LocalRedirect(returnUrl);
                //}
                return LocalRedirect(returnUrl);

            }
            else
            {
                if (res.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (res.IsLockedOut)
                {

                    ModelState.AddModelError("", "The account is locked out");
                    return RedirectToPage("./Lockout");
                }
                if (res.IsNotAllowed)
                {

                    _logger.LogWarning("User email is not confirmed.");
                    ModelState.AddModelError("", "Email is not confirmed.");


                    // var user = await _userManager.FindByEmailAsync(Input.Email);
                    UserId = res.UserId;
                    ShowResend = true;
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

            }
        }
    }
}
