using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<Vector3> spawnPoints;
    private Vector3 closestSpawn;

    private void Start()
    {
        spawnPoints = new List<Vector3>();
        spawnPoints.Add(Player.Instance.transform.position);
        EventManager.Current.RegisterListener<CheckpointEvent>(RegisterSpawn);
        EventManager.Current.RegisterListener<PlayerDeathEvent>(Respawn);
    }

    private void RegisterSpawn(CheckpointEvent eve)
    {
        if (!(spawnPoints.Contains(eve.SpawnPosition)) && eve.SpawnPosition != null)
        {
            spawnPoints.Add(eve.SpawnPosition);
            print(eve.eventDescription);
        }
    }

    private void Respawn(PlayerDeathEvent eve)
    {
        print(eve.eventDescription);
        foreach(Vector3 spawn in spawnPoints)
        {
            if(closestSpawn == null)
            {
                closestSpawn = spawn;
            }
            else if(Vector3.Distance(spawn, eve.Player.transform.position) < Vector3.Distance(closestSpawn, eve.Player.transform.position))
            {
                closestSpawn = spawn;
            }
        }
        print("moving " + eve.Player.gameObject.name);
        eve.Player.transform.position = closestSpawn;
    }
}
