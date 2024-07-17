using UnityEngine;

[CreateAssetMenu(menuName ="LifeJourney/Container")]
public class ContainerSO : ScriptableObject
{
    public string Name;
    public int MaxCapacity;
    public Transform prefab;
}
