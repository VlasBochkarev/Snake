using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _area;

    public IEnumerator Respawn(float Time)
    {
        yield return new WaitForSeconds(Time);
        RandomPositionsFood();
    }

    private void Start()
    {
        RandomPositionsFood();
    }

    public void RandomPositionsFood()
    {
        Bounds bounds = _area.bounds;
        float aplleX = Random.Range(bounds.min.x, bounds.max.x);
        float aplleY = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(aplleX), Mathf.Round(aplleY), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Player.PLAYER_TAG))
        {
            RandomPositionsFood();
        }
    }
}
