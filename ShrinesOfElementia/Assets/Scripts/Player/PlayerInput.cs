// Author: Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = Player.Instance;
    }
}
