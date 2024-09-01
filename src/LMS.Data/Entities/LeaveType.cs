﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities;

public class LeaveType : BaseEntity
{
    [MaxLength(150)]
    public string Name { get; set; } = default!;

    public int NumberOfDays { get; set; }

    //public List<LeaveAllocation>? LeaveAllocations { get; set; }
}
