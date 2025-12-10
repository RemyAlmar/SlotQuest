using System.Collections.Generic;
using System.Linq;

public class StatusHandler : IStatusHandler<FightTimeline>
{
    private readonly Dictionary<FightTimeline, List<IStatus>> statuses = new();
    public Dictionary<FightTimeline, List<IStatus>> Statuses { get => statuses; }

    public void AddStatus(FightTimeline _statusType, IStatus _status)
    {
        List<IStatus> statuses = GetOrAddStatusList(_statusType);
        IStatus _statusExisting = GetStatusFromType(_statusType, _status);
        if (_statusExisting == null)
        {
            statuses.Add(_status);
            _status.Add();
        }
        else
            _statusExisting.StackWith(_status);
    }
    private void AddStatusType(FightTimeline _statusType)
    {
        if (statuses.ContainsKey(_statusType)) return;
        statuses.Add(_statusType, new List<IStatus>());
    }
    private List<IStatus> GetOrAddStatusList(FightTimeline _statusType)
    {
        AddStatusType(_statusType);
        return statuses[_statusType];
    }
    public void ClearAllStatus()
    {
        foreach (FightTimeline _statusType in statuses.Keys)
        {
            ClearStatus(_statusType);
        }
    }
    public void ClearStatus(FightTimeline _statusType)
    {
        foreach (IStatus _status in statuses[_statusType])
        {
            _status.Remove();
        }
        statuses[_statusType].Clear();
    }

    public IStatus GetStatusFromType(FightTimeline _statusType, IStatus _status) => GetOrAddStatusList(_statusType).Find(s => s.Id == _status.Id);
    public List<IStatus> GetAllSameStatus(IStatus _status) => statuses.Keys.SelectMany(t => GetOrAddStatusList(t).Where(s => s.Id == _status.Id)).ToList();
    public bool ContainsStatus(FightTimeline _statusType, IStatus _status) => GetOrAddStatusList(_statusType).Contains(_status);
    public void ExecuteAllStatus(IStatus _status) => GetAllSameStatus(_status).ForEach(s => s.Execute());
    public void ExecuteStatus(FightTimeline _statusType, IStatus _status) => GetStatusFromType(_statusType, _status).Execute();
    public void RemoveStatus(FightTimeline _statusType, IStatus _status)
    {
        if (ContainsStatus(_statusType, _status))
        {
            GetOrAddStatusList(_statusType).Remove(_status);
            _status.Remove();
        }
    }
}
