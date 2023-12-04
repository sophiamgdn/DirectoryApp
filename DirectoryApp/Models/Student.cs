using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
        public string StudentGender { get; set; }
        public string StudentBirthday { get; set; }
        public string StudentMobile { get; set; }
        public string StudentCity { get; set; }
        public string SchoolName { get; set; }
        public string CourseName { get; set; }
        public string YearLevel { get; set; }

    }

    
}
