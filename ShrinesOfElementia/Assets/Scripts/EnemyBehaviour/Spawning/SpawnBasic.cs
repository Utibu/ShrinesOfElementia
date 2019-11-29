// Author: Sofia Kauko

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasic : MonoBehaviour
{

    [SerializeField]private GameObject spawnling;
    [SerializeField]private int spawnLimit;
    [SerializeField]private GameObject elite;
    [SerializeField]private float eliteSpawnLimit;
    [SerializeField]private float spawnRate;
    [SerializeField]private GameObject[] patrolPoints;

    private int currentSpawnCount;
    private int currentEliteSpawnCount;
    private float countdown;

    //private ArrayList spawnlings = new ArrayList();

    public ArrayList Spawnlings { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        Spawnlings = new ArrayList();
        //EventSystem.Current.RegisterListener<ShrineEvent>(increaseEnemyActivity);
        EventManager.Current.RegisterListener<EnemyDeathEvent>(OnEnemyDeath);
        currentSpawnCount = 0;
        countdown = spawnRate;

        //generate start amount
        for (int i = 0; i<= spawnLimit-(1+eliteSpawnLimit); i++)
        {
            spawnNew();
        }

        //GreaterElementals
        for (int i = currentSpawnCount; i <= spawnLimit - 1; i++)
        {
            spawnNewElite();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //first, fill upp enemies until limit is almost met
        if(currentSpawnCount < spawnLimit) // total nr of active enemies are too few
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)  // time to spawn
            {
                countdown = spawnRate;
                if (currentEliteSpawnCount < eliteSpawnLimit) // we are missing elites
                {
                    spawnNewElite();
                }
                else                                         // we are missing basics
                {
                    spawnNew();
                }
            }
        }
    }

    private void spawnNew()
    {
        currentSpawnCount += 1;
        //fix random x,z within spawn area later
        
        GameObject g = Instantiate(spawnling, gameObject.transform);
        //give enemy 2 random patrolpoints. Consider replacing this with constructor
        GameObject point1 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        GameObject point2 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        g.GetComponent<EnemySM>().SetPatrolPoints(point1, point2);
        g.GetComponent<EnemySM>().SetSpawnArea(this.gameObject);
        Spawnlings.Add(g);

    }


    private void spawnNewElite()
    {
        currentEliteSpawnCount += 1;
        currentSpawnCount += 1;
        //fix random x,z within spawn area later

        GameObject g = Instantiate(elite, gameObject.transform);
        //give enemy 2 random patrolpoints. Consider replacing this with constructor
        GameObject point1 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        GameObject point2 = patrolPoints[Random.Range(0, patrolPoints.Length)];
        g.GetComponent<EnemySM>().SetPatrolPoints(point1, point2);
        g.GetComponent<EnemySM>().SetSpawnArea(this.gameObject);
        Spawnlings.Add(g);

    }

    //Increase difficulty when player gets new ability
    private void increaseEnemyActivity(ShrineEvent ev)
    {
        spawnLimit += 1;
        eliteSpawnLimit += 1;
        spawnRate -= 5;
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
                if (ev.Elite)
                {
                    currentEliteSpawnCount -= 1;
                }
                currentSpawnCount -= 1;
            }
        }
        
    }
}
