using MessagePack;
using System;
using System.Collections.Generic;


namespace EMSEmployeeService.Models;

public partial class Document
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public int? EmpId { get; set; }

    public string? FileName { get; set; }

    public string? FileExtension { get; set; }

    public string? MimeType { get; set; }

    public string? FilePath { get; set; }
}
