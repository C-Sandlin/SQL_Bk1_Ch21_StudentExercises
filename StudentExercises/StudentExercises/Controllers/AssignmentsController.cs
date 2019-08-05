using StudentExercises.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercises.Controllers
{
    public class AssignmentsController
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public void AssignExercise(Assignment assignment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Assignment (StudentId, InstructorId, ExerciseId) VALUES('{assignment.StudentId}', '{assignment.InstructorId}', '{assignment.ExerciseId}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
    
}
