using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskProekt.Data;
using TaskProekt.Entities;
using TaskProekt.Services.Interfaces;
using Task = TaskProekt.Entities.Task;
namespace TaskProekt.Services.Repositories;

public class TaskRepository : ITaskRepository
{
	readonly AppDbContext _context;

	public TaskRepository(AppDbContext context) 
		=> _context = context;

	public async Task<Task> CheckTaskName(Task task)
	{
		var checkbase = await _context.Tasks.FindAsync(task);
		return checkbase ?? new Task();
	}

	public async  Task<Task> CreateAudit(Task newValue, Task oldValue, string actionType, User user)
	{
		var auditRecord = new AuditLog
		{
			UserName = user.UserName,
			Action = actionType,
			ControllerName = "Task",
			DateTime = DateTime.UtcNow,
			OldValue = JsonConvert.SerializeObject(oldValue, Formatting.Indented),
			NewValue = JsonConvert.SerializeObject(newValue,Formatting.Indented)
		};
		_context.AuditLog.Add(auditRecord);
		try
		{
			await _context.SaveChangesAsync();
			return newValue;
		}
		catch (Exception ex)
		{
			throw new Exception("Error saving audit log.", ex);
		}
	}

	public async Task<Task> CreateTaskAsync(Task task, string email)
	{
		_context.Tasks.Add(task);
		await _context.SaveChangesAsync();
		return task;
	}

	public async  Task<Task> DeleteTaskAsync(int taskId)
	{
		var tasks = await _context.Tasks.FirstOrDefaultAsync(z => z.Id == taskId);
		_context.Tasks.Remove(tasks);
		await _context.SaveChangesAsync();
		return tasks;
	}

	public async Task<List<Task>> GetAllTasksAsync()
		=> await _context.Tasks.ToListAsync();

	public async Task<Task> GetOldValueAsync(int id) 
		=> await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

	public async Task<Task> GetTaskByIdAsync(int taskId) 
		=> await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

	public async Task<Task> UpdateTaskAsync(Task task)
	{
		var taskUpdate  = await _context.Tasks.FirstOrDefaultAsync(x =>x.Id==task.Id);

		if(taskUpdate != null)
		{
			taskUpdate.Title = task.Title;
			taskUpdate.Status = task.Status;
			taskUpdate.DueDate = task.DueDate;
			taskUpdate.Description = task.Description;

			await _context.SaveChangesAsync();
		}
		return taskUpdate;

	}
}
