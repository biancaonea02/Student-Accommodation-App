using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class StudentHouse
    {
        //fields

        private String name;
        private String address;
        private List<User> theUser;
        private List<Complaint> theComplaint;
        private List<Announcement> theStudentAnnouncement;
        private List<Announcement> theEmployeeAnnouncement;
        private List<HouseRule> theHouseRule;
        private List<Task> theTask;

        //constructors

        public StudentHouse()
        {
            this.theStudentAnnouncement = new List<Announcement>();
            this.theEmployeeAnnouncement = new List<Announcement>();
            this.theComplaint = new List<Complaint>();
            this.theHouseRule = new List<HouseRule>();
            this.theUser = new List<User>();
            this.theTask = new List<Task>();
        }
        public StudentHouse(String newName, String newAddress)
        {
            this.name = newName;
            this.address = newAddress;
            this.theUser = new List<User>();


        }

        //methods


        public List<User> getAllUsers()
        {
            return this.theUser;
        }

        public List<Complaint> GetAllComplaints()
        {
            return this.theComplaint;
        }
        public List<Announcement> GetAllStudentAnnoncements()
        {
            return this.theStudentAnnouncement;
        }

        public List<Announcement> GetAllEmployeeAnnoncements()
        {
            return this.theEmployeeAnnouncement;
        }


        public List<HouseRule> GetAllHouseRules()
        {
            return this.theHouseRule;
        }

        public List<Task> GetAllTasks()
        {
            return this.theTask;
        }


        public User getUserID(int ID)
        {
            foreach (User u in this.theUser)
            {
                if (u.getIDUser() == ID)
                {
                    return u;
                }
            }
            return null;
        }

        public User GetUserName(String name)
        {
            foreach (User u in this.theUser)
            {
                if (u.GetNameUser() == name)
                {
                    return u;
                }
            }
            return null;
        }

        public User GetUserPassword(String password)
        {
            foreach (User u in this.theUser)
            {
                if (u.Password == password)
                {
                    return u;
                }
            }
            return null;
        }
        public User GetUserNamePasswordId(String name, String password, int id) //get user by name, password and id
        {
            foreach (User u in this.theUser)
            {
                if (u.Name == name && u.Password == password && u.Id == id)
                {
                    return u;
                }
            }
            return null;
        }



        public void AddUser(String newName, String newPhoneNumber, String newPassword, int permission)
        {
            User u = new User(newName, newPhoneNumber, newPassword, permission);
            this.theUser.Add(u);

        }


        public List<String> getInfoOfAllUsers()
        {
            List<String> temp;
            temp = new List<string>();
            foreach (User u in this.theUser) { temp.Add(u.getInfo()); }
            return temp;
        }

        public void AddComplaint(String newComplaint)
        {
            this.theComplaint.Add(new Complaint(newComplaint));
        }



        public void RemoveComplaint(int index)
        {
            if (index >= 0 && index < theComplaint.Count)
            {
                theComplaint.RemoveAt(index);

            }
        }

        public void AddStudentAnnoncement(String newAnnoncement)
        {
            this.theStudentAnnouncement.Add(new Announcement(newAnnoncement));
        }

        public void AddEmployeeAnnoncement(String newAnnoncement)
        {
            this.theEmployeeAnnouncement.Add(new Announcement(newAnnoncement));
        }

        public void AddTask(String newTask)
        {
            this.theTask.Add(new Task(newTask));
        }


        public void RemoveStudentAnnoncement(int index)
        {
            if (index >= 0 && index < theStudentAnnouncement.Count)
            {
                theStudentAnnouncement.RemoveAt(index);

            }
        }

        public void RemoveAnnoncement(int index)
        {
            if (index >= 0 && index < theEmployeeAnnouncement.Count)
            {
                theEmployeeAnnouncement.RemoveAt(index);

            }
        }
        public void AddHouseRule(String newHouseRule)
        {
            this.theHouseRule.Add(new HouseRule(newHouseRule));
        }



        public void RemoveHouseRule(int index)
        {
            if (index >= 0 && index < theHouseRule.Count)
            {
                theHouseRule.RemoveAt(index);

            }
        }

        public void RemoveTask(int index)
        {
            if (index >= 0 && index < theTask.Count)
            {
                theTask.RemoveAt(index);

            }
        }

    }
}
