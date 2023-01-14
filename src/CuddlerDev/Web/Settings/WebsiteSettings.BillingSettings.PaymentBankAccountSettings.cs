using System.ComponentModel.DataAnnotations;

namespace CuddlerDev.Web.Settings;

public class PaymentBankAccountSettings
{
    [Display(Name = "Bank Account Number", Order = 1)]
    public string AccountNo { get; set; } = "19200000174";

    [Display(Name = "Bank Address", Order = 2)]
    public string BankAddress { get; set; } = "Suite 1850, 801 - 6th Avenue SW, Calgary, AB T2P 3W2";

    [Display(Name = "Bank Name", Order = 0)]
    public string BankingInfo { get; set; } = "Industrial and Commercial Bank of China (Canada) Calgary Branch";

    [Display(Name = "Branch Transit No", Order = 2)]
    public string BranchTransitNo { get; set; } = "00049";

    [Display(Name = "GST Acc. Number", Order = 2)]
    public string GstAccountNo { get; set; } = "801831785 RT001";

    [Display(Name = "Institution Number", Order = 2)]
    public string InstitutionNo { get; set; } = "";

    public string? InvoicePrefix { get; set; }

    [Display(Name = "Swift Code", Order = 2)]
    public string SwiftCode { get; set; } = "ICBKCAT2";
}