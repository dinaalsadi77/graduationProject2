using System;

namespace Graduation_Project2.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public bool state { get; set; }

        public AllSchedule allSchedule { get; set; }
        public int AllScheduleId { get; set; }

        public Appointment appointment { get; set; }
    }
}
