using Graduation_Project2.Data;
using Graduation_Project2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation_Project2.Controllers
{
    //[Authorize]
    public class DoctorAdminController : Controller
    {
        private Graduation2DbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        private UserManager<IdentityUser> _userManager;
        public DoctorAdminController(Graduation2DbContext db, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;

        }


        public int isValedToAccess()
        {
            var id = HttpContext.Session.GetInt32("ID");
            var cat = HttpContext.Session.GetString("Cat");
            if (id != null && cat == "Doctor")
                return 0;
            else if (id != null && cat != "Doctor")
                return 1;
            else
                return 2;
        }


        public IActionResult SetDoctor()
        {
            if (isValedToAccess() == 0)
            {
                return View();

            }
            else if (isValedToAccess() == 1)
            {
               return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SetDoctor(DoctorAdmin d)
        {

       
        var id = HttpContext.Session.GetInt32("ID");
            var data = _db.doctorAdmins.Find(id);/////////////////////////////session//////////////////////////////////////
            data.waitingTime = d.waitingTime;
            data.FirstName = d.FirstName;
            data.LastName = d.LastName;
            data.PhoneNumber = d.PhoneNumber;
            data.Gender = d.Gender;
            data.Address = d.Address;
            data.Age = d.Age;
            data.Nationality = d.Nationality;
            data.Specialization = d.Specialization;
            data.Fees = d.Fees;

            /**/
            //Save image to wwwroot/image
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(d.IdPhoto_File.FileName);
            string extension = Path.GetExtension(d.IdPhoto_File.FileName);
            d.IDPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            data.IDPhoto = d.IDPhoto;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await d.IdPhoto_File.CopyToAsync(fileStream);
            }

            _db.doctorAdmins.Update(data);
            _db.SaveChanges();
            return RedirectToAction("HomePage");

        }


        // create Doctor Requset entity
        public IActionResult DoctorReq()
        {
        
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst")=="True")
            {
                var id = HttpContext.Session.GetInt32("ID");
                var sessionObj = _db.doctorAdmins.Find(id);

                var list = from item in _db.doctorReqs
                           from i in _db.doctorAdmins
                           where sessionObj.MedicalCentreId == i.MedicalCentreId && item.userID == i.DoctorAdminId
                           select item;

                return View(list);
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int? id)
        {
           
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                if (id == null)
                {
                    return RedirectToAction("DoctorReq");
                }
                var req = _db.doctorReqs.Find(id);

                if (req.category == "Doctor")
                {
                    var user = _db.doctorAdmins.Find(req.userID);
                    ViewData["MedicalCentre"] = _db.medicalCentres.Find(user.MedicalCentreId).Name;
                    return View(user);
                }
                //else if (req.category == "Guide")
                //{
                //    var user = _db.Guides.Find(req.userID);

                //    ViewBag.MedicalCentre = _db.medicalCentres.Find(user.MedicalCentreId).Name;
                //    return View(user);
                //}
                else
                {
                    return RedirectToAction("DoctorReq");
                }

            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Accept(DoctorReq reg)
        {
            var data = (from item in _db.doctorReqs
                        where item.DoctorReqId == reg.DoctorReqId
                        select item).FirstOrDefault();


            data.State = Graduation_Project2.Models.Response.Accept;
            _db.doctorReqs.Update(data);
            _db.SaveChanges();

            return RedirectToAction("DoctorReq");


        }

        [HttpPost]
        public async Task<IActionResult> Reject(DoctorReq reg)
        {
            var data = (from item in _db.doctorReqs
                        where item.DoctorReqId == reg.DoctorReqId
                        select item).FirstOrDefault();


            data.State = Graduation_Project2.Models.Response.Reject;
            _db.doctorReqs.Update(data);

            var doc = (from item in _db.doctorAdmins
                       where item.DoctorAdminId == data.userID
                       select item).FirstOrDefault();

            _db.doctorAdmins.Remove(doc);
            _db.doctorReqs.Remove(data);

            var user = await _userManager.FindByEmailAsync(doc.EmailAddress);

            var result = await _userManager.DeleteAsync(user);

            _db.SaveChanges();

            return RedirectToAction("DoctorReq");


        }



        [HttpGet]
        public IActionResult DeleteDoctor()
        {
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                var id = HttpContext.Session.GetInt32("ID");
                var sessionObj = _db.doctorAdmins.Find(id);

                var list = from item in _db.doctorReqs
                           from i in _db.doctorAdmins
                           where sessionObj.MedicalCentreId == i.MedicalCentreId
                           && item.userID == i.DoctorAdminId
                           && item.State == Graduation_Project2.Models.Response.Accept
                           select i;

                //List<DoctorAdmin> AllAcceptedDoc = new List<DoctorAdmin>();
                //foreach (var item in _db.doctorAdmins)
                //{
                //    foreach (var i in _db.doctorReqs)
                //    {
                //        if (item.DoctorAdminId == i.userID && i.State == Graduation_Project2.Models.Response.Accept)
                //        {
                //            AllAcceptedDoc.Add(item);
                //        }
                //    }
                //}
                return View(list);
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        //details from medical center

        public IActionResult DocAdmin_Details(int? id)
        {
           
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                if (id == null)
                {
                    return RedirectToAction("DeleteDoctor");
                }
                var data = _db.doctorAdmins.Find(id);

                return View(data);
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDA(DoctorAdmin d)
        {
            if (d == null) { return RedirectToAction("DeleteDoctor"); }
            var doc = _db.doctorAdmins.Find(d.DoctorAdminId);
           
            var DocReq = (from t in _db.doctorReqs
                          where t.userID == doc.DoctorAdminId
                          select t).FirstOrDefault();
            _db.doctorReqs.Remove(DocReq);

            var user = await _userManager.FindByEmailAsync(doc.EmailAddress);
            if (user != null) {await _userManager.DeleteAsync(user); }

            _db.doctorAdmins.Remove(doc);
            _db.SaveChanges();

            return RedirectToAction("DeleteDoctor");
        }
        [HttpGet]
        public IActionResult CreateGuide()
        {
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                if (TempData.TryGetValue("message", out object message))
                {
                    TempData["message"] = message;
                }
                ViewBag.m = "sss";
                return View();
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }

            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateGuide(Guide d)
        //{
        //    d.MedicalCentreId = 1;//Session MC id
        //    d.Cetegory = "Guide";
        //    _db.Guides.Add(d);
        //    _db.SaveChanges();

        //    var result =await _userManager.FindByEmailAsync(d.EmailAddress);
        //    if (result == null)
        //    {
        //        IdentityUser _user = new IdentityUser
        //        {
        //            Email = d.EmailAddress,
        //            UserName = d.EmailAddress

        //        };

        //        var Result = await _userManager.CreateAsync(_user, d.Password);
        //        if (Result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else return View();
        //    }
        //    else return View();
        //}


        [HttpGet]
        public IActionResult DeleteGuide()
        {           

            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                //find all guides that inside the same MC that session Doctor work on it
                var id = HttpContext.Session.GetInt32("ID");
                var sessionObj = _db.doctorAdmins.Find(id);

                var list = from i in _db.Guides
                           where sessionObj.MedicalCentreId == i.MedicalCentreId
                           select i;


                return View(list);
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        //details from medical center

        public IActionResult Guide_Details(int? id)
        {
            if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "True")
            {
                if (id == null)
                {
                    return RedirectToAction("DeleteGuide");
                }
                var data = _db.Guides.Find(id);

                return View(data);
            }
            else if (isValedToAccess() == 0 && HttpContext.Session.GetString("isFirst") == "False")
            {
                return RedirectToAction("HomePage", "DoctorAdmin");
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteG(Guide g)
        {
            if (g == null) { return RedirectToAction("DeleteGuide"); }
            var guide = _db.Guides.Find(g.GuideId);

            var user = await _userManager.FindByEmailAsync(guide.EmailAddress);
            if (user != null) { await _userManager.DeleteAsync(user); }

            try
            {
                _db.Guides.Remove(guide);
                _db.SaveChanges();
            }
            catch (Exception ex) {
                ModelState.AddModelError("","this guide has an appointments ..!!!");
                    }
            return RedirectToAction("DeleteGuide");
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            if (isValedToAccess() == 0)
            {
                var id = HttpContext.Session.GetInt32("ID");
                var sessionObj = _db.doctorAdmins.Find(id);

                ViewBag.name = sessionObj.FirstName + " " + sessionObj.LastName;

                return View();//session///////////////////////////////////////////////////
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }




        public IActionResult MyGuides()
        {
          

            if (isValedToAccess() == 0 )
            {
                var _session = HttpContext.Session.GetInt32("ID");
                var doc = _db.doctorAdmins.Find(_session);



                var filterGuide = (from gui in _db.Guides
                                   where gui.MedicalCentreId == doc.MedicalCentreId
                                   select gui);//.FirstOrDefault() ;
                return View(filterGuide);
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");

        }


        public IActionResult GuideDetails(int id)
        {
            if (isValedToAccess() == 0)
            {
                var gui = _db.Guides.Find(id);
                return View(gui);
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }



        private static int guideId ;  
        public IActionResult myAppointments(int id)
        {
            if (isValedToAccess() == 0)
            {
                var Gui = _db.Guides.Find(id);
                if (Gui != null)
                {
                    guideId = Gui.GuideId;
                }
                var _session = HttpContext.Session.GetInt32("ID");
                var doc = _db.doctorAdmins.Find(_session);


                var appList = from item in _db.Appointments
                              where item.DoctorAdminId == doc.DoctorAdminId
                              where item.GuideId==null
                              select item;



                return View(appList);
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SetGuideToAppointment(Appointment ap)
        {
            if (isValedToAccess() == 0) {
                
                var app = _db.Appointments.Find(ap.AppointmentID);

                var isValid = (from item in _db.Appointments
                              where item.AppointmentDate == app.AppointmentDate
                              where item.GuideId == guideId
                              select true).FirstOrDefault();
                if (!isValid)
                {
                    app.GuideId = guideId;
                    _db.Update(app);
                    _db.SaveChanges();
                    return RedirectToAction("HomePage");
                }
                else 
                {
                    return RedirectToAction("MyGuides");
                }
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult PatientDetails(int id)
        {
            if (isValedToAccess() == 0)
            {
                var pat = _db.patients.Find(id);
                return View(pat);
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }


        public IActionResult DoctorAppointments()
        {
            if (isValedToAccess() == 0)
            { 
            var id = HttpContext.Session.GetInt32("ID");
            var data = from item in _db.Appointments
                       where item.DoctorAdminId == id
                       select item;
            return View(data);
            }
              else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult PatientDetailsForD(int id)
        {
            if (isValedToAccess() == 0)
            {
                var pat = _db.patients.Find(id);
                return View(pat);
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }










        [HttpGet]
        public IActionResult setSchedule()
        {if (isValedToAccess() == 0)
            {
                return View();
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult setSchedule(DateTime date, string btnSubmit)
        {
            int? docId_Session = HttpContext.Session.GetInt32("ID");
            try
            {
                int allSchId = (from item in _db.allSchedules
                                where item.doctorAdminId == docId_Session
                                select item.AllScheduleId).FirstOrDefault();
                if (allSchId == 0) //if null but in c# int never equal to null so it take its default value which is 0
                {
                    AllSchedule allSch = new AllSchedule
                    {
                        doctorAdminId = docId_Session ?? 0
                    };
              
                    _db.allSchedules.Add(allSch);
                    _db.SaveChanges();


                    int alscId = (from item in _db.allSchedules
                                  where item.doctorAdminId == docId_Session
                                  select item.AllScheduleId).FirstOrDefault();

                    TempData["allSchudleId"] = alscId;

                    var flag = (from item in _db.schedules
                             where item.Date == date
                             select true).FirstOrDefault();
                    if (!flag)
                    {
                        TempData["mass"] = "";
                        Schedule sch = new Schedule
                        {
                            Date = date,
                            state = false,
                            AllScheduleId = Int32.Parse(TempData["allSchudleId"].ToString())
                        };

                        _db.schedules.Add(sch);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["mass"] = "the Date was taken !!";
                    }
                }
                else //اذا كان موجود
                {

                    var All = (from item in _db.allSchedules
                               where item.doctorAdminId == docId_Session
                               select item).FirstOrDefault();
                    var flag = (from item in _db.schedules
                                where item.AllScheduleId == All.AllScheduleId
                                where item.Date == date
                                select true).FirstOrDefault();
                    if (!flag)
                    {
                        Schedule sch = new Schedule
                        {
                            Date = date,
                            state = false,
                            AllScheduleId = allSchId
                        };
                        _db.schedules.Add(sch);
                        _db.SaveChanges();
                    }
                    else
                    {
                        TempData["mass"] = "the Date was taken !!";

                    }
                }

                //   TempData["aaa"] = "fff";
            }

            catch (Exception ex)
            {
                //  TempData["bbb"] = "docId_Session: "+ docId_Session + "\nerror here : " + ex.StackTrace;
                return View();

            }

            //TempData["aaa"] = "cdcdcdcdc";
            if (btnSubmit == "add")
                return RedirectToAction("setSchedule");

            return RedirectToAction("HomePage");
        }



        public IActionResult editSchedule()
        {
            if (isValedToAccess() == 0)
            {
                int? docId_Session = HttpContext.Session.GetInt32("ID");

                int allSchId = (from item in _db.allSchedules
                                where item.doctorAdminId == docId_Session
                                select item.AllScheduleId).FirstOrDefault();


                List<Schedule> schLst = (from item in _db.schedules
                                         where item.AllScheduleId == allSchId
                                         select item).ToList();


                ViewData["editScheules"] = schLst;

                if (docId_Session == null)
                    return NotFound();
                return View();
            }
            else if (isValedToAccess() == 1)
            {
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
            }
            else return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult editSchedule(DateTime[] times, String btnSubmit)
        {
            int? docId_Session = HttpContext.Session.GetInt32("ID");

            int allSchId = (from item in _db.allSchedules
                            where item.doctorAdminId == docId_Session
                            select item.AllScheduleId).FirstOrDefault();


            List<Schedule> schLst = (from item in _db.schedules
                                     where item.AllScheduleId == allSchId
                                     select item).ToList();

            if (btnSubmit != "save")
            { //if click delete
                int rowId = int.Parse(btnSubmit);
                Schedule sch = _db.schedules.Find(rowId);

                _db.schedules.Remove(sch);
                _db.SaveChanges();

                List<Schedule> schLst2 = (from item in _db.schedules //after delete
                                          where item.AllScheduleId == allSchId
                                          select item).ToList();

                ViewData["editScheules"] = schLst2;
                return View();
            }
            else
            {//if click save
             //   int counter = 0;
                for (int i = 0; i < schLst.Count; i++)
                {

                        schLst[i].Date = times[i];
                        _db.schedules.Update(schLst[i]);
                        _db.SaveChanges();
                   
                      


                }
          
            }
            return RedirectToAction("HomePage");

        }

    }
}
