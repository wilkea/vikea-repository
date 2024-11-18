using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dpcleague_2;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class Eveniment
    {
        [Key]
        public int EvenimentId { get; set; }

        [Required(ErrorMessage = "Denumirea este obligatorie")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Denumirea trebuie să aibă între 3 și 100 de caractere")]
        [Display(Name = "Denumire")]
        [Unique(typeof(Eveniment), "Denumire", ErrorMessage = "Denumirea deja exista.")]
        public string Denumire { get; set; }

        [Required(ErrorMessage = "Disciplina este obligatorie")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Disciplina trebuie să aibă între 2 și 50 de caractere")]
        [Display(Name = "Disciplină")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "Data începerii este obligatorie")]
        [DataType(DataType.Date)]
        [Display(Name = "Data începerii")]
        public DateTime DataInceput { get; set; }

        [Required(ErrorMessage = "Locația este obligatorie")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Locația trebuie să aibă între 3 și 200 de caractere")]
        [Display(Name = "Locație")]
        public string Locatia { get; set; }

        // Navigation properties
        public ICollection<RosterEveniment>? RosterEvenimente { get; set; }
        public ICollection<Bilet>? Bilets { get; set; }
    }

} 