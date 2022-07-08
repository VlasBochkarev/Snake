using System.Collections;
using UnityEngine;

public class Mouse : Food
{
    public Food MouseFood;
    
    IEnumerator respawnMouse()
    {
        yield return new WaitForSeconds(15.0f);
        MouseFood.RandomPositionsFood();
    }

    private void Start()
    {
        StartCoroutine(respawnMouse());
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
