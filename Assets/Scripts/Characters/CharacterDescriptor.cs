using UnityEngine;
using UnityEngine.AI;

public class CharacterDescriptor : MonoBehaviour
{
    [SerializeField]
    public CharacterSex Sex;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnAnimatorMove()
    {
        if (_navMeshAgent != null)
        {
            transform.parent.position = _navMeshAgent.nextPosition;
        }
    }
}
