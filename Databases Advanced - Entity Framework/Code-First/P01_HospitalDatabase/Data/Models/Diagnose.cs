using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public Diagnose()
        {

        }
        public Diagnose(string name,string comments,int patiendId)
        {
            Name = name;
            Comments = comments;
            PatientId = patiendId;
        }
        public int DiagnoseId { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
