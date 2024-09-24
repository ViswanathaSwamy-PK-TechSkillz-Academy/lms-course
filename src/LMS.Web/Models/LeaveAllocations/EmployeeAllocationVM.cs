using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Models.LeaveAllocations;

public class EmployeeAllocationVM
{
    public string Id { get; set; } = string.Empty;


    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;


    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;


    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    //public bool IsCompletedAllocation { get; set; }

    public List<LeaveAllocationVM>? LeaveAllocations { get; set; }
}
