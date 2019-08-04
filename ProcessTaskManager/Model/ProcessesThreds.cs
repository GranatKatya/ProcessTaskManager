using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessTaskManager.Model
{
    class ProcessesThreds
    {
        public Process[] GetActiveProcesses() {
            return Process.GetProcesses();
        }
    }
}
