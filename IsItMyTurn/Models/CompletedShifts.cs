using System;
using System.Collections.Generic;
using System.Text;

namespace IsItMyTurn.Models
{
    public class CompletedShifts
    {
        public int ShiftId { get; set; }
        public string ApartmentName { get; set; }
        public DateTime Date { get; set; }
    }
}
