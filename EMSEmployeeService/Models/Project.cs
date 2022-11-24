using System;
using System.Collections.Generic;

namespace EMSEmployeeService.Models;

public partial class Project
{
    [System.ComponentModel.DataAnnotations.Key]
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
