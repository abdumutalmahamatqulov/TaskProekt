﻿using TaskProekt.Entities;

namespace TaskProekt.ViewModels;

public class AuditLogViewModel
{
	public DateTime FromDate { get; set; }
	public DateTime ToDate { get; set; }
	public string? Name { get; set; }
	public List<AuditLog>? FilteredLogs { get; set; }
}
