using UnityEngine;
using System.Collections;

public class Nitro : Food
{

    public Food NitroFood;

    IEnumerator respawnNitro()
    {
        yield return new WaitForSeconds(7.0f);
        NitroFood.RandomPositionsFood();
    }

    private void Start()
    {
        StartCoroutine(respawnNitro());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Player.PLAYER_TAG))
        {
            transform.position = new Vector3(100f, 100f, 0.0f);
            StartCoroutine(respawnNitro());
        }
    }

}
