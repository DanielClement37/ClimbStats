using SQLite;
using System;

namespace ClimbStats.Models
{
    [Table("Boulders")]
    public class Boulder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime SendDate { get; set; }

        [MaxLength(5)]
        public string GradeText { get; set; }

        public int GradeInt { get; set; }

        public int NumAttempts { get; set; }
        public bool IsOutdoors { get; set; }
    }
}