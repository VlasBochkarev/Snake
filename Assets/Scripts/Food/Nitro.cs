using UnityEngine;


public class Nitro : Food
{
    private void Start()
    {
        StartCoroutine(Respawn(7.0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Player.PLAYER_TAG))
        {
            transform.position = new Vector3(100f, 100f, 0.0f);
            StartCoroutine(Respawn(7.0f));
        }
    }

}
