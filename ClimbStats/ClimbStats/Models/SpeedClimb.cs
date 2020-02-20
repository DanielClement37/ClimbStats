using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ClimbStats.Models
{
    [Table("SpeedAttempts")]
    public class SpeedClimb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime SendDate { get; set; }
        
        public double? SendTime { get; set; }
        public bool Topped { get; set; }
    }
}
