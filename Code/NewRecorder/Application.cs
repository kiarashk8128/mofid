using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder
{
    public class Application : IApplication
    {
        ILogic _logic;
        public Application(ILogic logic)
        {
            _logic = logic;

        }
        public void Run()
        {
            _logic.Process();
        }
    }
}
