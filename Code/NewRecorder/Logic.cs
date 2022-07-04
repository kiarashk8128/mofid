using Recorder;
using Recorder.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder
{
    public class Logic : ILogic
    {
        IRecordWriter _recordWriter;
        public Logic(IRecordWriter recordWriter )
        {
            _recordWriter = recordWriter;

        }
        public void Process()
        {
            using RecorderContext context = new RecorderContext();
            _recordWriter.Write(context);
            Console.ReadLine();
        }
    }
}
