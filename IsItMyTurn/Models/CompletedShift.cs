using System;
using System.Collections.Generic;
using System.Text;

namespace IsItMyTurn.Models
{
    public class CompletedShift
    {
        public int ShiftId { get; set; }
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public DateTime Date { get; set; }
        public string DateStr { get; set; }
    }
}
