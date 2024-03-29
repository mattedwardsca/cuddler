﻿using System.ComponentModel.DataAnnotations;
using CuddlerDev.Forms;
using CuddlerDev.Forms.Attributes;

namespace CuddlerDev.Web.Settings;

public class EmailSettings
{
    [Row(1, 0)]
    public CommonEmailSettings CommonSettings { get; set; } = new();

    [Row(2, 0)]
    [Required]
    [DropdownOption(nameof(SendGridSettings), nameof(SendGridSettings))]
    [DropdownOption(nameof(SmtpSettings), nameof(SmtpSettings))]
    [DropdownOption(nameof(Office365Settings), nameof(Office365Settings))]
    [FormField(EFormField.DropdownOptions)]
    public string EmailProvider { get; set; } = null!;

    [Row(5, 0)]
    //[FormField(EFormField.DropdownContentOption)]
    public Office365ServiceProvider Office365Settings { get; set; } = new();

    [Row(3, 0)]
    // [FormField(EFormField.DropdownContentOption)]
    public SendGridServiceProvider SendGridSettings { get; set; } = new();

    [Row(4, 0)]
    // [FormField(EFormField.DropdownContentOption)]
    public SmtpServiceProvider SmtpSettings { get; set; } = new();
}