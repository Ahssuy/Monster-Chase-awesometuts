using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject Spawnedmonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            Spawnedmonster = Instantiate(monsterReference[randomIndex]);

            //left side
            if (randomSide == 0)
            {
                Spawnedmonster.transform.position = leftPos.position;
                Spawnedmonster.GetComponent<Monster>().speed = Random.Range(4, 10);
            }

            // right side
            else
            {
                Spawnedmonster.transform.position = rightPos.position;
                Spawnedmonster.GetComponent<Monster>().speed = -Random.Range(4, 10);
                Spawnedmonster.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}