using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

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

    public string? Headers { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? Path { get; set; }
}
