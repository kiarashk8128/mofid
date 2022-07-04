
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;



namespace Recorder.Actions
{
    public class RecordWriter : IRecordWriter
    {
        public void Write(RecorderContext context)
        {
            int time = 0;
            while (time < 21)
            {

                Records now = new Records()
                {


                };
                List<Records> recordList = new List<Records>();
                recordList.Add(now);
                context.Add(now);
                context.SaveChanges();
                var records = from record in context.MofidRecorder
                              select record;
                foreach (Records r in records)
                {
                    Console.WriteLine($"Id:     {r.EntryId}");
                    Console.WriteLine($"Timer   {r.Timer}");
                    Console.WriteLine($"Random     {r.Random}");
                    Console.WriteLine(new String('-', 20));
                }
                Thread.Sleep(5000);
                time += 5;
            }

        }
    }
}
