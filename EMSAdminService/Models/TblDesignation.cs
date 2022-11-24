using System;
using System.Collections.Generic;

namespace EMSAdminService.Models;

public partial class TblDesignation
{
    [System.ComponentModel.DataAnnotations.Key]
    public int DesignationId { get; set; }

    public string Designation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
