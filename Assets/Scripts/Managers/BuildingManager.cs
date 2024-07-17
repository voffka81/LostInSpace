using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public bool IsOpen { get; set; }
    public bool IsInside { get; set; }

    private string _address;
    public IndoorSO Indoor;

    public void BuildingInteract(IndoorSO indoor)
    {
        IsOpen = CheckIsOpen(indoor.OpenHoursFrom, indoor.OpenHoursTo);
        if (!IsInside)
        {
            EnterBuilding(indoor);
        }
        else
        {
            ExitBuilding();
        }
    }

    private void EnterBuilding(IndoorSO indoor)
    {
        if(IsOpen)
        {
            Indoor = indoor;
            IsInside = true;
            GameManager.Instance.Scene.Change("indoor");
        }
    }

    public void ExitBuilding()
    {
        GameManager.Instance.Scene.Change("city");
    }

    private bool CheckIsOpen(int from, int to)
    {
        return GameManager.Instance.Time.CurrentTime.Hours >= from 
            && GameManager.Instance.Time.CurrentTime.Hours <= to;
    }
}
