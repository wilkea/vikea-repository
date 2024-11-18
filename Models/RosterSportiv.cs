using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class RosterSportiv

    {
        public int RosterSportivId { get; set; }
        public int RosterId { get; set; }
        public int SportivId { get; set; }

        // Navigation properties
        public  Roster? Roster { get; set; }
        public Sportiv? Sportiv { get; set; }
    }
}