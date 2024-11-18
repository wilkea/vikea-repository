using System;
using System.Collections.Generic;
using dpcleague_2.Validation;

namespace dpcleague_2.Models
{
    public class RosterEveniment
    {
        public int RosterEvenimentId { get; set; }
        public int EvenimentId { get; set; }
        public int RosterId { get; set; }

        // Navigation properties
        public virtual Eveniment? Eveniment { get; set; }
        public virtual Roster? Roster { get; set; }
    }
} 