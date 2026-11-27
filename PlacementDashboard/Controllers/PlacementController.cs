using Microsoft.AspNetCore.Mvc;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers;

// PlacementController - the Placement Dashboard.
// Data controls used: sortable + pageable data table (GridView equivalent),
// DropDownList filters (Company, Year), search box and a Details view.
public class PlacementController : Controller
{
    // In-memory data source (lab purpose - no database needed).
    // 12 placed students across 5 companies, packages 3.5 - 12 LPA.
    private static readonly List<Placement> _placements = new()
    {
        new Placement { Id = 1,  Name = "Aarav Patel",     Company = "TCS",       PackageLPA = 7.0,  Branch = "Computer Engineering", Year = 2026 },
        new Placement { Id = 2,  Name = "Diya Shah",       Company = "Infosys",   PackageLPA = 6.5,  Branch = "Computer Engineering", Year = 2026 },
        new Placement { Id = 3,  Name = "Rohan Mehta",     Company = "Wipro",     PackageLPA = 5.0,  Branch = "Information Technology", Year = 2026 },
        new Placement { Id = 4,  Name = "Sneha Joshi",     Company = "Accenture", PackageLPA = 8.5,  Branch = "Computer Engineering", Year = 2026 },
        new Placement { Id = 5,  Name = "Karan Singh",     Company = "Capgemini", PackageLPA = 7.5,  Branch = "Electronics & Communication", Year = 2026 },
        new Placement { Id = 6,  Name = "Ananya Rao",      Company = "TCS",       PackageLPA = 9.0,  Branch = "Computer Engineering", Year = 2025 },
        new Placement { Id = 7,  Name = "Vikram Nair",     Company = "Infosys",   PackageLPA = 4.5,  Branch = "Information Technology", Year = 2025 },
        new Placement { Id = 8,  Name = "Pooja Desai",     Company = "Wipro",     PackageLPA = 3.5,  Branch = "Computer Engineering", Year = 2025 },
        new Placement { Id = 9,  Name = "Arjun Kumar",     Company = "Accenture", PackageLPA = 12.0, Branch = "Computer Engineering", Year = 2026 },
        new Placement { Id = 10, Name = "Meera Iyer",      Company = "Capgemini", PackageLPA = 6.0,  Branch = "Information Technology", Year = 2025 },
        new Placement { Id = 11, Name = "Nikhil Gupta",    Company = "TCS",       PackageLPA = 5.5,  Branch = "Electronics & Communication", Year = 2026 },
        new Placement { Id = 12, Name = "Kavya Menon",     Company = "Infosys",   PackageLPA = 10.5, Branch = "Computer Engineering", Year = 2026 },
    };

    private const int PageSize = 5;

    // GET: /Placement
    // sort: name, name_desc, company, company_desc, package, package_desc
    // search: matches student name or company (case-insensitive)
    // company / year: DropDownList filters
    // page: 1-based page number (5 rows per page)
    public IActionResult Index(string? sort, string? search, string? company,
                               int? year, int page = 1)
    {
        // --- summary stat cards (computed from the full data) ---
        ViewBag.TotalPlaced = _placements.Count;
        ViewBag.AveragePackage = Math.Round(_placements.Average(p => p.PackageLPA), 2);
        ViewBag.HighestPackage = _placements.Max(p => p.PackageLPA);
        ViewBag.CompaniesCount = _placements.Select(p => p.Company).Distinct().Count();

        // --- filter drop-down data ---
        ViewBag.Companies = _placements.Select(p => p.Company)
                                        .Distinct().OrderBy(c => c).ToList();
        ViewBag.Years = _placements.Select(p => p.Year)
                                    .Distinct().OrderByDescending(y => y).ToList();

        // --- filtering ---
        IEnumerable<Placement> query = _placements;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Company.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(company))
        {
            query = query.Where(p => p.Company == company);
        }
        if (year.HasValue)
        {
            query = query.Where(p => p.Year == year.Value);
        }

        // --- sorting (clicking a column header toggles the direction) ---
        query = sort switch
        {
            "name"          => query.OrderBy(p => p.Name),
            "name_desc"     => query.OrderByDescending(p => p.Name),
            "company"       => query.OrderBy(p => p.Company),
            "company_desc"  => query.OrderByDescending(p => p.Company),
            "package"       => query.OrderBy(p => p.PackageLPA),
            "package_desc"  => query.OrderByDescending(p => p.PackageLPA),
            _               => query.OrderBy(p => p.Id),
        };

        // --- paging ---
        int totalRows = query.Count();
        int totalPages = (int)Math.Ceiling(totalRows / (double)PageSize);
        if (totalPages == 0) totalPages = 1;
        page = Math.Clamp(page, 1, totalPages);

        var rows = query.Skip((page - 1) * PageSize).Take(PageSize).ToList();

        ViewBag.Sort = sort ?? "";
        ViewBag.Search = search ?? "";
        ViewBag.Company = company ?? "";
        ViewBag.Year = year;
        ViewBag.Page = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalRows = totalRows;

        return View(rows);
    }

    // GET: /Placement/Details/5 - DetailsView-style single record page
    public IActionResult Details(int id)
    {
        var student = _placements.FirstOrDefault(p => p.Id == id);
        if (student == null) return NotFound();
        return View(student);
    }
}
