using System;
using System.Collections.Generic;

namespace EMSEmployeeService.Models;

public partial class Department
{
    [System.ComponentModel.DataAnnotations.Key]
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
