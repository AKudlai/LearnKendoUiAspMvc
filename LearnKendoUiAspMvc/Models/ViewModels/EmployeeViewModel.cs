namespace LearnKendoUiAspMvc.Models.ViewModels
{
    public class EmployeeViewModel
    {
        // The Id.
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // A nullable ParentId.
        public int? ReportsTo { get; set; }

        public string Address { get; set; }

        // This is a case-sensitive property. Define it only if you want to use lazy-loading.
        // If it is not defined, the TreeList will calculate and assign its value on the client.
        public bool HasChildren { get; set; }
    }
}