using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

public partial class EasyPayLog
{
    public int PaymentId { get; set; }

    public string TransactionId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public decimal TransferredAmount { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal ClosingBalance { get; set; }

    public bool IsSuccess { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? ApiUrl { get; set; }

    public string? Method { get; set; }

    public int? StatusCode { get; set; }

    public DateTime? RequestTime { get; set; }

    public string? RequestJson { get; set; }

    public string? ResponseJson { get; set; }

    public string? ClientDetails { get; set; }
}
