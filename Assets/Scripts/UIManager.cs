using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseButton, playButton, pauseMenu;
    public Button btnPause, btnPlay, btnRestart;
    [SerializeField]
    private AudioSource btnAudioSource;

    public TextMeshProUGUI
        textTime,
        textSnakeLength,
        textScore,
        textBestScore,
        currentScore,
        maxLengthSnake,
        currentTime;

    private float
        timerMin = 0,
        timerSecond = 0,
        timerHour = 0,
        currentSecond = 0,
        currentMin = 0,
        currentHour = 0,
        bestScore = 0,
        bestLength = 0;

    public static bool pauseIsActive = false;

    private void Start()
    {

        pauseMenu.SetActive(false);
        btnPause.onClick.AddListener(PauseGame);
        btnPlay.onClick.AddListener(PlayGame);
        btnRestart.onClick.AddListener(RestartGame);


    }

    private void Update()
    {
        textScore.text = "Score: " + Snake.score;
        textBestScore.text = "Best Score: " + PlayerPrefs.GetFloat("bestScore");
        textSnakeLength.text = "Snake Length: " + Snake.snakeTailLength;
        if (pauseIsActive == false && timerMin < 9)
        {
            textTime.text = "Time: " + "0" + timerHour + " : " + "0" + timerMin + " : " + Secundomer();

        }
        else if(pauseIsActive == false)
        {
            textTime.text = "Time: " + "0" + timerHour + " : " + timerMin + " : " + Secundomer();
        }
        if (PlayerPrefs.GetFloat("bestScore") < Snake.score)
        {
            bestScore = Snake.score;
            PlayerPrefs.SetFloat("bestScore", bestScore);
        }
        if (PlayerPrefs.GetFloat("bestLength") < Snake.snakeTailLength)
        {
            bestLength = Snake.snakeTailLength;
            PlayerPrefs.SetFloat("bestLength", bestLength);
        }
    }



    public void PauseGame()
    {
        btnAudioSource.Play();

        pauseIsActive = true;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        playButton.SetActive(true);

        currentHour = timerHour;
        currentMin = timerMin;
        currentSecond = timerSecond;

        currentScore.text = "Score: " + Snake.score;
        maxLengthSnake.text = "Max length: " + PlayerPrefs.GetFloat("bestLength");
        if(currentMin < 9)
        {
            currentTime.text = "Time: " + 0 + currentHour + " : " + 0 + currentMin + " : " + Mathf.Round(currentSecond);
        }
        else
        {
            currentTime.text = "Time: " + 0 + currentHour + " : " + currentMin + " : " + Mathf.Round(currentSecond);
        }

    }



    public void RestartGame()
    {
        btnAudioSource.Play();

        pauseIsActive = false;
        SceneManager.LoadScene("Main");
        timerMin = 0;
        timerSecond = 0;
        timerHour = 0;
        Snake.score = 0;
        Snake.snakeTailLength = 0;

    }

    private void PlayGame()
    {
        btnAudioSource.Play();

        pauseIsActive = false;
        pauseButton.SetActive(true);
        playButton.SetActive(true);

        pauseMenu.SetActive(false);

    }

    private float Secundomer()
    {

        if (timerSecond < 60) timerSecond += Time.deltaTime;
        if (timerSecond > 60)
        {
            timerMin++;
            timerSecond = 0;
        }
        if (timerMin > 60)
        {
            timerHour++;
            timerMin = 0;
        }
        return Mathf.Round(timerSecond);
    }
}
