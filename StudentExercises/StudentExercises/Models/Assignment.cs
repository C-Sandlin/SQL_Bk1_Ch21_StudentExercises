using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        public int ExerciseId { get; set; }
    }
}
