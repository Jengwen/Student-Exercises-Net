using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercisesNet.Data;
using StudentExercisesNet.Models;

namespace StudentExercisesNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            //See list of all Exercises

            //List<Exercise> exercises = repository.GetAllExercises();

            //PrintExerciseReport("All Exercises", exercises);

            //Pause();

            ////See list of Javascript Exercises
            //List<Exercise> javaExercises = repository.GetJavaExercises();

            //PrintJavaExerciseReport("Javascript Exercises", javaExercises);

            //Pause();
            ////Add Exercise to the databse
            //Exercise library = new Exercise
            //{
            //    Name = "Library",
            //    Language = "C#"
            //};
            //repository.AddExercise(library);

            //Pause();

            //Re print reports with new Exercise added
            //List<Exercise> Exercises = repository.GetAllExercises();

            //PrintExerciseReport("All Exercises with new", exercises);

            //List instructors by Cohort
            //List<Instructor> instructors = repository.GetInstructorsByCohort();

            //PrintInstructorReport("Instructor By Cohort", instructors);

            //Pause();
            //Add an instructor
            //Assign instructor to a cohort
            //Instructor jen = new Instructor
            //{
            //    FirstName = "Jennifer",
            //    LastName = "Johnson",
            //    SlackHandle = "jen@slack",
            //    CohortId = 2
            //};
            //repository.AddInstructor(jen);

            //Pause();

            ////Add existing exercise to existing student
            //StudentExercise bobby = new StudentExercise
            //{
            //    StudentId = 2,
            //    ExerciseId = 2
            //};
            //repository.AddStudentExercise(bobby);

            //List of STudents with Cohort and Assigned exercises
            List<Student> students = repository.GetStudentsWithExercises();

        }


        //create report to see all Exercises
        public static void PrintExerciseReport(string title, List<Exercise> exercises)
        {
            Console.WriteLine(title);

            foreach (Exercise e in exercises)
            {
                Console.WriteLine(e.Name);
            }
        }
        //create report to see Javascript Exercises
        public static void PrintJavaExerciseReport(string title, List<Exercise> exercises)
        {
            Console.WriteLine(title);

            foreach (Exercise e in exercises)
            {
                Console.WriteLine(e.Name);
            }
        }
        //create report to see Instructors by Cohort
        public static void PrintInstructorReport(string title, List<Instructor> instructors)
        {
            Console.WriteLine(title);
            for (int i = 0; i < instructors.Count; i++)
                if (instructors[i].Cohort == null)
                {
                    Console.WriteLine($"{instructors[i].Id}. {instructors[i].FirstName}");
                }
                else
                {
                    Console.WriteLine($"{instructors[i].Id}. {instructors[i].FirstName} Cohort: {instructors[i].Cohort.Name}");
                };
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
