public class PlayerTasks
{
    public Tasks Task { get; private set; }
    public BaseInteractableObject TagretObject { get; private set; }
    public TaskStatus Status { get; private set; }

    public PlayerTasks(Tasks task, BaseInteractableObject gameObject)
    {
        Task = task;
        TagretObject = gameObject;
        Status = TaskStatus.None;
    }

    public void UpdateStatus(TaskStatus status)
    {
        Status = status;
    }
}