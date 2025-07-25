using System.Collections.ObjectModel;
using BuggyTasks.Models;

namespace BuggyTasks.Services;

public static class TaskService
{
    public static ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();
}