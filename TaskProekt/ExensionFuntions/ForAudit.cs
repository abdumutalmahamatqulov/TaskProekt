using TaskProekt.Entities;

namespace TaskProekt.ExensionFuntions;

public class ForAudit
{
	public static List<AuditLog> FilterAuditLogsByDate(List<AuditLog> Logs, DateTime? fromDate, DateTime? toDate, string Name)
	{
		var filterLogs = Logs
			.Where(log =>
			(!fromDate.HasValue || log.DateTime >= fromDate) &&
			(!toDate.HasValue || log.DateTime <= toDate?.AddDays(1)) &&
			(Name == null || log.UserName.IndexOf(Name, StringComparison.OrdinalIgnoreCase) >= 0)
			)
			.ToList();
		return filterLogs;
	}
}
