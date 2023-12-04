using System.ComponentModel;

namespace Dashboard.Models
{
    public class Operation
    {
        public int id { get; set; }
        public String Patient { get; set; }
        public String OperationType { get; set; }
        public DateTime Booked { get; set; }
        public String Details { get; set; }
        public String DoctorName { get; set; }
    }
}
