using Graduation_Project2.Data;
using Graduation_Project2.Migrations;
using Graduation_Project2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation_Project2.Controllers
{
    public class SystemAdminController : Controller
    {
        private Graduation2DbContext _db;
        private UserManager<IdentityUser> _userManager;

        public SystemAdminController(Graduation2DbContext db ,UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public int isValedToAccess()
        {
            var id = HttpContext.Session.GetInt32("ID");
            var cat = HttpContext.Session.GetString("Cat");
            if (id != null && cat == "SystemAdmin")
                return 0;
            else if (id != null && cat != "SystemAdmin")
                return 1;
            else
                return 2;
        }



        // create registration Requset entity
        public IActionResult medicalReq()
        {
            if (isValedToAccess() == 0)
            {
                return View(_db.registrationReqs);

            }
            else if (isValedToAccess() == 1)
            {
                if (HttpContext.Session.GetString("Cat") == "Doctor")
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        //details from req
        public IActionResult MED_REQ_Details(int? id)
        {
           

            if (isValedToAccess() == 0)
            {
                if (id == null)
                {
                    return RedirectToAction("medicalReq");
                }
                var req = _db.registrationReqs.Find(id);

                var user = _db.medicalCentres.Find(req.MedicalCentreID);
                return View(user);
            }
            else if (isValedToAccess() == 1)
            {
                if (HttpContext.Session.GetString("Cat") == "Doctor")
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        

        [HttpPost]
        public IActionResult MC_Accept(RegistrationReq reg)
        {
            var data = (from item in _db.registrationReqs
                        where item.RegistrationReqId == reg.RegistrationReqId
                        select item).FirstOrDefault();


            data.State = Graduation_Project2.Models.Response.Accept;
            _db.registrationReqs.Update(data);
            _db.SaveChanges();

            return RedirectToAction("medicalReq");


        }

        [HttpPost]
        public async Task <IActionResult> MC_Reject(RegistrationReq reg)
        {
            var data = (from item in _db.registrationReqs
                        where item.RegistrationReqId == reg.RegistrationReqId
                        select item).FirstOrDefault();


            data.State = Graduation_Project2.Models.Response.Reject;
            _db.registrationReqs.Update(data);
            
            var MC= (from item in _db.medicalCentres
                     where item.MedicalCentreId == data.MedicalCentreID
                     select item).FirstOrDefault();
            foreach (var item in _db.doctorAdmins)
            {
                if(item.MedicalCentreId == reg.MedicalCentreID)
                {
                    var DocReq = (from t in _db.doctorReqs
                                  where t.userID == item.DoctorAdminId
                                  select t).FirstOrDefault();
                    if(DocReq != null)
                    {
                        _db.doctorReqs.Remove(DocReq);
                    }
               
                    _db.doctorAdmins.Remove(item);
                    var user = await _userManager.FindByEmailAsync(item.EmailAddress);

                    if (user != null)
                    {
                        var result = await _userManager.DeleteAsync(user);
                    
                    }
                }

            }
            foreach (var item in _db.Guides)
            {
                if (item.MedicalCentreId == reg.MedicalCentreID)
                {
                    _db.Guides.Remove(item);
                    var user =await _userManager.FindByEmailAsync(item.EmailAddress);

                    if (user != null)
                    {
                        var result =await _userManager.DeleteAsync(user);
                        //if (!result.Succeeded)
                        //{
                        //    return NotFound();
                        //}
                    }
                }

            }
          _db.registrationReqs.Remove(data);
            _db.medicalCentres.Remove(MC);
            _db.SaveChanges();
            
            return RedirectToAction("medicalReq");


        }





        [HttpGet]
        public IActionResult DeleteMedicalCentre()
        {
            if (isValedToAccess() == 0)
            {
                var list = from i in _db.medicalCentres
                           from ii in _db.registrationReqs
                           where i.MedicalCentreId == ii.MedicalCentreID && ii.State == Graduation_Project2.Models.Response.Accept
                           select i;

                return View(list);
            }

             else if (isValedToAccess() == 1)
            {
                if (HttpContext.Session.GetString("Cat") == "Doctor")
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        //details from medical center

        public IActionResult MED_CEN_Details(int? id)
        {
           

            if (isValedToAccess() == 0)
            {
                if (id == null)
                {
                    return RedirectToAction("DeleteMedicalCentre");
                }
                var data = _db.medicalCentres.Find(id);

                return View(data);

            }
            else if (isValedToAccess() == 1)
            {
                if (HttpContext.Session.GetString("Cat") == "Doctor")
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMC(MedicalCentre mc)
        {
            if (mc == null) { return RedirectToAction("DeleteMedicalCentre"); }
            var MC = _db.medicalCentres.Find(mc.MedicalCentreId);
           
            foreach (var item in _db.doctorAdmins)
            {
                if(item.MedicalCentreId == MC.MedicalCentreId) {
                    var DocReq = (from t in _db.doctorReqs
                                  where t.userID == item.DoctorAdminId
                                  select t).FirstOrDefault();
                    if (DocReq != null)
                    {
                        _db.doctorReqs.Remove(DocReq);
                    }
                    _db.doctorAdmins.Remove(item);

                    var user = await _userManager.FindByEmailAsync(item.EmailAddress);
                    if (user != null)
                    {
                      //  var result = await _userManager.DeleteAsync(user);
                    }
                }

            }
            foreach (var item in _db.Guides)
            {
                if (item.MedicalCentreId == MC.MedicalCentreId)
                {
                    _db.Guides.Remove(item);
                    var user = await _userManager.FindByEmailAsync(item.EmailAddress);

                    if (user != null)
                    {
                       // var result = await _userManager.DeleteAsync(user);
                    }
                }

            }

            var data = (from item in _db.registrationReqs
                        where item.MedicalCentreID == MC.MedicalCentreId
                        select item).FirstOrDefault();
            _db.registrationReqs.Remove(data);

            _db.medicalCentres.Remove(MC);
            _db.SaveChanges();
          
            return RedirectToAction("DeleteMedicalCentre");
        }



        [HttpGet]
        public IActionResult HomePage()
        {
            if (isValedToAccess() == 0)
            {
                return View(_db.systemAdmins.Find(HttpContext.Session.GetInt32("ID")));

            }
            else if (isValedToAccess() == 1)
            {
                if (HttpContext.Session.GetString("Cat") == "Doctor")
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                    return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

    }
}

