using Assets.Scripts.Interfaces;
using UnityEngine;

public class AreaName : MonoBehaviour
{
    [SerializeField]
    private string _areaName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<Player>() is Player)
        {
            (Player.Instance.Stats[StatsId.LocationName] as IStringStat).SetValue(_areaName);
        }
    }

    private void OnTriggerStay(Collider other)
    {
    }
}
