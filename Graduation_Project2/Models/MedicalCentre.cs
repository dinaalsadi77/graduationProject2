using Graduation_Project2.Models.SharedClass;
using System.Collections.Generic;

namespace Graduation_Project2.Models
{
    public class MedicalCentre
    {
        public int MedicalCentreId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }             
        //  public ICollection<MedicalCentreEmployees> medicalCentreEmp { get; set; }
        public ICollection<DoctorAdmin> doctorAdmins { get; set; }
        public ICollection<Guide> guides { get; set; }
    }
}
