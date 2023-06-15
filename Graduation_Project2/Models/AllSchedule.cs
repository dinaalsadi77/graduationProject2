using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections;
using System.Collections.Generic;

namespace Graduation_Project2.Models
{
	public class AllSchedule
	{
    
        public int AllScheduleId { get; set; }
		//public int schConter { get; set; }
		public DoctorAdmin doctorAdmin { get; set; }
		public int doctorAdminId { get; set; }

		public ICollection<Schedule> schedules { get; set; }

	}
}
