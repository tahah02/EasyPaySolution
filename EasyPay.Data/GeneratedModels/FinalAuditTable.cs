using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

public partial class FinalAuditTable
{
    public int PaymentId { get; set; }

    public string LogId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ApiRequest { get; set; }

    public string? JsonHeader { get; set; }

    public string? UserName { get; set; }

    public decimal TransferredAmount { get; set; }

    public decimal ClosingBalance { get; set; }

    public bool IsSuccess { get; set; }

    public string? ApiResponse { get; set; }
}
