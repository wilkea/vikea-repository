using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class Roster
    {
        [Key]
        public int RosterId { get; set; }

        [Required]
        public int OrganizatieId { get; set; }

        [Required(ErrorMessage = "Disciplina este obligatorie")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "Data formării este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataFormare { get; set; }

        // Navigation properties
        public Organizatie? Organizatie { get; set; }
        public ICollection<RosterSportiv>? RosterSportivi { get; set; }
        public ICollection<RosterEveniment>? RosterEvenimente { get; set; }
    }
} 