// Author: Bilal El Medkouri

using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Deals 10 damage to the player
            player.Health.CurrentHealth -= 10;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            // Heals the player for 10 hp
            player.Health.CurrentHealth += 10;
        }
    }
}
