using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public BoxCollider2D area;

    IEnumerator respawnSlowMotion()
    {
        yield return new WaitForSeconds(10.0f);
        RandomPositionSlower();
    }

    private void Start()
    {
        StartCoroutine(respawnSlowMotion());
    }

    private void RandomPositionSlower()
    {
        Bounds bounds = area.bounds;
        float slowerX = Random.Range(bounds.min.x, bounds.max.x);
        float slowerY = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(slowerX), Mathf.Round(slowerY), 0.0f);

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

