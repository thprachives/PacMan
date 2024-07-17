using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange1 : MonoBehaviour
{
  public string nextSceneName = "Scene2"; // Set the name of the scene you want to switch to

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}
