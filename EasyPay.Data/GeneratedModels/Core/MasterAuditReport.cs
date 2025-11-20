using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels.Core;

public partial class MasterAuditReport
{
    public int AuditLogId { get; set; }

    public DateTime RequestTime { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public int StatusCode { get; set; }

    public string? ChannelId { get; set; }

    public string? DeviceId { get; set; }

    public string? ClientIp { get; set; }

    public string? UserEmail { get; set; }

    public string? StatusMessage { get; set; }

    public string? RequestBody { get; set; }

    public string? ResponseBody { get; set; }

    public string? RawHeadersJson { get; set; }
}
