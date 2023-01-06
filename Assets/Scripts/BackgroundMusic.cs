using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        var instanceCount = FindObjectsOfType<BackgroundMusic>().Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
