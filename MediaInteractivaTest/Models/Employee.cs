using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaInteractivaTest.Models
{
    public class Employee
    {
        public Employee()
        {
            Pets = new List<Pet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Lastname { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool IsEmployee { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
