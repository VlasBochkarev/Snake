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

	private float _timerForBuffs = 0;
	private float _speed = 0.25f;
	private bool _isNitro = false;
	private bool _isSlow = false;
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
		Move();
		if (_trigerForBuffs)
		{
			TimerBuff();
		}
	}


	private void Move()
	{
		if (UIManager.PauseIsActive == false)
		{
			_speed -= Time.deltaTime;
			if (_speed < 0)
			{
				transform.Translate(Vector2.up * 1.1f);

				for (int i = _tail.Count - 1; i > 0; i--)
				{
					_tail[i].position = _tail[i - 1].position;
				}
				if (_tail.Count > 2)
				{
					for (int j = 3; j < _tail.Count; j++)
					{
						_tail[j].GetComponent<BoxCollider2D>().isTrigger = true;
					}
				}
				if (_isNitro)
				{
					_speed = 0.1f;
				}
				else if (_isSlow)
				{
					_speed = 0.5f;
				}
				else
				{
					_speed = 0.25f;
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(Constants.Effects.FOOD_TAG))
		{
			if (_tail.Count == 1)
			{
				Grow();
				Grow();
				EffectsManagerScript.EffectGrow();
			}
			else
			{
				Grow();
				EffectsManagerScript.EffectGrow();
			}
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
		_isNitro = true;
		TimerBuff();
		Score += 2;
	}

	private void SlowMotionIsActive()
	{
		_isSlow = true;
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
			_isSlow = false;
			_isNitro = false;
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
