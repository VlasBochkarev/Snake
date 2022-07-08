using System.Collections;
using UnityEngine;

public class SlowMotion : Food
{
	public Food SlowFood;

	IEnumerator respawnSlowMotion()
	{
		yield return new WaitForSeconds(10.0f);
		SlowFood.RandomPositionsFood();
	}

	private void Start()
	{
		StartCoroutine(respawnSlowMotion());
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			transform.position = new Vector3(100f, 100f, 0.0f);
			StartCoroutine(respawnSlowMotion());
		}
	}
}

