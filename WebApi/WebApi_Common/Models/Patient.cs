using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApi_Common.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Complaint { get; set; }
        public string Diagnosis { get; set; }

        public override string ToString()
        {
            return $"Név: {Name} - Lakcím: {Address} - TAJ szám: {SocialSecurityNumber} - Panasz: {Complaint} - Diagnózis: {Diagnosis}";
        }
    }
}
