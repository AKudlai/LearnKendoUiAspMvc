using System;

namespace LearnKendoUiAspMvc.Models.ViewModels
{
    public class QuarterToDateSalesViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Current { get; set; }
        public decimal Target { get; set; }
    }
}