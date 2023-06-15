using Graduation_Project2.Models.SharedClass;
//using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;

namespace Graduation_Project2.Models
{
    public class DoctorAdmin:MedicalCentreEmployees
    {
        public int DoctorAdminId { get; set; }

        public double Fees { get; set; }
        public string DoctorDescription { get; set; }
        public double Discount { get; set; }
        public string waitingTime { get; set; }
        public string Specialization { get; set; }
        //public List<Guide> guides { get; set; }

        //to know if the doctor is the first doctor in the medical menter or not
        public bool isFirst { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public AllSchedule allSchedule { get; set; }

        public MedicalCentre medicalCentre { get; set; }
       public int ? MedicalCentreId { get; set; }

    }
}
