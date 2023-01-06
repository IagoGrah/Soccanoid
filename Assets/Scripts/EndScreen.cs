using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI highScoreText;

    float clickBuffer = 1f;
    float bufferCounter;

    void Start()
    {
        var gm = FindObjectOfType<GameManager>();
        gm.SetTimer(false);

        timerText.text = gm.GetTimer();
        highScoreText.text = gm.SetHighScore() ? "PERSONAL BEST" : $"BEST: {gm.GetHighScore(gm.CurrSpeed) ?? ""}";
    }

    private void Update()
    {
        bufferCounter += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && bufferCounter > clickBuffer)
        {
            FindObjectOfType<SceneLoader>().ResetGame();
        }
    }
}
