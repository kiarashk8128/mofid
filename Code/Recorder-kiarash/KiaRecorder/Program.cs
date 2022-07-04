using KiaRecorder.Data;
using KiaRecorder.Data.KiarashRecorder.Data;
using KiaRecorder.Models;
namespace KiarashRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            using KiaRecorderContext context = new KiaRecorderContext();
          
            int time = 0;
            while (time < 21)
            {
                
                Recorder now = new Recorder()
                {
                   
                    
                };
                context.Add(now);
                context.SaveChanges();
                var records = from record in context.Reecorders
                              select record;
                foreach (Recorder r in records)
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

