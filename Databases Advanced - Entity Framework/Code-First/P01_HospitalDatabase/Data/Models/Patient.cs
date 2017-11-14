using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
        }
        public Patient(string firstName, string lastName, string address, string email, bool hasInsurance)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            HasInsurance = hasInsurance;
        }

        public int PatientId { get; set; }        
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool HasInsurance { get; set; }
        public ICollection<Visitation> Visitations { get; set; } = new List<Visitation>();
        public ICollection<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();
        public ICollection<PatientMedicament> Prescriptions { get; set; } = new List<PatientMedicament>();
    }
}
