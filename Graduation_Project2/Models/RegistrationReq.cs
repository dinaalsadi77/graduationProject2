using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation_Project2.Models
{
    public enum Response
    {
        InProcess,
        Accept,
        Reject
    }
    public class RegistrationReq 
    {
        public int RegistrationReqId { get; set; }
        public  int MedicalCentreID { get; set; }
        public Response State { get; set; }
    }
}
