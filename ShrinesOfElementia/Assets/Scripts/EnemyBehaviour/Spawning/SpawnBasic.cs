// Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasic : MonoBehaviour
{

    [SerializeField]private GameObject spawnling;
    [SerializeField] private GameObject elite;
    [SerializeField]private int spawnLimit;
    [SerializeField]private float spawnRate;
    [SerializeField]private GameObject[] patrolPoints;

    private int currentSpawnCount;
    private float countdown;

    private ArrayList spawnlings = new ArrayList();


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener<ShrineEvent>(increaseEnemyActivity);
        EventSystem.Current.RegisterListener<EnemyDeathEvent>(OnEnemyDeath);
        currentSpawnCount = 0;
        countdown = spawnRate;

        //generate start amount
        for (int i = 0; i<= spawnLimit-1; i++)
        {
            spawnNew();
        }

    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (currentSpawnCount < spawnLimit && countdown <= 0)
        {
            //check spawnrate timer
            countdown = spawnRate;
            spawnNew();
        }
    }

    private void spawnNew()
    {
        currentSpawnCount += 1;
        //fix random x,z within spawn area later
        
        GameObject g = Instantiate(spawnling, GetComponent<Collider>().transform);
        //give enemy 2 random patrolpoints
        GameObject point1 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        GameObject point2 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        g.GetComponent<EnemySM>().SetPatrolPoints(point1, point2);
        g.GetComponent<EnemySM>().setSpawnArea(this.gameObject);
        spawnlings.Add(g);

    }


    private void increaseEnemyActivity(ShrineEvent ev)
    {
        spawnLimit *= 2;
        spawnRate /= 2;
    }

    private void OnEnemyDeath(EnemyDeathEvent ev)
    {
        // whats the fookin problem here 
        //spawnlings.Remove(ev.Enemy);

        Debug.Log("enemy removed from spawnling list.");
        if(ev.SpawnArea != null)
        {
            if (ev.SpawnArea.GetInstanceID().Equals(this.gameObject.GetInstanceID()))
            {
                currentSpawnCount -= 1;
            }
        }
        
    }
}
