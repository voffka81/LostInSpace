using UnityEngine;

[CreateAssetMenu(menuName = "LifeJourney/Indor object")]
public class IndoorSO : ScriptableObject
{
    public string BuidingName;
    public string Address;
    public string SpawnPointInSceneName;
    public GameObject Prefab;
    public int OpenHoursFrom;
    public int OpenHoursTo;
}
