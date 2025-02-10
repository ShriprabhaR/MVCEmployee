using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class AddEmployeeModel
    {
        [Required(ErrorMessage = "Manadatory field")]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "First letter should be in UpperCase other letters should LowerCase.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers.")]

        public int Age { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers.")]

        public int Salary { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [EmailAddress]

        public string Email { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numbers allowed in the sequence.")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numbers allowed in the sequence.")]

        public string Department { get; set; }
        [Required(ErrorMessage = "Manadatory field")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numbers allowed in the sequence.")]
        public string City { get; set; }
    }
}
