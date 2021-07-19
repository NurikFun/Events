using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class ProcessEventArgs : EventArgs
    {
        public bool isSuccessfull { get; set; }
        public DateTime CompletedTime { get; set; }
    }


    class ProcessBusinessLogic
    {

        public event EventHandler<ProcessEventArgs> ProcessCompleted;

        public void StartProcess()
        {
            Console.WriteLine("Process started...");

            try
            {
                OnProcessCompleted(new ProcessEventArgs { isSuccessfull = true, CompletedTime = DateTime.Now });
            }
            catch
            {
                OnProcessCompleted(new ProcessEventArgs { isSuccessfull = false, CompletedTime = DateTime.Now });
            }
            
        }

        protected virtual void OnProcessCompleted(ProcessEventArgs e)
        {
            ProcessCompleted?.Invoke(this, e);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            ProcessBusinessLogic bl = new ProcessBusinessLogic();

            bl.ProcessCompleted += bl_ProcessCompleted;

            bl.StartProcess();

            Console.ReadKey();
        }

        static void bl_ProcessCompleted(object sender, ProcessEventArgs e)
        {
            Console.WriteLine("The process is" + (e.isSuccessfull? " completed" : " broken for some reason") + " and time is " + e.CompletedTime);
        }

    }
}
