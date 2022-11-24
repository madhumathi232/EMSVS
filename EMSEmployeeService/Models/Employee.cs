using System;
using System.Collections.Generic;

namespace EMSEmployeeService.Models;

public partial class Employee
{
    [System.ComponentModel.DataAnnotations.Key]
    public int EmpId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? Dob { get; set; }

    public string? ContactNo { get; set; }

    public string? DepartmentName { get; set; }

    public string? Designation { get; set; }

    public string? RoleName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateofJoining { get; set; }

    public string? Address { get; set; }

    public string? Salary { get; set; }

    public string? ReportingManager { get; set; }

    public string? LeaveBalance { get; set; }

    public string? MaxYearLeave { get; set; }

    public string? MaxMonthLeave { get; set; }

    public string? FileName { get; set; }

    public string? FileExtension { get; set; }

    public string? MimeType { get; set; }

    public string? FilePath { get; set; }

    public string? ProjectName { get; set; }

    public virtual Department? DepartmentNameNavigation { get; set; }

    public virtual TblDesignation? DesignationNavigation { get; set; }

    public virtual Project? ProjectNameNavigation { get; set; }

    public virtual ReportingManager? ReportingManagerNavigation { get; set; }

    public virtual RoleMaster? RoleNameNavigation { get; set; }
}
