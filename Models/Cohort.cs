﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercisesNet.Models
{
    public class Cohort
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student> Student {get; set; }
    }
}
