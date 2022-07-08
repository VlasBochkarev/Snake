using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject PauseButton;
	public GameObject PlayButton;
	public GameObject PauseMenu;

	public Button BtnPause;
	public Button BtnPlay;
	public Button BtnRestart;

	public TextMeshProUGUI TextTime;
	public TextMeshProUGUI TextSnakeLength;
	public TextMeshProUGUI TextScore;
	public TextMeshProUGUI TextBestScore;
	public TextMeshProUGUI CurrentScore;
	public TextMeshProUGUI MaxLengthSnake;
	public TextMeshProUGUI CurrentTime;

	public static bool PauseIsActive = false;

	[SerializeField]
	private AudioSource BtnAudioSource;

	private float TimerMin = 0;
	private float TimerSecond = 0;
	private float TimerHour = 0;
	private float CurrentSecond = 0;
	private float CurrentMin = 0;
	private float CurrentHour = 0;
	private float BestScore = 0;
	private float BestLength = 0;

	private void Start()
	{
		PauseMenu.SetActive(false);
		BtnPause.onClick.AddListener(PauseGame);
		BtnPlay.onClick.AddListener(PlayGame);
		BtnRestart.onClick.AddListener(RestartGame);
	}

	private void Update()
	{
		ShowTimerScoreLength();
	}

	public void PauseGame()
	{
		BtnAudioSource.Play();
		PauseIsActive = true;
		PauseMenu.SetActive(true);
		PauseButton.SetActive(false);
		PlayButton.SetActive(true);
		CurrentHour = TimerHour;
		CurrentMin = TimerMin;
		CurrentSecond = TimerSecond;
		CurrentScore.text = "Score: " + Snake.Score;
		MaxLengthSnake.text = "Max length: " + PlayerPrefs.GetFloat("bestLength");
		if (CurrentMin < 9)
		{
			CurrentTime.text = "Time: " + 0 + CurrentHour + " : " + 0 + CurrentMin + " : " + Mathf.Round(CurrentSecond);
		}
		else
		{
			CurrentTime.text = "Time: " + 0 + CurrentHour + " : " + CurrentMin + " : " + Mathf.Round(CurrentSecond);
		}
	}



	public void RestartGame()
	{
		BtnAudioSource.Play();
		PauseIsActive = false;
		SceneManager.LoadScene("Main");
		TimerMin = 0;
		TimerSecond = 0;
		TimerHour = 0;
		Snake.Score = 0;
		Snake.SnakeTailLength = 0;

	}

	private void PlayGame()
	{
		BtnAudioSource.Play();
		PauseIsActive = false;
		PauseButton.SetActive(true);
		PlayButton.SetActive(true);
		PauseMenu.SetActive(false);
	}

	private float Secundomer()
	{
		if (TimerSecond < 60) TimerSecond += Time.deltaTime;
		if (TimerSecond > 60)
		{
			TimerMin++;
			TimerSecond = 0;
		}
		if (TimerMin > 60)
		{
			TimerHour++;
			TimerMin = 0;
		}
		return Mathf.Round(TimerSecond);
	}

	private void ShowTimerScoreLength()
    {
		TextScore.text = "Score: " + Snake.Score;
		TextBestScore.text = "Best Score: " + PlayerPrefs.GetFloat("bestScore");
		TextSnakeLength.text = "Snake Length: " + Snake.SnakeTailLength;
		if (PauseIsActive == false && TimerMin < 9)
		{
			TextTime.text = "Time: " + "0" + TimerHour + " : " + "0" + TimerMin + " : " + Secundomer();
		}
		else if (PauseIsActive == false)
		{
			TextTime.text = "Time: " + "0" + TimerHour + " : " + TimerMin + " : " + Secundomer();
		}
		if (PlayerPrefs.GetFloat("bestScore") < Snake.Score)
		{
			BestScore = Snake.Score;
			PlayerPrefs.SetFloat("bestScore", BestScore);
		}
		if (PlayerPrefs.GetFloat("bestLength") < Snake.SnakeTailLength)
		{
			BestLength = Snake.SnakeTailLength;
			PlayerPrefs.SetFloat("bestLength", BestLength);
		}
	}
}
