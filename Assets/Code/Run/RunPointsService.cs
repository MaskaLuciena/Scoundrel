using System;
using UnityEngine;

public class RunPointsService
{
    private readonly RunSessionModel _session;

    public event Action<int> OnRunPointsChanged;

    public RunPointsService(RunSessionModel session)
    {
        _session = session;
    }
    
    //Начислить очки за прохождение комнаты.
    //Побег (escape) не даёт очков.
    public void AddPointForRoom(bool escaped)
    {
        if (!escaped)
        {
            _session.AddRunPoint();     //обновлено
            _session.AddRoomCleared();  //считаем для XP

            Debug.Log($"[Run] +1 RunPoint (total: {_session.RunPoints}), RoomsCleared: {_session.RoomsCleared}");
            OnRunPointsChanged?.Invoke(_session.RunPoints);
        }
    }
    
    public bool TrySpendPoints(int cost)
    {
        if (_session.RunPoints >= cost)
        {
            _session.SpendPoints(cost);
            Debug.Log($"[Run] Spent {cost} RunPoints (left: {_session.RunPoints})");
            OnRunPointsChanged?.Invoke(_session.RunPoints);
            return true;
        }

        Debug.Log("[Run] Not enough points!");
        return false;
    }

    public int CurrentPoints => _session.RunPoints;
}