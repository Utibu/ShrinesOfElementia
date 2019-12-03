//Author: Joakim Ljung
//Co-Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<Vector3> spawnPoints;
    private Vector3 closestSpawn;

    private static CheckpointManager current;
    public static CheckpointManager Current
    {
        get
        {
            if (current == null)
            {
                current = GameObject.FindObjectOfType<CheckpointManager>();
            }
            return current;
        }
    }

    private void Start()
    {
        spawnPoints = new List<Vector3>();
        spawnPoints.Add(Player.Instance.transform.position);
        EventManager.Current.RegisterListener<CheckpointEvent>(RegisterSpawn);
    }

    private void RegisterSpawn(CheckpointEvent eve)
    {
        if (!spawnPoints.Contains(eve.SpawnPosition) && eve.SpawnPosition != null)
        {
            spawnPoints.Add(eve.SpawnPosition);
            print(eve.eventDescription);
            //save to gamemanager
            Vector3 point = eve.SpawnPosition;
            Debug.Log(point.x + " " + point.y + " " + point.z);
            GameManager.Current.SaveLatestCheckpoint(point);
        }
        
    }

    public Vector3 FindNearestSpawnPoint()
    {
        foreach(Vector3 spawn in spawnPoints)
        {
            if(closestSpawn == null)
            {
                closestSpawn = spawn;
            }
            else if(Vector3.Distance(spawn, Player.Instance.transform.position) < Vector3.Distance(closestSpawn, Player.Instance.transform.position))
            {
                closestSpawn = spawn;
            }
        }
        return closestSpawn;
    }
}
