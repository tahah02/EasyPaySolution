using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

public partial class PaymentRecord
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public decimal TransferredAmount { get; set; }

    public string SecretPin { get; set; } = null!;

    public bool IsSuccess { get; set; }

    public decimal ClosingBalance { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal OpeningBalance { get; set; }

    public string LogId { get; set; } = null!;
}
