using System;
using System.Collections.Generic;

namespace EMSAdminService.Models;

public partial class ReportingManager
{
    [System.ComponentModel.DataAnnotations.Key]
    public int ManagerId { get; set; }

    public string ManagerName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
