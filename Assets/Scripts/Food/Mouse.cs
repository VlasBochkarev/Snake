using System.Collections;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public BoxCollider2D area;

    IEnumerator respawnMouse()
    {
        yield return new WaitForSeconds(15.0f);
        RandomPositionMouse();
    }

    private void Start()
    {
        StartCoroutine(respawnMouse());
    }

    private void RandomPositionMouse()
    {
        Bounds bounds = area.bounds;
        float mouseX = Random.Range(bounds.min.x, bounds.max.x);
        float mouseY = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(mouseX), Mathf.Round(mouseY), 0.0f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = new Vector3(100f, 100f, 0.0f);
            StartCoroutine(respawnMouse());
        }
    }
}
