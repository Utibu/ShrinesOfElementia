using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }
}
