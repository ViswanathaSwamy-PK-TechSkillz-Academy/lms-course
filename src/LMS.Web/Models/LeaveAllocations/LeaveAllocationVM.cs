using LMS.Web.Models.LeaveTypes;
using LMS.Web.Models.Periods;
using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Models.LeaveAllocations;

public class LeaveAllocationVM
{
    public int Id { get; set; }


    [Display(Name = "Number Of Days")]
    public int Days { get; set; }


    [Display(Name = "Allocation Period")]
    public PeriodVM Period { get; set; } = new PeriodVM();

    public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM();
}