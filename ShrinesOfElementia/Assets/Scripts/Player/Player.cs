// Author: Bilal El Medkouri

using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public HealthComponent Health { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; Debug.Log("Player Instance set to " + this); }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        Debug.Log("Health before getter: " + Health);
        Health = GetComponent<HealthComponent>();
        Debug.Log("Health after: " + Health);

        Debug.Log("Animator before getter: " + Animator);
        Animator = GetComponent<Animator>();
        Debug.Log("Animator after: " + Animator);
    }
}
