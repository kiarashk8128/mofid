using Recorder.Data;
using Recorder.Models;
using System;
using System.Linq;
using System.Threading;



namespace Recorder
{
    public class Program
    {
        static void Main(string[] args)
        {
            using RecorderContext context = new RecorderContext();
            IRecorderAction writeRecords = new RecordWriter();
            RecordWriteService record = new RecordWriteService(writeRecords);
            record.Execute(context);
            Console.ReadLine();
            //int time = 0;
            //while (time < 21)
            //{

            //    Records now = new Records()
            //    {


            //    };
            //    context.Add(now);
            //    context.SaveChanges();
            //    var records = from record in context.MofidRecorder
            //                  select record;
            //    foreach (Records r in records)
            //    {
            //        Console.WriteLine($"Id:     {r.EntryId}");
            //        Console.WriteLine($"Timer   {r.Timer}");
            //        Console.WriteLine($"Random     {r.Random}");
            //        Console.WriteLine(new String('-', 20));
            //    }
            //    Thread.Sleep(5000);
            //    time += 5;
            //}
        }
    }
    public interface IRecorderAction
    {
        void Write(RecorderContext context);
    }
    public class RecordWriteService 
    {
        IRecorderAction record;
        public RecordWriteService(IRecorderAction recorderAction)
        {
            record = recorderAction;
        }
        public void Execute(RecorderContext context)
        {
            Console.WriteLine("Writing Records");
            record.Write(context);
            
        }
    }
    public class RecordWriter:IRecorderAction
    {
        public void Write(RecorderContext context)
        {
            int time = 0;
            while (time < 21)
            {

                Records now = new Records()
                {


                };
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
