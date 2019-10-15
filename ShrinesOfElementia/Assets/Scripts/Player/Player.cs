// Author: Bilal El Medkouri

using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { private set; get; }
    public HealthComponent Health { private set; get; }
    public Animator Animator { private set; get; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        Health = GetComponent<HealthComponent>();
        Animator = GetComponent<Animator>();
    }
}
