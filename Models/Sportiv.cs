using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dpcleague_2.Validation;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class Sportiv
    {
        [Key]
        public int SportivId { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Numele trebuie să aibă între 2 și 50 de caractere")]
        [RegularExpression(@"^[A-Za-zĂăÂâÎîȘșȚț\s-]+$", ErrorMessage = "Numele poate conține doar litere, spații și cratimă")]
        [Display(Name = "Nume")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prenumele trebuie să aibă între 2 și 50 de caractere")]
        [RegularExpression(@"^[A-Za-zĂăÂâÎîȘșȚț\s-]+$", ErrorMessage = "Prenumele poate conține doar litere, spații și cratimă")]
        [Display(Name = "Prenume")]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "Data nașterii este obligatorie")]
        [DataType(DataType.Date)]
        [Display(Name = "Data nașterii")]
        [AgeRange(MinAge = 16, MaxAge = 50, ErrorMessage = "Vârsta trebuie să fie între 16 și 50 de ani")]
        public DateTime DataNasterii { get; set; }

        [Required(ErrorMessage = "Organizația este obligatorie")]
        [Display(Name = "Organizație")]
        public int OrganizatieId { get; set; }

        [Required(ErrorMessage = "Porecla este obligatorie")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Porecla trebuie să aibă între 2 și 30 de caractere")]
        [Display(Name = "Poreclă")]
        [Unique(typeof(Sportiv), "Porecla", ErrorMessage = "Denumirea deja exista.")]
        public string Porecla { get; set; }

        [NotMapped]
        public int? RosterId { get; set; }

        // Navigation properties
        public Organizatie? Organizatie { get; set; }
        public ICollection<RosterSportiv>? RosterSportivi { get; set; }
    }

    // Custom validation attribute for age range
    public class AgeRangeAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                var age = DateTime.Today.Year - date.Year;
                if (date > DateTime.Today.AddYears(-age)) age--;
                return age >= MinAge && age <= MaxAge;
            }
            return false;
        }
    }
} 