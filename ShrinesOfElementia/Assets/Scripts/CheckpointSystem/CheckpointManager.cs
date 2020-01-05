// Main Author: Joakim Ljung
// Co-Author: Sofia Kauko
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<Vector3> spawnPoints;
    private Vector3 closestSpawn;

    public static CheckpointManager Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    private void Start()
    {
        spawnPoints = new List<Vector3>();
        //spawnPoints.Add(Player.Instance.transform.position);
        spawnPoints.Add(PlayerSpawn.Instance.transform.position);
        EventManager.Instance.RegisterListener<CheckpointEvent>(RegisterSpawn);
        EventManager.Instance.RegisterListener<BossDeathEvent>(ClearCheckpoints);
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
            GameManager.Instance.SaveLatestCheckpoint(point);
        }

    }

    public void ClearCheckpoints(BossDeathEvent eve)
    {
        print("clearing checkpoints");
        spawnPoints.Clear();
    }

    public Vector3 FindNearestSpawnPoint()
    {
        foreach (Vector3 spawn in spawnPoints)
        {
            if (closestSpawn == null)
            {
                closestSpawn = spawn;
            }
            else if (Vector3.Distance(spawn, Player.Instance.transform.position) < Vector3.Distance(closestSpawn, Player.Instance.transform.position))
            {
                closestSpawn = spawn;
            }
        }
        return closestSpawn;
    }
}
