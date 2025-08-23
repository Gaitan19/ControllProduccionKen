using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public DateTime TimeUtc { get; set; }

    public string? Level { get; set; }

    public string? Message { get; set; }

    public string? Exception { get; set; }

    public string? StackTrace { get; set; }

    public string? RequestPath { get; set; }

    public string? RequestMethod { get; set; }

    public string? QueryString { get; set; }

    public string? RequestBody { get; set; }

    public string? UserId { get; set; }

    public string? Ipaddress { get; set; }

    public string? UserAgent { get; set; }

    public string? CorrelationId { get; set; }
}
