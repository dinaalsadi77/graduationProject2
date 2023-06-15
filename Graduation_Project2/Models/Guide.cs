using Graduation_Project2.Models.SharedClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Graduation_Project2.Models
{
    public class Guide: MedicalCentreEmployees
    {
        public int GuideId { get; set; }

       public ICollection<Appointment> Appointments { get; set; }
        public MedicalCentre medicalCentre { get; set; }
        public int ? MedicalCentreId { get; set; }



    }
}
