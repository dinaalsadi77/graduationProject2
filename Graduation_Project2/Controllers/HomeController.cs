using Graduation_Project2.Data;
using Graduation_Project2.Models;
using Graduation_Project2.Models.SharedClass;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation_Project2.Controllers
{
    public class HomeController : Controller
    {
        Graduation2DbContext _db;
        public HomeController(Graduation2DbContext db)
        {
            _db = db;
        }
        private readonly ILogger<HomeController> _logger;







        public IActionResult Index()
        {
            List<DoctorAdmin> doc = new List<DoctorAdmin>();

            doc = _db.doctorAdmins.Take(4).ToList();


            ViewBag.numOfDoc = _db.doctorAdmins.Count();

            ViewBag.numOfDept = _db.doctorAdmins.Select(x => x.Specialization).Distinct().Count();
            return View(doc);
        }



       



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//1
//  اخلي كل الاكشنز الي تابعات ل كاتيغوري معينه يرجعوا على الهووم بيج تاعتها


//2
// Layouts for each Role

//3
// قبل ما يعمل تسجيل دخول يشيك اذا الحساب مقبول سواء كان دكتور او مرشد  


//4
//  صور البروفايلات بالداتا بيس


//5
//  اذا كان الدكتور هو الدكتور الاول يشيك اذا العيادة تاعته مقبوله قبل ما يسجل دخول



//6
// لازم اعمل لكل الاكشنز اوثارايزد بحيث ما اقدر ادخل على الاكشن من الرابط 
//+قصه اللاي اوت للدكتور الاول




//7
// الي ما انقبل بنحذف من الداتا بيس القديمه والجديده 




//8
// اي اشي بنحذف لازم احذفه من اليوزرز تيبل         



//9
// السيشين وتوابعه







//((((

//ممكن نحذف النقطتين 10+11 مع حذف الريت والتقييمات كلها من الداتبيس


//10
//  rate prop انه ما الدكتور او المرشد ما يضيفوا الريت لما يدخلو معلوماتهم ولكن بعطيها صفر عشان ما تكون نل ةتضرب صفحات ثانيه


//11
// في عندي بالابوينتمنت مغيرين بمثلوا تققيم المرشد وتقييم الدكتور بالتالي لازم نعمل عمليه
// حسابيه داخل فنكشن داخل القايد كونترولار بعدل على الرييت تاعتهم كل ما تم اضافه انهاء موعد بخصهم  


//)))






//12
//مشكله بتسجيل الخروج



//13
//الاندكس بيج لازم اعدل القيم الستاسيك الي فيها



//14
//انه الدكتور يكون قادر انه يحذف او يعدل عالسكاجوال تاعته



//15
//تقفيل الفنكشنز للمريض والدكتور من التيم




//16
//الماب



//17
//الشات




