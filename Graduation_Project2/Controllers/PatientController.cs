using Graduation_Project2.Data;
using Graduation_Project2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Graduation_Project2.Controllers
{
    //[Authorize]
    public class PatientController : Controller
    {
        private Graduation2DbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PatientController(Graduation2DbContext db , IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public int isValedToAccess()
        {
            var id = HttpContext.Session.GetInt32("ID");
            var cat = HttpContext.Session.GetString("Cat");
            if (id !=null && cat=="Patient")
            return 0;
            else if(id !=null&& cat!="Patient")
            return 1;
            else 
            return 2;
        }
        [HttpGet]
        public IActionResult HomePage()
        {
            if (isValedToAccess() == 0)
            {
                return RedirectToAction("Index", "Home");
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

        public IActionResult SetPatient()
        {
            if (isValedToAccess() == 0)
            {
                return View();
            }
            else if (isValedToAccess() == 1) {
                if(HttpContext.Session.GetString("Cat")=="Doctor")
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat") + "Admin");
                else
                return RedirectToAction("HomePage", HttpContext.Session.GetString("Cat"));
                 
            } 
       else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SetPatient(Patient g)
        {
            var id = HttpContext.Session.GetInt32("ID");

            var data = _db.patients.Find(id);/////////////////////////////////////////////
            data.FirstName = g.FirstName;
            data.LastName = g.LastName;
            data.Gender = g.Gender;
            data.PhoneNumber = g.PhoneNumber;
            data.Age = g.Age;
            data.Address = g.Address;
            data.Nationality = g.Nationality;
            data.MedicalRecord = g.MedicalRecord;

            /**/
            //Save image to wwwroot/image
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(g.IdPhoto_File.FileName);
            string extension = Path.GetExtension(g.IdPhoto_File.FileName);
            g.IDPhoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            data.IDPhoto = g.IDPhoto;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await g.IdPhoto_File.CopyToAsync(fileStream);
            }



            _db.patients.Update(data);
            _db.SaveChanges();
            return RedirectToAction("HomePage");

        }

        [HttpGet]
        public IActionResult Search()
        {
            if (isValedToAccess() == 0)
            {
                // List<string> countries = new List<string> { "USA", "Canada", "Mexico", "France", "Germany", "Spain" };

                // ViewBag.country = new SelectList(countries, "ToString()");


                var MC = from item in _db.medicalCentres
                         from i in _db.registrationReqs
                         where item.MedicalCentreId == i.MedicalCentreID && i.State == Graduation_Project2.Models.Response.Accept
                         select item;
                ViewBag.country = new SelectList(MC, "MedicalCentreId", "Country");

                var DR = from item in _db.doctorAdmins
                         from i in _db.doctorReqs
                         where item.DoctorAdminId == i.userID && i.State == Graduation_Project2.Models.Response.Accept
                         select item;

                ViewBag.dr = JsonConvert.SerializeObject(DR);


                return View();
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


        [HttpGet]
        public IActionResult DoctorList(int country, string mySelect)
        {
            if (isValedToAccess() == 0)
            {
                var filterDoctor = (from doc in _db.doctorAdmins
                                    from req in _db.doctorReqs
                                    where doc.MedicalCentreId == country
                                    where doc.Specialization == mySelect
                                    where doc.DoctorAdminId == req.userID && req.State == Graduation_Project2.Models.Response.Accept
                                    select doc);//.FirstOrDefault() ;
                return View(filterDoctor);
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

        public IActionResult DoctorDetails(int id)
        {
            if (isValedToAccess() == 0)
            {



                DoctorAdmin doc = _db.doctorAdmins.Find(id);
                List<Schedule> scheules = (from table1 in _db.schedules
                                           join table2 in _db.allSchedules
                                           on table1.AllScheduleId equals table2.AllScheduleId
                                           where table2.doctorAdminId == id
                                           select table1).ToList();

                ViewData["scheules"] = scheules;
                if (doc == null)
                    return NotFound();
                return View(doc);



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


        

        public IActionResult ProfileDate(string query, int id)
        {
            DateTime dateTime = DateTime.Parse(query);
            Schedule sch = (from row in _db.schedules
                            where row.Date == dateTime && row.AllScheduleId == id
                            select row).FirstOrDefault();
            sch.state = true;
            _db.schedules.Update(sch);
            _db.SaveChanges();


            //for create appointment
            int? patId_Session = HttpContext.Session.GetInt32("ID");
            AllSchedule allsch = _db.allSchedules.Find(id);
            int docId = allsch.doctorAdminId;

            Appointment app = new Appointment
            {
                scheduleId = sch.ScheduleId,
                
                PatientId = patId_Session ?? 0,
                DoctorAdminId = docId
            };

            _db.Appointments.Add(app);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult myAppointments()
        {
            appointmentFun();

            return View();

        }

        [HttpPost]
        public IActionResult myAppointments(int id)
        {
            Appointment appointment = _db.Appointments.Find(id);

            var oo = (from item in _db.allSchedules
                     where item.doctorAdminId == _db.doctorAdmins.Find(appointment.DoctorAdminId).DoctorAdminId
                     select item).FirstOrDefault();

            var ii =(from i in _db.schedules
                     where i.AllScheduleId ==oo.AllScheduleId
                    where i.ScheduleId== appointment.scheduleId
                    select i).FirstOrDefault();
            ii.state = false;
            _db.schedules.Update(ii);
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            appointmentFun();

            return View();
        }

        public void appointmentFun()
        {
            int? patientId = HttpContext.Session.GetInt32("ID");


            List<Appointment> appointments = (from app in _db.Appointments
                                              where app.PatientId == patientId
                                              select app).ToList();

            List<DoctorAdmin> doctorLst = new List<DoctorAdmin>();
            List<Schedule> scheduleLst = new List<Schedule>();
            foreach (Appointment appointment in appointments)
            {
                DoctorAdmin doctorAdmin = _db.doctorAdmins.Find(appointment.DoctorAdminId);
                Schedule schedule = _db.schedules.Find(appointment.scheduleId);
                doctorLst.Add(doctorAdmin);
                scheduleLst.Add(schedule);
            }

            ViewData["appointments"] = appointments;
            ViewData["doctors"] = doctorLst;
            ViewData["schedules"] = scheduleLst;

        }


    }
}
