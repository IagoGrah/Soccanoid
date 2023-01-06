using TMPro;
using UnityEngine;

public class HighScoresDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI slowText;
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI fastText;
    
    void Start()
    {
        var gm = FindObjectOfType<GameManager>();

        var slowScore = gm.GetHighScore(Speed.SLOW);
        slowText.text = slowScore != null ? $"BEST: {slowScore}" : "";

        var normalScore = gm.GetHighScore(Speed.NORMAL);
        normalText.text = normalScore != null ? $"BEST: {normalScore}" : "";

        var fastScore = gm.GetHighScore(Speed.FAST);
        fastText.text = fastScore != null ? $"BEST: {fastScore}" : "";
    }
}
