using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

	public EffectsManager EffectsManagerScript;
	public Transform SnakeTailPrefab;
	public UIManager UiScript;

	public static float Score = 0;
	public static float SnakeTailLength = 0;

	private float _speed = 8f;
	private float _timerForBuffs = 0;
	private bool _trigerForBuffs = false;
	private List<Transform> _tail;


    private void Start()
	{
		_tail = new List<Transform>();
		_tail.Add(transform);
	}

	private void Update()
	{
		SnakeController();

		if (_trigerForBuffs)
		{
			TimerBuff();
		}

	}

	private void FixedUpdate()
	{
		if (UIManager.PauseIsActive == false)
		{
			for (int i = _tail.Count - 1; i > 0; i--)
			{
				_tail[i].position = _tail[i - 1].position;
			}

			transform.Translate(Vector2.up * _speed * Time.fixedDeltaTime);
		}
	}

	//public float GetSpeed()
	//{
	//	return _speed;
	//}

	public void SetSpeed(float _speed)
	{
		
		if (_speed < 5)
		{
			this._speed = 5;
			return;
		}
		if (_speed > 12)
		{
			this._speed = 12;
			return;
		}
		this._speed = _speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(Constants.Effects.FOOD_TAG))
		{
			Grow();
            EffectsManagerScript.EffectGrow();
		}
		else if (collision.CompareTag(Constants.Effects.NITRO_TAG))
		{
			NitroIsActive();
			EffectsManagerScript.EffectNitro();

		}
		else if (collision.CompareTag(Constants.Effects.SLOW_MOTION_TAG))
		{
			SlowMotionIsActive();
			EffectsManagerScript.EffectSlow();
		}
		else if (collision.CompareTag(Constants.Effects.MOUSE_TAG))
		{
			SuperGrow();
			EffectsManagerScript.EffectSuperGrow();
		}
		else if (collision.CompareTag(Constants.Obstacles.BASE_OBSTACLE_TAG))
		{
			UIManager.PauseIsActive = true;
			StartCoroutine(waitForLoseEffect());
			EffectsManagerScript.EffectGameOver();
			
		}
	}

	private void SnakeController()
	{
		if (Input.GetKeyDown(KeyCode.A) && transform.rotation != Quaternion.Euler(0, 0, -90))
		{
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}
		if (Input.GetKeyDown(KeyCode.D) && transform.rotation != Quaternion.Euler(0, 0, 90))
		{
			transform.rotation = Quaternion.Euler(0, 0, -90);
		}
		if (Input.GetKeyDown(KeyCode.W) && transform.rotation != Quaternion.Euler(0, 0, 180))
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		if (Input.GetKeyDown(KeyCode.S) && transform.rotation != Quaternion.Euler(0, 0, 0))
		{
			transform.rotation = Quaternion.Euler(0, 0, -180);
		}
	}

	private void Grow()
	{
		Transform tail = Instantiate(SnakeTailPrefab);
		tail.position = _tail[_tail.Count - 1].position;
		_tail.Add(tail);
		SnakeTailLength++;
		Score += 2;
	}

	private void SuperGrow()
	{
		for (int i = 0; i < 5; i++)
		{
			Grow();
		}
	}

	private void NitroIsActive()
	{
		SetSpeed(12);
		TimerBuff();
		Score += 2;
	}

	private void SlowMotionIsActive()
	{
		SetSpeed(5);
		TimerBuff();
		Score += 2;
	}
	private void TimerBuff()
	{
		_trigerForBuffs = true;
		if (_timerForBuffs < 5) _timerForBuffs += Time.deltaTime;
		else if (_timerForBuffs >= 0)
		{
			_timerForBuffs = 0;
			SetSpeed(8);
			_trigerForBuffs = false;
		}
	}

	IEnumerator waitForLoseEffect()
	{
		yield return new WaitForSeconds(3.0f);
		UIManager.PauseIsActive = true;
		UiScript.PauseGame();
		UiScript.PlayButton.SetActive(false);

	}

}
