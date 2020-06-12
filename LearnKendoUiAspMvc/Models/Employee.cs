namespace LearnKendoUiAspMvc.Models
{

    public class Employee
    {

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string HomePhone { get; set; }
        public string Notes { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}