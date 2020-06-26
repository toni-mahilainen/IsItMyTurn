using System;
using System.Collections.Generic;
using System.Text;

namespace IsItMyTurn.ViewModels
{
    public class SeekAndDestroyListViewModel
    {
        public List<CompletedShift> CompletedShiftList { get; set; }
    }

    public class CompletedShift
    {
        public int ShiftId { get; set; }
        public string ApartmentName { get; set; }
        public string Date { get; set; }
    }
}
