namespace LearnKendoUiAspMvc.Models.ViewModels
{
    public class OrderViewModel
    {
        // The example will use this as a unique model Id.
        public int OrderID { get; set; }

        public string ShipCountry { get; set; }

        public int Freight { get; set; }
    }
}