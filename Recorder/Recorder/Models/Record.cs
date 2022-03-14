using System.ComponentModel.DataAnnotations.Schema;

namespace Recorder.Models
{
    public class Record
    {
        private int id;
        private int randomInt;
        private string time;
        private static Random random = new Random();


        public Record()
        {
            // Id = StaticId++;
            // this line automatically run by SQL with this syntax: Identity (1, 1)
            RandomInt = random.Next(1, 1000);
            Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public int Id
        {
            get => id;
            set => id = value;
        }

        public int RandomInt
        {
            get => randomInt;
            set => randomInt = value;
        }


        [Column(TypeName = "varchar(20)")]
        public string Time
        {
            get => time;
            set => time = value;
        }

    }
}
