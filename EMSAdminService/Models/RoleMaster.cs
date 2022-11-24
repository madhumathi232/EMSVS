using System;
using System.Collections.Generic;

namespace EMSAdminService.Models;

public partial class RoleMaster
{
    [System.ComponentModel.DataAnnotations.Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
