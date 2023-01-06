using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]private int breakableBlocks;
    
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBreakableBlock()
    {
        breakableBlocks++;
    }

    public void RemoveBreakableBlock()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
