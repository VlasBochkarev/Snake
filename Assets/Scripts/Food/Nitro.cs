using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Nitro : MonoBehaviour
{

    public BoxCollider2D area;

    IEnumerator respawnNitro()
    {
        yield return new WaitForSeconds(7.0f);
        RandomPositionNitro();
    }

    private void Start()
    {
        StartCoroutine(respawnNitro());
    }

    private void RandomPositionNitro()
    {
        Bounds bounds = area.bounds;
        float nitroX = Random.Range(bounds.min.x, bounds.max.x);
        float nitroY = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(nitroX), Mathf.Round(nitroY), 0.0f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = new Vector3(100f, 100f, 0.0f);
            StartCoroutine(respawnNitro());
        }
    }

}
