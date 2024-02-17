namespace Location.API.Common;

public class OperationResult<T> where T : class
{
    public bool IsError { get; set; }
    public T Payload { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public void AddError(string message)
    {
        this.Errors.Add(message);
        this.IsError = true;
    }
}
