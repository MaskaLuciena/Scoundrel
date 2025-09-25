using UnityEngine;

public class RunSessionModel
{
    public int RunPoints { get; private set; }
    public int RoomsCleared { get; private set; } //новый счётчик комнат
    public bool RunCompleted { get; private set; }

    public RunSessionModel()
    {
        Reset();
    }

    public void AddRunPoint()
    {
        RunPoints++;
    }

    public void SpendPoints(int amount)
    {
        RunPoints = Mathf.Max(0, RunPoints - amount);
    }

    public void AddRoomCleared()
    {
        RoomsCleared++;
    }

    //сброс при старте новой игры
    public void Reset()
    {
        RunPoints = 0;
        RoomsCleared = 0;
        RunCompleted = false;
    }
}