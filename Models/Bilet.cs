using System;
using System.ComponentModel.DataAnnotations;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class Bilet
    {
        [Key]
        public int BiletId { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "Data procurării este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataProcurarii { get; set; }

        [Required]
        public int EvenimentId { get; set; }

        // Navigation property
        public Eveniment? Eveniment { get; set; }
    }
} 