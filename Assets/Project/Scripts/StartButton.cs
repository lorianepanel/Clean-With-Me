using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("coucou");
        SceneManager.LoadScene("Mainscene");
    }
}
