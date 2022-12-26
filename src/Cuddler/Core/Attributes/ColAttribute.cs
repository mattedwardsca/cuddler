using System.Text;

namespace Cuddler.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColAttribute : Attribute
{
    public ColAttribute()
    {
    }

    public ColAttribute(int columnStart, int columnSpan = 1)
    {
        ColumnStart = columnStart;
        ColumnSpan = columnSpan;
    }

    public int ColumnSpan { get; set; } = 1;

    public int ColumnStart { get; set; } = 1;

    public string ToLayoutStyles()
    {
        var sb = new StringBuilder();

        // grid-column: <start-line> / <end-line> | <start-line> / span <value>;
        sb.Append("grid-column:" + ColumnStart + " / span " + ColumnSpan + ";");

        return sb.ToString();
    }
}