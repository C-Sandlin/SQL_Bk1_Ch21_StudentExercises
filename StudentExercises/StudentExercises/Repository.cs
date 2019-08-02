using StudentExercises.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercises
{
    public class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, ExerciseName, ExerciseLanguage FROM Exercise";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while(reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("ExerciseName");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int languageColumnPosition = reader.GetOrdinal("ExerciseLanguage");
                        string languageValue = reader.GetString(languageColumnPosition);

                        Exercise exercise = new Exercise()
                        {
                            Id = idValue,
                            ExerciseName = nameValue,
                            ExerciseLanguage = languageValue
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }

        public List<Exercise> GetExerciseByLanguage(string lang)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, ExerciseName, ExerciseLanguage 
                                      FROM Exercise
                                      WHERE ExerciseLanguage = @language
                                      ";

                    cmd.Parameters.Add(new SqlParameter ("@language", lang));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("ExerciseName");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int languageColumnPosition = reader.GetOrdinal("ExerciseLanguage");
                        string languageValue = reader.GetString(languageColumnPosition);

                        Exercise exercise = new Exercise()
                        {
                            Id = idValue,
                            ExerciseName = nameValue,
                            ExerciseLanguage = languageValue
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }

        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Exercise(ExerciseName, ExerciseLanguage) VALUES('{exercise.ExerciseName}', '{exercise.ExerciseLanguage}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
