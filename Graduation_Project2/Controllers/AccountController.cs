using Graduation_Project2.Models.ViewModel;
using Graduation_Project2.Models.SharedClass;
using Graduation_Project2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation_Project2.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project2.Controllers
{
  //  [Authorize]
    public  class AccountController : Controller
    {
        private  Graduation2DbContext _db;
        private  UserManager<IdentityUser> _userManager;   // create read delete edit
        private  SignInManager<IdentityUser> _signInManager; //sign in    sign out 
        private  RoleManager<IdentityRole> _roleManager;
      //  private DbSet<CustomIdentityUserRole> _userRoles;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                Graduation2DbContext db
                                )
        {
            _db = db;
         //   _userRoles = _db.customIdentityUserRoles;

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; 
            CreateTheRoles().Wait();
        }
       

      
        private static bool rolesCreated = false;
         
        private async Task CreateTheRoles()
        {
            if (!rolesCreated)
            {
                // Check if the desired role already exists
                if (!await _roleManager.RoleExistsAsync("Doctor"))
                {
                    // Create the role
                    var role = new IdentityRole("Doctor");
                    await _roleManager.CreateAsync(role);
                }
                if (!await _roleManager.RoleExistsAsync("SystemAdmin"))
                {
                    // Create the role
                    var role = new IdentityRole("SystemAdmin");
                    await _roleManager.CreateAsync(role);
                }
                if (!await _roleManager.RoleExistsAsync("Patient"))
                {
                    // Create the role
                    var role = new IdentityRole("Patient");
                    await _roleManager.CreateAsync(role);
                }
                if (!await _roleManager.RoleExistsAsync("Guide"))
                {
                    // Create the role
                    var role = new IdentityRole("Guide");
                    await _roleManager.CreateAsync(role);
                }
                var user =await _userManager.FindByEmailAsync(_db.systemAdmins.Find(1).EmailAddress);
                if (user == null)
                {
                    IdentityUser _user = new IdentityUser
                    {
                        Email = _db.systemAdmins.Find(1).EmailAddress,
                        UserName = _db.systemAdmins.Find(1).EmailAddress

                    };
                    var Result = await _userManager.CreateAsync(_user, _db.systemAdmins.Find(1).Password);
                }

                rolesCreated = true;
            }
           
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(User user, string? MidCentre, string ?Country)
        {
            
            if (ModelState.IsValid)
            {
                if (user.Cetegory == "Doctor")
                {
             
                    var query = _db.medicalCentres.Where(e => e.Name.Contains(MidCentre))
                                                    .Where(x => x.Country.Contains(Country));
                    var results = query.ToList();
                    if (results.Count > 0)
                    {
                        var d = new DoctorAdmin
                        {
                            EmailAddress = user.EmailAddress,
                            Password = user.Password,
                            Cetegory = user.Cetegory,
                            isFirst = false, /// becouse this doctor is not the first doc in the medical centre
                            MedicalCentreId = results[0].MedicalCentreId
                        };
                        _db.doctorAdmins.Add(d);
                        _db.SaveChanges();
                        ///////////////////////////////////////////////////////////////////////////////////
                        var orderedDoc = from item in _db.doctorAdmins
                                         orderby item.DoctorAdminId descending
                                         select item;
                        DoctorReq req = new DoctorReq
                        {
                            userID = orderedDoc.First().DoctorAdminId,
                            category = orderedDoc.First().Cetegory,
                            State = Graduation_Project2.Models.Response.InProcess

                        };
                        _db.doctorReqs.Add(req);
                        ////////////////////////////////////////////////////////////////////////////////////
                    }

                    else
                    {
                        MedicalCentre mc = new MedicalCentre
                        {
                            Name = MidCentre,
                            Country = Country
                        };
                        _db.medicalCentres.Add(mc);
                        _db.SaveChanges();
                        var orderedMC = from item in _db.medicalCentres
                                        orderby item.MedicalCentreId descending
                                        select item;

                        var d = new DoctorAdmin
                        {
                            EmailAddress = user.EmailAddress,
                            Password = user.Password,
                            Cetegory = user.Cetegory,
                            isFirst = true, // becouse this doctor is the first doc in the medical centre
                            MedicalCentreId = orderedMC.First().MedicalCentreId
                        };
                        _db.doctorAdmins.Add(d);
                        _db.SaveChanges();
                        ///////////////////////////////////////////////////////////////////////////////////
                        var orderedDoc = from item in _db.medicalCentres
                                         orderby item.MedicalCentreId descending
                                         select item;
                        RegistrationReq req = new RegistrationReq
                        {

                            MedicalCentreID = orderedMC.First().MedicalCentreId,
                            State = Graduation_Project2.Models.Response.InProcess

                        };
                        _db.registrationReqs.Add(req);

                        var _orderedDoc = from item in _db.doctorAdmins
                                         orderby item.DoctorAdminId descending
                                         select item;

                        DoctorReq _req = new DoctorReq
                        {
                            userID = _orderedDoc.First().DoctorAdminId,
                            category = _orderedDoc.First().Cetegory,
                            State = Graduation_Project2.Models.Response.Accept

                        };
                        _db.doctorReqs.Add(_req);
                        ////////////////////////////////////////////////////////////////////////////////////
                    }


                }
                else if (user.Cetegory == "Patient")
                {
                    var p = new Patient
                    {
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        Cetegory = user.Cetegory
                    };
                    _db.patients.Add(p);

                }
                else
                {
                    var id = HttpContext.Session.GetInt32("ID");
                    var doc = _db.doctorAdmins.Find(id);
                    Guide g = new Guide {
                    MedicalCentreId = doc.MedicalCentreId,//Session MC id//////////////////////////////////////////
                    EmailAddress = user.EmailAddress,
                    Password = user.Password,
                    Cetegory= "Guide"
                    };
                    user.Cetegory = "Guide";
                    _db.Guides.Add(g);

                    IdentityUser __user = new IdentityUser
                    {
                        Email = user.EmailAddress,
                        UserName = user.EmailAddress

                    };
                    var _Result = await _userManager.CreateAsync(__user, user.Password);
                    if (_Result.Succeeded)
                    {
                        return RedirectToAction("HomePage", "DoctorAdmin");
                    }
                    string m = "Your Email is taken or inValed Password";
                    TempData["message"] = m;
                    return RedirectToAction("CreateGuide", "DoctorAdmin");

                }

                IdentityUser _user = new IdentityUser
                {
                    Email = user.EmailAddress,
                    UserName = user.EmailAddress
                    
                };
                var Result = await _userManager.CreateAsync(_user, user.Password);

                //   var __user = await _userManager.FindByEmailAsync(user.EmailAddress);
                //bool roleExists = await _roleManager.RoleExistsAsync("Doctor");
                //  var ___user = await _userManager.FindByIdAsync("09ead184-687b-4b76-9039-e903d5c5dadd"); // Retrieve user from the appropriate source

                //  if (roleExists && ___user != null && Result.Succeeded)
                //  {
                //      var result = await _userManager.AddToRoleAsync(___user, "Doctor");
                //  }





               // var userManager = new UserManager<IdentityUserRole>(new UserStore<IdentityUserRole>(_db));
                //var usler = await userManager.FindByNameAsync("username");

                //if (user != null)
                //{
                //    var roleId = context.Roles.SingleOrDefault(r => r.Name == "RoleName").Id;
                //    context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = roleId });
                //    context.SaveChanges();
                //}

                if (Result.Succeeded)
                { 
                    //IdentityRole role = await _roleManager.FindByNameAsync("Doctor");
                    //if(role !=null &&_user != null)
                    //{
                    //    _db.UserRoles.Add(new CustomIdentityUserRole { UserId = _user.Id, RoleId = role.Id ,CustomIdentityUserRoleId=2});
                    //}
                    /* if (user.Cetegory == "Doctor")
                     {
                         var role = await _roleManager.FindByNameAsync("Doctor");
                         if (role != null )
                         {
                             IEnumerable<string> s = new List<string> {"Doctor" };
                              var result=
                             // await _userManager.AddToRoleAsync(_user, role.Name);

                             //if (!result.Succeeded)
                             //{
                             //    foreach (var err in result.Errors)
                             //    {
                             //        ModelState.AddModelError("", err.Description);
                             //    }
                             //    return View(user);
                             //}
                         }
                     }*/
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(user);
            }
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
         return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user, string EmailAddress, string Password)
        {
           

            if (ModelState.IsValid)
            {
                var Result = await
                       _signInManager.PasswordSignInAsync(EmailAddress, Password, false, false);

                
                if (Result.Succeeded)
                {

                    var r = (from item in _db.doctorAdmins
                              where item.EmailAddress == EmailAddress
                              select item).FirstOrDefault();
                    if (r != null)
                    {
                        bool _r = (from item in _db.doctorReqs
                                   where item.userID == r.DoctorAdminId
                                   where item.State == Models.Response.Accept
                                   select true).FirstOrDefault();
                        if (_r != false)
                        {
                            bool __r = (from item in _db.registrationReqs
                                        where item.MedicalCentreID == r.MedicalCentreId
                                        where item.State == Models.Response.Accept
                                        select true).FirstOrDefault();
                            if (r != null && _r == true && __r == true)
                            {
                                HttpContext.Session.SetString("EmailAddress",EmailAddress);
                                HttpContext.Session.SetInt32("ID",r.DoctorAdminId);
                                HttpContext.Session.SetString("Cat",r.Cetegory);
                                HttpContext.Session.SetString("isFirst",r.isFirst.ToString());
                             //   HttpContext.Session.SetString("IsFirst",r.isFirst.ToString());
                                return RedirectToAction("HomePage", "DoctorAdmin");
                            }
                        }

                    }
                    /*********************************************************************************/
                    var rr = (from item in _db.patients
                              where item.EmailAddress == EmailAddress
                              select item).FirstOrDefault();
                    if (rr != null)
                    {
                            HttpContext.Session.SetString("EmailAddress", EmailAddress);
                        HttpContext.Session.SetInt32("ID", rr.PatientId);
                        HttpContext.Session.SetString("Cat", rr.Cetegory);

                        return RedirectToAction("HomePage", "Patient");
                    }
                    /*********************************************************************************/
                    var rrr = (from item in _db.Guides
                              where item.EmailAddress == EmailAddress
                              select item).FirstOrDefault();
                    if (rrr != null)
                    {
                        bool _rrr = (from item in _db.registrationReqs
                                     where item.MedicalCentreID == rrr.MedicalCentreId
                                     where item.State == Models.Response.Accept
                                     select true).FirstOrDefault();
                        if (rrr != null && _rrr == true)
                        {
                            HttpContext.Session.SetString("EmailAddress", EmailAddress);
                            HttpContext.Session.SetInt32("ID", rrr.GuideId);
                            HttpContext.Session.SetString("Cat", rrr.Cetegory);

                            return RedirectToAction("HomePage", "Guide");
                        }
                    }
                    /*******************************************************************/
                    var rrrr = (from item in _db.systemAdmins
                                 where item.EmailAddress == EmailAddress
                                 select item).FirstOrDefault();
                    if (rrrr != null)
                    {
                        HttpContext.Session.SetString("EmailAddress", EmailAddress);
                        HttpContext.Session.SetInt32("ID", rrrr.SystemAdminId);
                        HttpContext.Session.SetString("Cat", "SystemAdmin");

                        return RedirectToAction("HomePage", "SystemAdmin");
                    }
                }
            
                ModelState.AddModelError("", "Invalid UserName or Password !!");
                return View();

            }

            return View();
        }

        private async Task<bool> ValidateUser(string EmailAddress, string Password)
        {
            var query = _db.doctorAdmins.Where(e => e.EmailAddress.Contains(EmailAddress))
                        .Where(x => x.Password.Contains(Password));
            var results = query.ToList();

            IdentityUser user = new IdentityUser {Email=EmailAddress };
          //  var Result =await _signInManager.CheckPasswordSignInAsync(user,Password,false);
           // if (Result.Succeeded) { return true; }
            if (results.Count > 0)
            {
                HttpContext.Session.SetString("userId", results[0].DoctorAdminId.ToString());
                return true;
            }
            else return false;
        }
        
       

        /*
        public async Task LoginGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<ActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return RedirectToAction("Index", "Home");
        }
 

        [Route("authcontroller")]
        [Route("LoginFacebook")]
        public IActionResult LoginFacebook()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }
        */

        public ActionResult Logout()
        {
             HttpContext.Session.Remove("ID");
             HttpContext.Session.Remove("EmailAddress");
             HttpContext.Session.Remove("Cat");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        //  var Result = await _roleManager.CreateAsync(role);
        //await _userManager.IsInRoleAsync(user, role.Name)
        //            var role = await _roleManager.FindByIdAsync(roleID);

        //      result = await _userManager.AddToRoleAsync(user, role.Name);
        //                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);

        /*


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var Result = await _roleManager.CreateAsync(role);

                if (Result.Succeeded) return RedirectToAction("RolesList");

                ModelState.AddModelError("", "Role Not Created ..!");
                return View(model);

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction("RolesList");
            }
            var data=await  _roleManager.FindByIdAsync(id);
            if (data ==null)
            {
                return RedirectToAction("RolesList");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleID=data.Id,
                RoleName=data.Name
            };
            foreach (var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, model.RoleName))
                {
                    model.UsersNames.Add(user.UserName);
                }
            }
                return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleID);
                role.Name = model.RoleName;
                var Result = await _roleManager.UpdateAsync(role);
                if (Result.Succeeded) return RedirectToAction("RolesList");
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("",err.Description);

                }
                return View(model);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> AddRemoveUserRole(string id)
        {

           


            HttpContext.Session.SetString("roleID", id);
            var role = await _roleManager.FindByIdAsync(id);
            if (id == null) return RedirectToAction("RolesList");

            var UserRolesViewModel = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var UserRoleVM = new UserRoleViewModel {
                UserId=user.Id,
                UserName=user.UserName
                };
                
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoleVM.IsSelected = true;
                }
                else
                {
                    UserRoleVM.IsSelected = false;
                }

                UserRolesViewModel.Add(UserRoleVM);
            }
            return View(UserRolesViewModel);
        }
        
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRemoveUserRole(List<UserRoleViewModel> model)
        {
            string roleID = HttpContext.Session.GetString("roleID");
            var role = await _roleManager.FindByIdAsync(roleID);
            if (role == null) return RedirectToAction("RolesList");
            IdentityResult result = null;

            for(int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                return RedirectToAction("RolesList");
 
            }

            return RedirectToAction("RolesList");
        }
        */
    }
}
