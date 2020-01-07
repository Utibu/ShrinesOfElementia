// Author: Bilal El Medkouri

using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    public static Player Instance { get; private set; }
    public MovementInput MovementInput { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public HealthComponent Health { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public int healthDropsAmount = 20;

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }

        Health = GetComponent<HealthComponent>();
        Animator = GetComponent<Animator>();
        MovementInput = GetComponent<MovementInput>();
        PlayerInput = GetComponent<PlayerInput>();
        CharacterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //base.Awake();
    }

    private void Start()
    {
        BalancingManager.Instance.SetHealthOrbsValue(healthDropsAmount);
    }
}
