using TaskProekt.Entities;
using TaskProekt.ViewModels;

namespace TaskProekt.Services.Interfaces
{
	public interface IAuditRepository
	{
		Task<AuditLogViewModel> Index(DateTime? fromDate, DateTime? toDate, string name);
		Task<List<AuditLog>> SortByUserName(string name);
		Task<List<AuditLog>> GetFiltered(string fromDate, string toDate);
		Task<List<AuditLog>> GetAllAudit();
	}
}
