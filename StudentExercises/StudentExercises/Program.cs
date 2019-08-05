using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercises;
using StudentExercises.Controllers;
using StudentExercises.Models;

namespace StudentExercises
{
    class Program
    {
        static void Main(string[] args)
        {

            // GET ALL EXERCISES
            ExercisesController exercisesController = new ExercisesController();

            var exercises = exercisesController.GetAllExercises();

            exercises.ForEach(exercise =>
            {
                Console.WriteLine($"{exercise.Id}: {exercise.ExerciseName} -- {exercise.ExerciseLanguage}");
                Console.WriteLine(" ");
            });

            Pause();

            // Find all the exercises in the database where the language is JavaScript.
            var selectExercises = exercisesController.GetExerciseByLanguage("C#");

            selectExercises.ForEach(exercise =>
            {
                Console.WriteLine($"{exercise.Id}: {exercise.ExerciseName} -- {exercise.ExerciseLanguage}");
                Console.WriteLine(" ");
            });

            Pause();

            // Insert a new exercise into the database.
            Exercise exerciseToAdd = new Exercise
            {
                ExerciseName = "Personal Website",
                ExerciseLanguage = "ReactJS"
            };

            //exercisesController.AddExercise(exerciseToAdd);
            Pause();

            // Insert a new instructor into the database. Assign the instructor to an existing cohort.
            InstructorsController instructorsController = new InstructorsController();
            Instructor instructorToAdd = new Instructor
            {
                FirstName = "Todd",
                LastName = "Packer",
                SlackHandle = "@tpDaddy",
                CohortId = 2,
                Specialty = "Hitting on Women"
            };

            instructorsController.AddInstructor(instructorToAdd);
            Pause();

            // Assign an existing exercise to an existing student.
            AssignmentsController assignmentsController = new AssignmentsController();
            Assignment assignmentToAssign = new Assignment
            {
                StudentId = 1,
                InstructorId = 2,
                ExerciseId = 3
            };

            assignmentsController.AssignExercise(assignmentToAssign);
            Pause();

            // Find all the students in the database. Include each student's cohort AND each student's list of exercises.
            StudentsController studentsController = new StudentsController();
            var students = studentsController.GetAllStudents();

            // HOW TO ONLY SHOW STUDENTS ONCE???
            students.ForEach(student =>
            {
                
                Console.WriteLine($"{student.Id}: {student.FirstName}{student.LastName} [{student.cohort.Title}] -- ");
                Console.WriteLine("Exercises:");
                exercises.ForEach(ex =>
                {
                    Console.WriteLine($"{ex.ExerciseName} -- {ex.ExerciseLanguage}");
                });
            });

            Pause();
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
