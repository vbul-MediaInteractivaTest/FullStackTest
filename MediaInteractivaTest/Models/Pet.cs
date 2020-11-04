using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaInteractivaTest.Models
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public PetType Type { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public long OwnerId { get; set; }

        public Employee Owner { get; set; }
    }

    public enum PetType
    {
        Cat,
        Dog,
        Fish,
        Spider,
        Hamster
    }
}
