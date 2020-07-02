using System;
using System.Collections.Generic;
using System.Text;

namespace IsItMyTurn.Models
{
    public class NewShift
    {
        public int ApartmentId { get; set; }
        public DateTime Date { get; set; }

        public string FCMToken { get; set; }
    }
}
