using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject
        buffEfectNitro,
        buffEfectSlow,
        buffEfectSuperGrow,
        buffEfectGrow,
        effectLose;

    public static float
        speed = 8f,
        score = 0,
        snakeTailLength = 0;

    private float timerForBuffs = 0;
    private bool trigerForBuffs = false;

    public UIManager uiScript;

    private List<Transform> _tail;
    public Transform snakeTailPrefab;

    private void Start()
    {
        _tail = new List<Transform>();
        _tail.Add(transform);


    }

    private void Update()
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

        if (trigerForBuffs) TimerBuff();

    }

    private void FixedUpdate()
    {
        if (UIManager.pauseIsActive == false)
        {
            for (int i = _tail.Count - 1; i > 0; i--)
            {
                _tail[i].position = _tail[i - 1].position;
            }

            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            SpawnAndDestroyPrefabs(buffEfectGrow);
        }
        else if (collision.tag == "Nitro")
        {
            NitroIsActive();
            SpawnAndDestroyPrefabs(buffEfectNitro);

        }
        else if (collision.tag == "Slow")
        {
            SlowMotionIsActive();
            SpawnAndDestroyPrefabs(buffEfectSlow);
        }
        else if (collision.tag == "Mouse")
        {
            SuperGrow();
            SpawnAndDestroyPrefabs(buffEfectSuperGrow);
        }
        else if (collision.tag == "Tail")
        {
            UIManager.pauseIsActive = true;
            StartCoroutine(waitForLoseEffect());
            SpawnAndDestroyPrefabs(effectLose);
        }
        else if (collision.tag == "Obstacle")
        {
            transform.position = -transform.position;
        }
    }

    private void Grow()
    {
        Transform tail = Instantiate(snakeTailPrefab);
        tail.position = _tail[_tail.Count - 1].position;
        _tail.Add(tail);
        snakeTailLength++;
        score += 2;
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
        speed = 12;
        TimerBuff();
        score += 2;
    }

    private void SlowMotionIsActive()
    {
        speed = 5;
        TimerBuff();
        score += 2;
    }
    private void TimerBuff()
    {
        trigerForBuffs = true;
        if (timerForBuffs < 5) timerForBuffs += Time.deltaTime;
        else if (timerForBuffs >= 0)
        {
            timerForBuffs = 0;
            speed = 8;
            trigerForBuffs = false;
        }
    }

    IEnumerator waitForLoseEffect()
    {
        yield return new WaitForSeconds(3.0f);
        UIManager.pauseIsActive = true;
        uiScript.PauseGame();
        uiScript.playButton.SetActive(false);

    }

    private void SpawnAndDestroyPrefabs(GameObject buffEfect)
    {
        GameObject spawnPrefab = Instantiate(buffEfect, transform.position, transform.rotation);
        spawnPrefab.GetComponent<AudioSource>().Play();
        Destroy(spawnPrefab, 1f);
    }

}
