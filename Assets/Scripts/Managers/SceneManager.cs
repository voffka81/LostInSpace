using UnityEngine;

public class SceneManager 
{
    private string _spawnLocationName= "DefaultStartPoint";
    
    public SceneManager()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    public void Change(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        if (spawnPoints != null)
        {
            foreach (var spawn in spawnPoints)
            {
                if (spawn.name.ToLower() == _spawnLocationName.ToLower())
                {
                    var interactable = spawn.GetComponent<BaseInteractableObject>();
                    Player.Instance.SetPosition(interactable._interactionPoint);
                    
                }
            }
        }
        GameManager.Instance.Camera.ResetToPlayerPosition();
    }
}
