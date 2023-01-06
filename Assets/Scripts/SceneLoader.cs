using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame(int speed)
    {
        var gm = FindObjectOfType<GameManager>();
        gm.CurrSpeed = (Speed)speed;
        gm.SetTimer(true);
        SceneManager.LoadScene("Level0");
    }

    public void ResetGame()
    {
        Cursor.visible = true;
        Destroy(FindObjectOfType<GameManager>().gameObject);
        SceneManager.LoadScene(0);
    }
}
