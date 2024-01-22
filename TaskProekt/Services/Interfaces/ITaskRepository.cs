namespace TaskProekt.Services.Interfaces;

using TaskProekt.Entities;
using Task = TaskProekt.Entities.Task;
public interface ITaskRepository
{
	Task<Task> GetTaskByIdAsync(int taskId);
	Task<List<Task>> GetAllTasksAsync();
	Task<Task> CreateTaskAsync(Task task, string email);
	Task<Task> UpdateTaskAsync(Task task);
	Task<Task> DeleteTaskAsync(int taskId);
	public Task<Task> GetOldValueAsync(int id);

	public Task<Task> CreateAudit(Task newValue, Task oldValue, string actionType, User user);

	Task<Task> CheckTaskName(Task task);
}
