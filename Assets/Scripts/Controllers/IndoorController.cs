using Unity.AI.Navigation;
using UnityEngine;

public class IndoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var indoor = GameManager.Instance.BuildingSystem.Indoor;
        var transform =Instantiate(indoor.Prefab, Vector3.zero, Quaternion.identity);

        
        // If the NavMeshSurface is not assigned in the Inspector, try to find it
        var navMeshSurface = transform.GetComponentInChildren<NavMeshSurface>();
        
        // Build the NavMesh
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface is not assigned and not found on the GameObject.");
        }

        SpawnPlayer(indoor);
    }

    private Vector3 SpawnPlayer(IndoorSO indoor)
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        BaseInteractableObject interactable = null;
        if (spawnPoints != null)
        {
            foreach (var spawn in spawnPoints)
            {
                if (spawn.name.ToLower() == indoor.SpawnPointInSceneName.ToLower())
                {
                    interactable = spawn.GetComponent<BaseInteractableObject>();
                    Player.Instance.SetPosition(interactable._interactionPoint);
                    break;
                }
            }
        }
        GameManager.Instance.Camera.ResetToPlayerPosition();
        return (interactable == null) ? Vector3.zero : interactable._interactionPoint.position;
    }
}
