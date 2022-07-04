using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder
{
    public class Records
    {
        [Key]
        public int EntryId { get; set; }
        [Required]
        private DateTime timer = DateTime.Now;
        public DateTime Timer { get { return timer; } set { } }

        private int random = new Random().Next(1, 1000);
        public int Random { get { return random; } set { } }
        private List<Records> RecordsList { get; set; }
        public List<Records> GetRecordsList()
        {
            List<Records> RecordsList = new List<Records>();
            return RecordsList;

        }
    }
}
