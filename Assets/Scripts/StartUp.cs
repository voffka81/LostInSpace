using UnityEngine;

public class StartUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Scene.Change("Game");
    }
}
