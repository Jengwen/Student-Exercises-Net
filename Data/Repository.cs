using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using StudentExercisesNet.Models;

namespace StudentExercisesNet.Data
{
    class Repository
    {
        //Create SQL connection
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=StudentExercises2;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                // Note, we must Open() the connection, the "using" block doesn't do that for us.
                conn.Open();

                // We must "use" commands too.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = "SELECT Id, Name FROM Exercise";

                    // Execute the SQL in the database and get a "reader" that will give us access to the data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Exercise> exercises = new List<Exercise>();

                    // Read() will return true if there's more data to read
                    while (reader.Read())
                    {
                        //  For our query, "Id" has an ordinal value of 0 and "Name" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        // Now let's create a new department object using the data from the database.
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = NameValue
                        };

                        // ...and add that department object to our list. C#
                        exercises.Add(exercise);
                    }

                    reader.Close();

                    // Return the list of exercises. C#
                    return exercises;
                }
            }
        }

        public List<Exercise> GetJavaExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = "SELECT Id, Name FROM Exercise WHERE Language LIKE 'Java%'";

                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Exercise> javaExercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        //  For our query, "Id" has an ordinal value of 0 and "Name" is 1.
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);

                        // Now let's create a new department object using the data from the database.
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = NameValue
                        };

                        // ...and add that exercise object to our list. C#
                        javaExercises.Add(exercise);
                    }

                    reader.Close();

                    // Return the list of exercises  C#
                    return javaExercises;
                }
            }

        }
        //Add an exercise to the database
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO Exercise (Name, Language) Values ('{exercise.Name}', '{exercise.Language}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Get list of instructors by Cohort
        public List<Instructor> GetInstructorsByCohort()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = "SELECT Instructor.Id, Instructor.FirstName, Instructor.CohortId, Cohort.Name as 'Cohort' FROM Instructor JOIN Cohort ON CohortId = Cohort.Id ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the departments we retrieve from the database.
                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            Cohort = new Cohort
                            {
                                Name = reader.GetString(reader.GetOrdinal("Cohort"))
                            }
                        };


                        // ...and add that exercise object to our list. C#
                        instructors.Add(instructor);
                    }

                    reader.Close();

                    // Return the list of instructors  C#
                    return instructors;
                }
            }
        }
            //Add an instructor to the database
            public void AddInstructor(Instructor instructor)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        // More string interpolation
                        cmd.CommandText = $"INSERT INTO Instructor (FirstName, LastName, SlackHandle, CohortId) Values ('{instructor.FirstName}', '{instructor.LastName}', '{instructor.SlackHandle}','{instructor.CohortId}')";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        //Assign an existing exercise to an existing student
        public void AddStudentExercise(StudentExercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO StudentExercise (StudentId, ExerciseId) Values ('{exercise.StudentId}', '{exercise.ExerciseId}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Get all Students with Cohort and Exercises
        public List<Student> GetStudentsWithExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Here we setup the command with the SQL we want to execute before we execute it.
                    cmd.CommandText = @"SELECT Student.FirstName, Student.LastName, Cohort.Name AS 'Cohort Name', Exercise.Name AS 'Exercise Name', Exercise.Language
FROM Student JOIN Cohort ON Student.CohortId = Cohort.Id
JOIN StudentExercise ON StudentExercise.StudentId = Student.Id 
JOIN Exercise ON StudentExercise.ExerciseId = Exercise.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    // A list to hold the students we retrieve from the database.
                    List<Student> studentsExercises = new List<Student>();

                    while (reader.Read())
                    {
                        string studentFirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        if (studentsExercises.FirstOrDefault(student => student.FirstName == studentFirstName) == null)
                        {
                            Student currentStudent = new Student
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            };
                            Cohort currentCohort = new Cohort
                            {
                                Name = reader.GetString(reader.GetOrdinal("Cohort Name"))
                            };
                            Exercise exercise = new Exercise
                            {
                                Name = reader.GetString(reader.GetOrdinal("Exercise Name")),
                                Language = reader.GetString(reader.GetOrdinal("Language"))
                            };

                            currentStudent.cohort = currentCohort;
                            currentStudent.assignedExercises.Add(exercise);

                            // ...and add that student object to our list. C#

                            studentsExercises.Add(currentStudent);
                        }
                        else
                        {
                            Exercise exercise = new Exercise
                            {
                                Name = reader.GetString(reader.GetOrdinal("Exercise Name")),
                                Language = reader.GetString(reader.GetOrdinal("Language"))
                            };
                            Student studentToAssignTo = studentsExercises.FirstOrDefault(student => student.FirstName == studentFirstName);
                            studentToAssignTo.assignedExercises.Add(exercise);
                        }
                    }
                    reader.Close();

                    // Return the list of students with cohort and exercises 
                    return studentsExercises;
                }
            }
        }
        //Add a method that accepts a Cohort and an exercise and assigns an exercise to students in a cohort if they have not been assigned that exercise
        public void AddExerciseCohort(Cohort cohort, StudentExercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // More string interpolation
                    cmd.CommandText = $"INSERT INTO StudentExercise (StudentId, ExerciseId) Values ('{exercise.StudentId}', '{exercise.ExerciseId}')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

