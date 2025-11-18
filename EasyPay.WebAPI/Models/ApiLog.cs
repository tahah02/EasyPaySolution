using System;
using System.Collections.Generic;

namespace EasyPay.WebAPI.Models;

public partial class ApiLog
{
    public int Id { get; set; }

    public DateTime RequestTime { get; set; }

    public string? Method { get; set; }

    public string? Url { get; set; }

    public string? UserId { get; set; }

    public string? RequestBody { get; set; }

    public string? ResponseBody { get; set; }

    public int StatusCode { get; set; }
}
