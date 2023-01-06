using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Speed CurrSpeed { get; set; } = Speed.NORMAL;

    [SerializeField] float slowBall = 10f;
    [SerializeField] float normalBall = 15f;
    [SerializeField] float fastBall = 20f;

    [SerializeField] float slowKick = 1.5f;
    [SerializeField] float normalKick = 1.0f;
    [SerializeField] float fastKick = 0.5f;

    [SerializeField] TextMeshProUGUI timerText;
    float timer;
    bool timerOn;

    void Awake()
    {
        var gameManagerCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;

            if (timerText != null)
            {
                float minutes = Mathf.FloorToInt(timer / 60);
                float seconds = Mathf.FloorToInt(timer % 60);
                float milliseconds = (timer % 1) * 1000;

                timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
            }
        }
    }

    public void SetTimer(bool state)
    {
        timerOn = state;
        timerText.enabled = state;
    }

    public float GetBallSpeed()
    {
        switch (CurrSpeed)
        {
            case Speed.SLOW:
                return slowBall;

            case Speed.NORMAL:
            default:
                return normalBall;

            case Speed.FAST:
                return fastBall;
        }
    }

    public float GetKickCooldown()
    {
        switch (CurrSpeed)
        {
            case Speed.SLOW:
                return slowKick;

            case Speed.NORMAL:
            default:
                return normalKick;

            case Speed.FAST:
                return fastKick;
        }
    }

    public string GetTimer()
    {
        return timerText.text;
    }

    public string GetHighScore(Speed speed)
    {
        var speedName = speed == Speed.SLOW ? "Slow" : speed == Speed.FAST ? "Fast" : "Normal";
        if (!PlayerPrefs.HasKey($"{speedName}HighScore"))
        {
            return null;
        }
        var totalSeconds = PlayerPrefs.GetFloat($"{speedName}HighScore", 0f);
        float minutes = Mathf.FloorToInt(totalSeconds / 60);
        float seconds = Mathf.FloorToInt(totalSeconds % 60);
        float milliseconds = (totalSeconds % 1) * 1000;

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public bool SetHighScore()
    {
        var speedName = CurrSpeed == Speed.SLOW ? "Slow" : CurrSpeed == Speed.FAST ? "Fast" : "Normal";
        
        if (timer > 0f && timer < PlayerPrefs.GetFloat($"{speedName}HighScore", float.MaxValue))
        {
            PlayerPrefs.SetFloat($"{speedName}HighScore", timer);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}
