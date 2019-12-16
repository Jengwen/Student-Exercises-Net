using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercisesNet.Models
{
    public class Instructor
    {
        public int Id { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string SlackHandle { get; set;}
        public int CohortId { get; set;}
        // This property is for storing the C# object representing the department
        public Cohort Cohort { get; set; }


    }
}
