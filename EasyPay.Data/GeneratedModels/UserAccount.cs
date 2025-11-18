using System;
using System.Collections.Generic;

namespace EasyPay.Data.GeneratedModels;

public partial class UserAccount
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string AccountTitle { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public string? PasswordHash { get; set; }
}
