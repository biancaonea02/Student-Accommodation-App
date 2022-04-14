using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proba
{
    public class Task
    {
        private String task;

        public Task(String newTask)
        {
            this.task = newTask;
        }
   
        public String GetTask()
        {
            return this.task;
        }
    }
}
