using System.Collections.Generic;

public interface IStatusHandler<T>
{
    public Dictionary<T, List<IStatus>> Statuses { get; }
    public void AddStatus(T _statusType, IStatus _status);
    public void RemoveStatus(T _statusType, IStatus _status);
    public IStatus GetStatusFromType(T _statusType, IStatus _status);
    public List<IStatus> GetAllSameStatus(IStatus _status);
    public void ClearAllStatus();
    public void ClearStatus(T _statusType);
    public bool ContainsStatus(T _statusType, IStatus _status);
    public void ExecuteAllStatus(IStatus _status);
    public void ExecuteStatus(T _statusType, IStatus _status);
}
