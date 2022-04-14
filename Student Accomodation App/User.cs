using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class User
    {
        // fields
        private static int idSeeder = 100;
        private int id;
        private String name;
        private String phoneNumber;
        private int nrOfPoints;
        private String password;
        private int permission;

        //properties
        public int NrOfPoints { get { return this.nrOfPoints; } }
        public String Name { get { return this.name; } }
        public String Password { get { return this.password; } }
        public int Id { get { return this.id; } }
        public int Permission { get { return this.permission; } }
        public String PhoneNumber { get { return this.phoneNumber; } }

        //constructors

        public User(String newName, String newPhoneNumber, String newPassword, int permission)
        {
            this.id = idSeeder;
            idSeeder++;
            this.name = newName;
            this.phoneNumber = newPhoneNumber;
            this.nrOfPoints = 0;
            this.password = newPassword;
            this.permission = permission;
        }
        public User(String newName, int newID, String newPassword)
        {
            this.name = newName;
            this.id = newID;
            this.password = newPassword;
            this.phoneNumber = "";
            this.nrOfPoints = 0;
        }

        public User(String newName)
        {
            this.name = newName;
            this.nrOfPoints = 0;
        }



        public String GetNameUser()
        {
            return this.name;
        }

        public int getIDUser()
        {
            return this.id;
        }

        public int getNrOfPointsUser()
        {
            return this.nrOfPoints;
        }
        public String getPassword()
        {
            return this.password;
        }
        /// <summary>
        /// Method to verify the password, who is logged in. Returns true for students and false for employees.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool verifyPassword(String password)
        {
            if (this.getPassword() == "student")
            { return true; }
            else
            {
                return false;
            }
        }

        public String getInfo()
        {
            String holder;
            holder = this.name + " has this ID: " + this.id + ", " + " password: " + this.password + ", number of points: " + this.nrOfPoints + ", phone number: " + this.phoneNumber;
            return holder;
        }

        public void AddPoint()
        {
            this.nrOfPoints++;
        }
    }
}
