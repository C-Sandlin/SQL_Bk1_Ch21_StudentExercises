using StudentExercises.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercises.Controllers
{
    public class InstructorsController
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        // Find all instructors in the database. Include each instructor's cohort.
        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, SlackHandle, CohortId, Specialty FROM Instructor";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> Instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                        int specialtyColumnPosition = reader.GetOrdinal("Specialty");
                        string specialtyValue = reader.GetString(specialtyColumnPosition);


                        Instructor instructor = new Instructor()
                        {
                            Id = idValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortId = cohortIdValue,
                            Specialty = specialtyValue
                        };

                        Instructors.Add(instructor);
                    }

                    reader.Close();
                    return Instructors;
                }
            }
        }

        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Instructor(FirstName, LastName, SlackHandle, CohortId, Specialty) VALUES('{instructor.FirstName}', '{instructor.LastName}', '{instructor.SlackHandle}', '{instructor.CohortId}', '{instructor.Specialty}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
