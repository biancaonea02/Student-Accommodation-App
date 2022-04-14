using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class Complaint
    {
        //fields
        private String complaintText;
        private int assignEmployee;

        //constructors
        public Complaint(int newAssignEmployee)
        {
            this.assignEmployee = newAssignEmployee;
        }

        public Complaint(String complaint)
        {
            this.complaintText = complaint;
        }
        //methods
        public String GetComplaint()
        {
            return this.complaintText;
        }
        public string GetInfo()
        {
            return $"Complaint: {this.complaintText}";
        }


    }


}
