using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Models.LeaveTypes;

public class IndexVM
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Display(Name = "Maximum Allocation of Days")]
    public int NumberOfDays { get; set; }
}
