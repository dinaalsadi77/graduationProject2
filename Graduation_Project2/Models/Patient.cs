using Graduation_Project2.Models.SharedClass;
using System.Collections.Generic;

namespace Graduation_Project2.Models
{
    public class Patient:User
    {
        public int PatientId { get; set; }
        public string MedicalRecord { get; set; }


        //public CridetPayment cridet_payment { get; set; }
        //public int CridetPaymentId { get; set; }


        public ICollection<Appointment> Appointments { get; set; }
    }
}
