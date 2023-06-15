using Graduation_Project2.Data;
using Graduation_Project2.Models;
using Graduation_Project2.Models.outOfBD;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Graduation_Project2.Controllers
{
    public class GuideController : Controller
    {

        Graduation2DbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GuideController(Graduation2DbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public int isValedToAccess()
        {
            var id = HttpContext.Session.GetInt32("ID");
            var cat = HttpContext.Session.GetString("Cat");
            if (id != null && cat == "Guide")
                return 0;
            else if (id != null && cat != "Guide")
                return 1;
            else
                return 2;
        }


        [HttpGet]
        public IActionResult HomePage()
        {
            if (isValedToAccess() == 0)
            {

                var id = HttpContext.Session.GetInt32("ID");
                var sessionObj = _db.Guides.Find(id);

                ViewBag.name = sessionObj.FirstName + " " + sessionObj.LastName;

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
        public IActionResult SetGuide()
        {
            if(isValedToAccess() == 0)
            {
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

        [HttpPost]
        public async Task<IActionResult> SetGuide(Guide g)
        {
            var id = HttpContext.Session.GetInt32("ID");
            
            var data = _db.Guides.Find(id);//////////////////////////////////////////////////////////////
            data.FirstName = g.FirstName;
            data.LastName = g.LastName;
            data.Gender = g.Gender;
            data.PhoneNumber = g.PhoneNumber;
            data.Age = g.Age;
            data.Address = g.Address;
            data.Nationality = g.Nationality;


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



            _db.Guides.Update(data);
            _db.SaveChanges();
            return RedirectToAction("HomePage");

        }


        [HttpGet]
        public IActionResult GuideAppoinments()
        {
         
            if (isValedToAccess() == 0)
            {
                var id = HttpContext.Session.GetInt32("ID");
                var data = from item in _db.Appointments
                           from p in _db.patients
                           where item.guide.GuideId == id  //session/////////////////////////////////////////////////
                           select item;

                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GradProject2me;Trusted_Connection=True;MultipleActiveResultSets=true";
                List<getAppointments> ap = new List<getAppointments>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = $"SELECT * FROM patients,Appointments where Appointments.PatientId  = patients.PatientId AND Appointments.GuideId={id} AND Appointments.DoneStatus = 1";
                    // string sqlQuery = "SELECT * FROM patients";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                getAppointments ahmed = new getAppointments
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    AppointmentDate = reader["AppointmentDate"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Age = reader["Age"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Nationality = reader["Nationality"].ToString(),
                                    MedicalRecord = reader["MedicalRecord"].ToString(),
                                    patientId = reader["PatientId"].ToString()

                                };

                                // Access the columns from the query result
                                ap.Add(ahmed);

                                // Perform further processing with the retrieved data
                            }
                        }
                    }

                    connection.Close();


                    //  return View(data);
                    return View(ap);
                }

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
        public IActionResult PatientInfo(string ?id)
        {
          

            if (isValedToAccess() == 0)
            {
                var data = _db.patients.Find(int.Parse(id));
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
    }
}



