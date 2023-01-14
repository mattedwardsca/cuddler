namespace CuddlerDev.Pages.Shared.Cuddler.Wizard;

public class WizardStep
{
    public string Id { get; set; } = null!;

    public string StepTitle { get; set; } = null!;

    public string StepUrl { get; set; } = null!;

    public bool ShowTitle { get; set; }

    public bool IsDone { get; set; }
}