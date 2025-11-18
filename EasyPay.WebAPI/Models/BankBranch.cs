using System;
using System.Collections.Generic;

namespace EasyPay.WebAPI.Models;

public partial class BankBranch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? City { get; set; }
}
