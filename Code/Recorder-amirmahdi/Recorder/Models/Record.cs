using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return id; }
            set { id = value; }
        }

        public int RandomInt
        {
            get { return randomInt; }
            set { randomInt = value; }
        }


        [Column(TypeName = "varchar(20)")]
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

    }
}
