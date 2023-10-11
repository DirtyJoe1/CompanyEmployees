using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class StudentForManipulation
    {
        [Required(ErrorMessage = "Student name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Range(15, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 15")]
        public int Age { get; set; }
    }
}
