using Cuddler.Forms;

namespace Cuddler.Ui;

public class CuddlerFormModel
{
    public CuddlerUri FormAction { get; set; } = null!;

    public List<FormField> FormFields { get; set; } = null!;
}