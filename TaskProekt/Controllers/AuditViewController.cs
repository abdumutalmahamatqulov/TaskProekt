using Microsoft.AspNetCore.Mvc;
using TaskProekt.Services.Interfaces;

namespace TaskProekt.Controllers;

public class AuditViewController : Controller
{
    readonly IAuditRepository _context;
    public AuditViewController(IAuditRepository context) => _context = context;
    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string Name)
        => View(await _context.Index(fromDate, toDate, Name));
}
