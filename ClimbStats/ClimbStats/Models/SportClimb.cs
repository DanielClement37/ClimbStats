using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ClimbStats.Models
{
    [Table("SportClimbs")]
    public class SportClimb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime SendDate { get; set; }
        [MaxLength(5)]
        public string Grade { get; set; }
        public int NumAttempts { get; set; }
        public bool IsOutdoors { get; set; }
    }
}
