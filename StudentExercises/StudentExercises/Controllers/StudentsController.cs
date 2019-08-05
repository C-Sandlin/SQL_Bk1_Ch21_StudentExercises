using Microsoft.AspNet.SignalR.Infrastructure;
using StudentExercises.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentExercises.Controllers
{
    class StudentsController
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Student> GetAllStudents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, s.CohortId, c.Id, c.Title, a.StudentId, a.ExerciseId, a.InstructorId, e.ExerciseName, e.ExerciseLanguage
                                        FROM Student s
                                        JOIN Cohort c
                                        ON s.CohortId = c.Id
                                        JOIN Assignment a
                                        ON s.Id = a.StudentId
                                        JOIN Exercise e 
                                        ON a.ExerciseId = e.Id
                                       ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> Students = new List<Student>();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        int foundStudentId = Students.FindIndex(stu => stu.Id == id);
                        if (foundStudentId == -1)
                        {

                        }

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        int cohortIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortIdValue = reader.GetInt32(cohortIdColumnPosition);

                        int cohortTitleColumnPosition = reader.GetOrdinal("Title");
                        string cohortTitleValue = reader.GetString(cohortTitleColumnPosition);

                        Student student = new Student
                        {
                            Id = id,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                            CohortId = cohortIdValue,
                            cohort = new Cohort
                            {
                                Id = cohortIdValue,
                                Title = cohortTitleValue
                            } 
                        };

                        if (reader.GetString(reader.GetOrdinal("ExerciseName")) != null)
                        {
                            student.exercises.Add(new Exercise()
                            {
                                ExerciseName = reader.GetString(reader.GetOrdinal("ExerciseName")),
                                ExerciseLanguage = reader.GetString(reader.GetOrdinal("ExerciseLanguage"))
                            });
                        }


                        Students.Add(student);
                    }

                    reader.Close();
                    return Students;
                }
            }
        }
    }
}
