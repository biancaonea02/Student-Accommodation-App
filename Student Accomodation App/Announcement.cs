using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class Announcement
    {
        private String annoncementText;


        //constructor 
        public Announcement(String annoncement)
        {
            this.annoncementText = annoncement;
        }

        //methods

        public string GetInfo()
        {
            return $"Announcement: {this.annoncementText}";
        }


    }
}
