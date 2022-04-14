using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proba
{
    public partial class Form1 : Form
    {
        private StudentHouse sh;
        
        
        public List<string> linesHouseRules = File.ReadAllLines(@"..\..\myData\HouseRules.txt").ToList();
        public List<string> linesStudent = File.ReadAllLines(@"..\..\myData\AnnouncementsStudents.txt").ToList();
        public List<string> linesEmployee = File.ReadAllLines(@"..\..\myData\AnnouncementsEmployees.txt").ToList();
        public List<string> linesComplaints = File.ReadAllLines(@"..\..\myData\ComplaintsStudents.txt").ToList();
        public List<string> linesTasks = File.ReadAllLines(@"..\..\myData\Tasks.txt").ToList();
        private int allowed; //we use this variable to check is the user can be allowed to access certain features in the app. 
                         //1 means that he/she is a student, 2 employee, 3 the special employee that solves complaints. It is 0 if the access is denied.
        User s1, s2, s3;
        private Panel selectedPanel;

        public Form1()
        {

            InitializeComponent();

            this.Text = "ABA Accommodation";

            this.sh = new StudentHouse();
            this.allowed = 0;
            this.pnlMenu.Hide();


            this.sh.AddUser("Ana", "376298", "student", 1);
            this.sh.AddUser("Ante", "579321", "student", 1);
            this.sh.AddUser("Bianca", "12090", "student", 1);
            this.sh.AddUser("Bert", "687990", "employee", 2);
            this.sh.AddUser("Zhao Qin", "9280", "employee", 2);
            this.sh.AddUser("George", "9281", "employee", 3);
         

            foreach (string line in linesComplaints)
            {
                this.sh.AddComplaint(line);
            }
            foreach (string line in linesStudent)
            {
                this.sh.AddStudentAnnoncement(line);
            }

            foreach (string line in linesEmployee)
            {
                this.sh.AddEmployeeAnnoncement(line);
            }

            foreach (string line in linesHouseRules)
            {
                this.sh.AddHouseRule(line);
            }

            foreach (string line in linesTasks)
            {
                this.sh.AddTask(line);
            }

            List<User> users;
            users = this.sh.getAllUsers();
            this.s1 = users[0];
            this.s2 = users[1];
            this.s3 = users[2];
            DataTable points = new DataTable();
            points.Columns.Add("Student");
            points.Columns.Add("Points");

            DataRow info = points.NewRow();
            info["Student"] = s1.Name;
            info["Points"] = Convert.ToString(s1.NrOfPoints);
            points.Rows.Add(info);

            info = points.NewRow();
            info["Student"] = s2.Name;
            info["Points"] = Convert.ToString(s2.NrOfPoints);
            points.Rows.Add(info);

            info = points.NewRow();
            info["Student"] = s3.Name;
            info["Points"] = Convert.ToString(s3.NrOfPoints);
            points.Rows.Add(info);

            foreach (DataRow dataRow in points.Rows)
            {
                int num = dataGridView2.Rows.Add();
                dataGridView2.Rows[num].Cells[0].Value = dataRow["Student"].ToString();
                dataGridView2.Rows[num].Cells[1].Value = dataRow["Points"].ToString();
            }

            rbStudent1.Text = s1.Name;
            rbStudent2.Text = s2.Name;
            rbStudent3.Text = s3.Name;

            cbStudentsName.Items.Add(s1.Name);
            cbStudentsName.Items.Add(s2.Name);
            cbStudentsName.Items.Add(s3.Name);

            cbCategory.Items.Add("Cleaning");
            cbCategory.Items.Add("Buying");
            cbCategory.Items.Add("Garbage");

            pnlMenu.Hide();
            pnlContact.Hide();
            this.selectedPanel = this.pnlHomePage;
            ShowPanel(this.pnlLogIn);
            Point p = new Point(150, 10);
            this.pnlLogIn.Location = p;
            Size s = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - 10);
            this.pnlLogIn.Size = s;

            btnHome.Visible = false;
            btnRules.Visible = false;
            btnTasks.Visible = false;
            btnAnnouncements.Visible = false;
            btnComplaints.Visible = false;

            this.checkAllow(this.allowed);

            ShowInfoAllEmployeeAnnouncements();
            ShowInfoAllStudentAnnouncements();
            ShowInfoAllHouseRules();
            ShowInfoOfAllComplaints();
            ShowInfoAllTasks();
        }

        private void ShowPanel(Panel p)
        {
            this.selectedPanel.Visible = false;
            this.selectedPanel = p;
            this.selectedPanel.Visible = true;
        }

        //house rules
        private void ShowInfoAllHouseRules()
        {
            this.lbRules.Items.Clear();
            foreach (HouseRule r in this.sh.GetAllHouseRules())
            {
                this.lbRules.Items.Add(r.GetHouseRule());
            }

        }

        public void VisibilityHouseRules(int allowed)
        {
            if (allowed == 1)
            {
                this.btnAddRule.Visible = false;
                this.btnRemoveAt.Visible = false;
                this.btnUpdateRule.Visible = false;
                this.tbRule.Visible = false;
                this.tbUpdatedRule.Visible = false;
                this.lblRule.Visible = false;
            }
        }

        public void VisibilityTasks(int allowed)
        {
            if (allowed == 1)
            {
                this.rbStudent1.Visible = false;
                this.rbStudent2.Visible = false;
                this.rbStudent3.Visible = false;
                this.btnAddPoint.Visible = false;
                tbTask.Visible = false;
                tbDueDate.Visible = false;
                cbCategory.Visible = false;
                cbStudentsName.Visible = false;
                btnSubmitTask.Visible = false;
                lbTasks.Location = new Point(140, 104);
                lbTasks.Size = new Size(624, 244);
                lblTasksOv.Location = new Point(373, 87);
                lblTasksOv.Size = new Size(96, 16);
                lblTasks.Visible = false;
                lblStudentName.Visible = false;
                lblDuteDate.Visible = false;
                lblCategory.Visible = false;
                btnTaskCompleted.Visible = false;
            }
            else
            {
                lbTasks.Location = new Point(393, 88);
                lbTasks.Size = new Size(489, 244);
            }
        }

        public void VisibilityAnnouncements (int allowed)
        {
            if (allowed == 2 || allowed == 3)
            {
                this.btnAddStudentAnnouncement.Visible = false;
                this.btnRemoveStudentAnnoncement.Visible = false;
                this.btnUpdateAnnouncement.Visible = false;
                this.tbStudentAnnouncement.Visible = false;
                this.tbUpdateStudentAnnouncement.Visible = false;
                this.lblStudent.Visible = false;
            }
            else if (allowed == 1)
            {
                this.btnAddEmployeeAnnouncement.Visible = false;
                this.btnRemoveEmployeeAnnouncement.Visible = false;
                this.btnUpdateEmployeeAnnouncement.Visible = false;
                this.tbEmployeeAnnouncement1.Visible = false;
                this.tbUpdateEmployeeAnnouncements.Visible = false;
                this.lblEmployee.Visible = false;
            }
        }

        public void VisibilityComplaints (int allowed)
        {
            if (allowed == 2)
            {
                this.btnAddComplaint.Visible = false;
                this.btnRemoveComplaint.Visible = false;
                this.btnUpdateComplaint.Visible = false;
                this.lblPostComplaint.Visible = false;
                this.tbComplaint.Visible = false;
                this.tbUpdateComplaint.Visible = false;
                this.btnSolved.Visible = false;
            }
            else
            if (allowed == 3)
            {
                this.btnAddComplaint.Visible = false;
                this.btnRemoveComplaint.Visible = false;
                this.btnUpdateComplaint.Visible = false;
                this.lblPostComplaint.Visible = false;
                this.tbComplaint.Visible = false;
                this.tbUpdateComplaint.Visible = false;
            }
            else
            {
               this.btnSolved.Visible = false;
            }
        }

        private void checkAllow(int isAllowed)
        {
            if (isAllowed == 0)
            {
                //  The login page is on the form, in the lines below, we hide every other panel/user control/button until the user adds the login details
                pnlMenu.Hide();
                pnlHomePage.Hide();
                pnlHouseRules.Hide();
                pnlComplaints.Hide();
                pnlAnnouncements.Hide();
                pnlTasks.Hide();
                btnHome.Hide();
                btnRules.Hide();
                btnTasks.Hide();
                btnAnnouncements.Hide();
                btnComplaints.Hide();
                btnLogOut.Hide();
            }
            else
            {
                btnHome.Show();
                btnRules.Show();
                btnTasks.Show();
                btnAnnouncements.Show();
                btnComplaints.Show();
                btnLogOut.Show();
                pnlHomePage.Show();
            }


        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            String username = this.tbUsername.Text;
            String password = this.tbPassword1.Text;
            int id = Convert.ToInt32(this.tbId.Text);
            User u = this.sh.GetUserNamePasswordId(username, password, id);

            if (tbUsername.Text == "")
            {
                lblInfo.Text = "Please enter your username";
                this.allowed = 0;
            }
            else
            if (tbPassword1.Text == "")
            {
                lblInfo.Text = "Please enter your password";
                this.allowed = 0;
            }
            else
            if(tbId.Text == "")
            {
                lblInfo.Text = "Please enter your ID";
            }
            else
            if (u != null)
            {   MessageBox.Show($"Hello, {username}! Logged in succesfully as {password}!");
                        this.allowed = u.Permission; 
                        this.lblInfo.Visible = false;
                        this.lblPassword.Visible = false;
                        this.lblUsername.Visible = false;
                        pnlMenu.Visible = true;
                        lblEnterInfo.Visible = false;
                        pnlLogIn.Hide();
                        pnlHomePage.Show();
                        btnHome.Visible = true;
                        Point p = new Point(150, 10);
                        this.pnlHomePage.Location = p;
                        Size s = new Size(this.ClientSize.Width - 1, this.ClientSize.Height - 1);
                        this.pnlHomePage.Size = s;
                        this.selectedPanel = this.pnlHomePage;
                        pnlTop.BackColor = Color.Black;
                        this.BackColor = Color.Black;
            }
            else
            {
                MessageBox.Show("You are not allowed.");
                allowed = 0;
            }
              
            this.checkAllow(this.allowed);
            this.tbUsername.Clear();
            this.tbPassword1.Clear();
            this.tbId.Clear();
        }



        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //add a house rule
        private void btnAddRule_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                String newHouseRule = tbRule.Text;
                linesHouseRules.Add(newHouseRule);
                File.WriteAllLines(@"..\..\myData\HouseRules.txt", linesHouseRules);
                this.sh.AddHouseRule(newHouseRule);
                ShowInfoAllHouseRules();
                this.tbRule.Clear();

            }
            else
            {
                MessageBox.Show("You are not allowed to add a rule!");
            }
        }

        //remove a house rule
        private void btnRemoveAt_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                int existingHouseRule = this.lbRules.SelectedIndex;
                this.sh.RemoveHouseRule(existingHouseRule);
                tbUpdatedRule.Clear();
                ShowInfoAllHouseRules();
            }
            else
            {
                MessageBox.Show("You are not allowed to remove a rule!");
            }
        }

        

        //update a rule
        private void btnUpdateRule_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                int index = this.lbRules.SelectedIndex;
                String updatedRule = tbUpdatedRule.Text;
                lbRules.Items.RemoveAt(index);
                lbRules.Items.Insert(index, updatedRule);
            }
            else
            {
                MessageBox.Show("You are not allowed to update a rule!");
            }
        }

        private void btnAddComplaint_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                String newComplaint = tbComplaint.Text;
                linesComplaints.Add(newComplaint);
                File.WriteAllLines(@"..\..\myData\ComplaintsStudents.txt", linesComplaints);
                sh.AddComplaint(newComplaint);
                ShowInfoOfAllComplaints();
                this.tbComplaint.Clear();
            }
        }

        private void btnRemoveComplaint_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int existingComplaint = this.lbComplaint.SelectedIndex;
                this.sh.RemoveComplaint(existingComplaint);
                ShowInfoOfAllComplaints();
            }
        }

        private void lbComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tbUpdateComplaint.Text = Convert.ToString(lbComplaint.SelectedItem);
            }

            catch
            {

            }
        }

        private void btnUpdateComplaint_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int index = lbComplaint.SelectedIndex;
                String updatedComplaint = tbUpdateComplaint.Text;
                lbComplaint.Items.RemoveAt(index);
                lbComplaint.Items.Insert(index, updatedComplaint);
            }
        }

        private void btnSolved_Click(object sender, EventArgs e)
        {
            if (allowed == 3)
            {
                int existingComplaint = this.lbComplaint.SelectedIndex;
                this.sh.RemoveComplaint(existingComplaint);
                ShowInfoOfAllComplaints();
            }
        }


        private void btnAnnouncements_Click_1(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
            btnTasks.BackColor = Color.Black;
            btnAnnouncements.BackColor = Color.Gray;
            btnComplaints.BackColor = Color.Black;
            btnRules.BackColor = Color.Black;
            btnContact.BackColor = Color.Black;
            ShowPanel(this.pnlAnnouncements);
            Point p = new Point(150, 10);
            this.pnlAnnouncements.Location = p;
            Size s = new Size(this.ClientSize.Width - 150, this.ClientSize.Height - 20);
            this.pnlAnnouncements.Size = s;

            VisibilityAnnouncements(allowed);
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Gray;
            btnTasks.BackColor = Color.Black;
            btnAnnouncements.BackColor = Color.Black;
            btnComplaints.BackColor = Color.Black;
            btnRules.BackColor = Color.Black;
            btnContact.BackColor = Color.Black;
            ShowPanel(pnlHomePage);

            Point p = new Point(150, 10);
            this.pnlHomePage.Location = p;
            Size s = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - 1);
            this.pnlHomePage.Size = s;

        }

        private void btnRules_Click_1(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
            btnTasks.BackColor = Color.Black;
            btnAnnouncements.BackColor = Color.Black;
            btnComplaints.BackColor = Color.Black;
            btnRules.BackColor = Color.Gray;
            btnContact.BackColor = Color.Black;
            ShowPanel(this.pnlHouseRules);

            Point p = new Point(150, 10);
            this.pnlHouseRules.Location = p;
            Size s = new Size(this.ClientSize.Width - 150, this.ClientSize.Height - 20);
            this.pnlHouseRules.Size = s;

            VisibilityHouseRules(allowed);
            ShowInfoAllHouseRules();
        }

        private void btnTasks_Click_1(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
            btnTasks.BackColor = Color.Gray;
            btnAnnouncements.BackColor = Color.Black;
            btnComplaints.BackColor = Color.Black;
            btnRules.BackColor = Color.Black;
            btnContact.BackColor = Color.Black;
            ShowPanel(this.pnlTasks);
            Point p = new Point(150, 10);
            this.pnlTasks.Location = p;
            Size s = new Size(this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            this.pnlTasks.Size = s;

            VisibilityTasks(allowed);

        }

        private void btnComplaints_Click_1(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
            btnTasks.BackColor = Color.Black;
            btnAnnouncements.BackColor = Color.Black;
            btnComplaints.BackColor = Color.Gray;
            btnRules.BackColor = Color.Black;
            btnContact.BackColor = Color.Black;
            ShowPanel(this.pnlComplaints);
            Point p = new Point(150, 10);
            this.pnlComplaints.Location = p;
            Size s = new Size(this.ClientSize.Width - 150, this.ClientSize.Height - 20);
            this.pnlComplaints.Size = s;

            VisibilityComplaints(allowed);
            ShowInfoOfAllComplaints();

        }

        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
            btnTasks.BackColor = Color.Black;
            btnAnnouncements.BackColor = Color.Black;
            btnComplaints.BackColor = Color.Black;
            btnRules.BackColor = Color.Black;
            btnContact.BackColor = Color.Gray;
            ShowPanel(this.pnlContact);
            Point p = new Point(150, 10);
            this.pnlContact.Location = p;
            Size s = new Size(this.ClientSize.Width - 150, this.ClientSize.Height - 20);
            this.pnlContact.Size = s;
        }


        private void btnAddPoint_Click_1(object sender, EventArgs e)
        {
            if (rbStudent1.Checked)
            {
                s1.AddPoint();
                dataGridView2.Rows[0].Cells[1].Value = s1.NrOfPoints.ToString();
                rbStudent1.Checked = false;


            }
            else
                if (rbStudent2.Checked)
            {
                s2.AddPoint();
                dataGridView2.Rows[1].Cells[1].Value = s2.NrOfPoints.ToString();
                rbStudent2.Checked = false;

            }
            else
                if (rbStudent3.Checked)
            {
                s3.AddPoint();
                dataGridView2.Rows[2].Cells[1].Value = s3.NrOfPoints.ToString();
                rbStudent3.Checked = false;
            }
            else
            {
                MessageBox.Show("Sorry, you have to select a student");
            }
        }

        private void ShowInfoAllStudentAnnouncements()
        {
            this.lbStudentAnnouncements.Items.Clear();
            foreach (Announcement an in this.sh.GetAllStudentAnnoncements())
            {
                this.lbStudentAnnouncements.Items.Add(an.GetInfo());
            }
        }

        private void btnAddStudentAnnouncement_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                String newAnnoncementToAdd = this.tbStudentAnnouncement.Text;
                linesStudent.Add(newAnnoncementToAdd);
                File.WriteAllLines(@"..\..\myData\AnnouncementsStudents.txt", linesStudent);
                this.sh.AddStudentAnnoncement(newAnnoncementToAdd);
                ShowInfoAllStudentAnnouncements();
            }
        }

        private void btnRemoveStudentAnnoncement_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int index = this.lbStudentAnnouncements.SelectedIndex;
                tbUpdateStudentAnnouncement.Clear();
                this.lbStudentAnnouncements.Items.RemoveAt(index);
                this.sh.GetAllStudentAnnoncements().RemoveAt(index);
            }
        }

        private void lbStudentAnnouncements_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tbUpdateStudentAnnouncement.Text = Convert.ToString(lbStudentAnnouncements.SelectedItem);
            }

            catch
            {

            }
        }

        private void btnUpdateAnnouncement_Click(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int index = lbStudentAnnouncements.SelectedIndex;
                String updatedAnnouncement = tbUpdateStudentAnnouncement.Text;
                lbStudentAnnouncements.Items.RemoveAt(index);
                lbStudentAnnouncements.Items.Insert(index, updatedAnnouncement);
            }
        }

        private void ShowInfoAllEmployeeAnnouncements()
        {
            this.lbEmployeeAnnouncements.Items.Clear();
            foreach (Announcement ann in this.sh.GetAllEmployeeAnnoncements())
            {
                this.lbEmployeeAnnouncements.Items.Add(ann.GetInfo());
            }
        }

        private void btnAddEmployeeAnnouncement_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                String newAnnoncementToAdd = this.tbEmployeeAnnouncement1.Text;
                linesEmployee.Add(newAnnoncementToAdd);
                File.WriteAllLines(@"..\..\myData\AnnouncementsEmployees.txt", linesEmployee);
                this.sh.AddEmployeeAnnoncement(newAnnoncementToAdd);
                ShowInfoAllEmployeeAnnouncements();
            }
        }

        private void btnRemoveEmployeeAnnouncement_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                int index = this.lbEmployeeAnnouncements.SelectedIndex;
                this.lbEmployeeAnnouncements.Items.RemoveAt(index);
                tbUpdateEmployeeAnnouncements.Clear();
                this.sh.GetAllEmployeeAnnoncements();
            }
        }

        private void lbEmployeeAnnouncements_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.tbUpdateEmployeeAnnouncements.Text = Convert.ToString(lbEmployeeAnnouncements.SelectedItem);
            }

            catch
            {

            }
        }

        private void btnUpdateEmployeeAnnouncement_Click(object sender, EventArgs e)
        {
            if (allowed == 2)
            {
                int index = lbEmployeeAnnouncements.SelectedIndex;
                String updatedAnnouncement = tbUpdateStudentAnnouncement.Text;
                lbEmployeeAnnouncements.Items.RemoveAt(index);
                lbEmployeeAnnouncements.Items.Insert(index, updatedAnnouncement);
            }
        }

        private void btnAddComplaint_Click_1(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                String newComplaint = tbComplaint.Text;
                linesComplaints.Add(newComplaint);
                File.WriteAllLines(@"..\..\myData\ComplaintsStudents.txt", linesComplaints);
                this.sh.AddComplaint(newComplaint);
                ShowInfoOfAllComplaints();
                this.tbComplaint.Clear();
            }
        }

        private void btnRemoveComplaint_Click_1(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int existingComplaint = this.lbComplaint.SelectedIndex;
                this.sh.RemoveComplaint(existingComplaint);
                tbUpdateComplaint.Clear();
                ShowInfoOfAllComplaints();
            }
        }

        private void btnSolved_Click_1(object sender, EventArgs e)
        {
            if (allowed == 3)
            {
                int existingComplaint = this.lbComplaint.SelectedIndex;
                this.sh.RemoveComplaint(existingComplaint);
                ShowInfoOfAllComplaints();
            }
        }

        private void lbComplaint_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                tbUpdateComplaint.Text = Convert.ToString(lbComplaint.SelectedItem);
            }

            catch
            {

            }
        }

        private void btnUpdateComplaint_Click_1(object sender, EventArgs e)
        {
            if (allowed == 1)
            {
                int index = lbComplaint.SelectedIndex;
                String updatedComplaint = tbUpdateComplaint.Text;
                lbComplaint.Items.RemoveAt(index);
                lbComplaint.Items.Insert(index, updatedComplaint);
            }
        }

        private void btnSubmitTask_Click(object sender, EventArgs e)
        {
            String taskToDo;
            String studentName = Convert.ToString(cbStudentsName.SelectedItem);
            String newTask = tbTask.Text;
            String dueDate = tbDueDate.Text;
            String taskType = Convert.ToString(cbCategory.SelectedItem);
            taskToDo = studentName + ": " + newTask + " until " + dueDate + ", category: " + taskType;
            linesTasks.Add(taskToDo);
            File.WriteAllLines(@"..\..\myData\Tasks.txt", linesTasks);
            this.sh.AddTask(taskToDo);
            ShowInfoAllTasks();
            tbTask.Clear();
            tbDueDate.Clear();
            cbCategory.SelectedIndex = -1;
            cbStudentsName.SelectedIndex = -1;

        }

        private void btnTaskCompleted_Click(object sender, EventArgs e)
        {
            int existingTask = this.lbTasks.SelectedIndex;
            String task = Convert.ToString(lbTasks.SelectedItem);
            this.sh.RemoveTask(existingTask);
            ShowInfoAllTasks();

            if(task.Contains(s1.Name))
            {
                s1.AddPoint();
                dataGridView2.Rows[0].Cells[1].Value = s1.NrOfPoints.ToString();
            }
            else
                if (task.Contains(s2.Name))
            {
                s2.AddPoint();
                dataGridView2.Rows[1].Cells[1].Value = s2.NrOfPoints.ToString();

            }
            else
                if (task.Contains(s3.Name))
            {
                s3.AddPoint();
                dataGridView2.Rows[2].Cells[1].Value = s3.NrOfPoints.ToString();
            }
        }

        private void lbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tbUpdatedRule.Text = Convert.ToString(lbRules.SelectedItem);
            }

            catch
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //show info of all complaints
        private void ShowInfoOfAllComplaints()
        {
            this.lbComplaint.Items.Clear();
            foreach (Complaint c in this.sh.GetAllComplaints())
            {
                lbComplaint.Items.Add(c.GetComplaint());
            }
        }

        private void ShowInfoAllTasks()
        {
            this.lbTasks.Items.Clear();
            foreach(Task t in this.sh.GetAllTasks())
            {
                lbTasks.Items.Add(t.GetTask());
            }
        }
    }
}
