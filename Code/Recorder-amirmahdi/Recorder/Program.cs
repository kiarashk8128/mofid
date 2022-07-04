using Recorder.Data;
using Recorder.Models;

namespace Recorder
{
    class Program
    {
        static void Main(string[] args)
        {
            // USE ANY METHOD YOU WANT
            // Create();   // insert into table
            // Read();     // read all data
            // Read(5);    // read data with Id 5
            // Update(5);  // modify RandomInteger and Time of Data with Id 5
            // Delete();   // delete all data
            // Delete(5);  // delete data with Id 5
        }

        private static void Create()
        {
            using RecorderContext recorderContext = new RecorderContext();
            DateTime tik = DateTime.Now;
            DateTime tok;
            TimeSpan interval = new TimeSpan(00, 01, 00);
            while (true)
            {
                Record data = new Record();
                recorderContext.Records.Add(data);
                recorderContext.SaveChanges();
                Console.WriteLine("a new data added!");
                Thread.Sleep(5000);
                tok = DateTime.Now;
                if (tok.Subtract(tik) > interval) break;
            }
        }

        private static void Read()
        {
            using RecorderContext recorderContext = new RecorderContext();
            var Records = from record in recorderContext.Records
                          orderby record.Id
                          select record;

            foreach (var record in Records)
            {

                Console.WriteLine($"Id: {record.Id}");
                Console.WriteLine($"Random Integer: {record.RandomInt}");
                Console.WriteLine($"Time: {record.Time}");
                Console.WriteLine(new String('-', 20));

            }
        }

        private static void Read(int id)
        {
            using RecorderContext recorderContext = new RecorderContext();
            var Record = (from record in recorderContext.Records
                          where record.Id == id
                          select record).FirstOrDefault();

            Console.WriteLine($"Id: {Record.Id}");
            Console.WriteLine($"Random Integer: {Record.RandomInt}");
            Console.WriteLine($"Time: {Record.Time}");
        }

        private static void Update(int id)
        {
            using RecorderContext recorderContext = new RecorderContext();
            var ConsideredRecord = (from record in recorderContext.Records
                                    where record.Id == id
                                    select record).FirstOrDefault();

            if (ConsideredRecord is Record)
            {
                Random random = new Random();   
                ConsideredRecord.RandomInt = random.Next(1, 1000);
                ConsideredRecord.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            recorderContext.SaveChanges();
            Console.WriteLine("data updated!");
        }

        private static void Delete()
        {
            using RecorderContext recorderContext = new RecorderContext();
            var Records = from record in recorderContext.Records
                                   orderby record.Id
                                   select record;

            foreach (var ConsideredRecord in Records)
            {
                if (ConsideredRecord is Record)
                {
                    recorderContext.Records.Remove(ConsideredRecord);
                    Console.WriteLine("data deleted!");
                }
            }
            

            recorderContext.SaveChanges();
        }

        private static void Delete(int id)
        {
            using RecorderContext recorderContext = new RecorderContext();
            var ConsideredRecord = (from record in recorderContext.Records
                                    where record.Id == id
                                    select record).FirstOrDefault();

            if (ConsideredRecord is Record)
            {
                recorderContext.Records.Remove(ConsideredRecord);
            }
            recorderContext.SaveChanges();
            Console.WriteLine("data deleted!");
        }
    }
}