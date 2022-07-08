using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D area;

    private void Start()
    {
        RandomPositionsFood();
    }

    public void RandomPositionsFood()
    {
        Bounds bounds = area.bounds;
        float aplleX = Random.Range(bounds.min.x, bounds.max.x);
        float aplleY = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(aplleX), Mathf.Round(aplleY), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RandomPositionsFood();
        }
    }
}
