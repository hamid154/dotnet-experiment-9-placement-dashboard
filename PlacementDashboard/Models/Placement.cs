namespace PlacementDashboard.Models;

// Placement - one record per placed student.
// The dashboard reads these records through the data controls:
// a sortable, pageable table, DropDownList filters and a Details view.
public class Placement
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Company { get; set; } = "";

    // Package in LPA (Lakhs Per Annum), e.g. 7.5
    public double PackageLPA { get; set; }

    public string Branch { get; set; } = "";

    public int Year { get; set; }
}
