using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercises;

namespace StudentExercises
{
    class Program
    {
        static void Main(string[] args)
        {

            // GET ALL EXERCISES
            Repository repository = new Repository();

            var exercises = repository.GetAllExercises();

            exercises.ForEach(exercise =>
            {
                Console.WriteLine($"{exercise.Id}: {exercise.ExerciseName} -- {exercise.ExerciseLanguage}");
                Console.WriteLine(" ");
            });

            Pause();

            // Find all the exercises in the database where the language is JavaScript.
            var selectExercises = repository.GetExerciseByLanguage("C#");

            selectExercises.ForEach(exercise =>
            {
                Console.WriteLine($"{exercise.Id}: {exercise.ExerciseName} -- {exercise.ExerciseLanguage}");
                Console.WriteLine(" ");
            });

            Pause();

            // Insert a new exercise into the database.
               

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
