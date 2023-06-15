using System;

namespace Graduation_Project2.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Schedule schedule { get; set; }
        public int scheduleId { get; set; }
        public bool DoneStatus { get; set; }/////////////////////////////////////////link to next tow prop Later
        public int DoctorFeedback { get; set; }
        public int GaideFeedback { get; set; }



        public Patient patient { get; set; }
        public DoctorAdmin doctorAdmin { get; set; }
        public Guide guide { get; set; }

        
        
        public int  PatientId { get; set; }
        public int  DoctorAdminId { get; set; }
        public int ? GuideId { get; set; }


    }
}
