using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class Organizatie
    {
        [Key]
        public int OrganizatieId { get; set; }

        [Required(ErrorMessage = "Denumirea este obligatorie")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Denumirea trebuie să aibă între 2 și 100 de caractere")]
        [Display(Name = "Denumire")]
        [Unique(typeof(Organizatie), "Denumire", ErrorMessage = "Denumirea deja exista.")]
        public string Denumire { get; set; }

        [Required(ErrorMessage = "Data creării este obligatorie")]
        [DataType(DataType.Date)]
        [Display(Name = "Data creării")]
        public DateTime DataCrearii { get; set; }

        [Required(ErrorMessage = "Originea este obligatorie")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Originea trebuie să aibă între 2 și 100 de caractere")]
        [Display(Name = "Origine")]
        public string Originea { get; set; }

        // Navigation properties
        public ICollection<Sportiv>? Sportivi { get; set; }
        public ICollection<Roster>? Rostere { get; set; }
    }

} 