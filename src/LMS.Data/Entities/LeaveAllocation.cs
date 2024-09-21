namespace LMS.Data.Entities;

public class LeaveAllocation : BaseEntity
{
    public LeaveType? LeaveType { get; set; }

    public int LeaveTypeId { get; set; }

    public ApplicationUser? Employee { get; set; }

    public string EmployeeId { get; set; } = string.Empty;

    public int PeriodId { get; set; }

    public int Days { get; set; }
}