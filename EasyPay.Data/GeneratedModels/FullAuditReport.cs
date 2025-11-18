using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

public partial class FullAuditReport
{
    public int PaymentId { get; set; }

    public string LogId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public decimal TransferredAmount { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal ClosingBalance { get; set; }

    public bool PaymentSuccess { get; set; }

    public DateTime? RequestTime { get; set; }

    public string? Method { get; set; }

    public string? Url { get; set; }

    public string? RequestBody { get; set; }

    public string? ResponseBody { get; set; }

    public int? StatusCode { get; set; }
}
