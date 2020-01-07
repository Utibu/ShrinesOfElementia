using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBalanceVariable : MonoBehaviour
{

    public BalancingVariables type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(string val)
    {
        switch (type)
        {
            case BalancingVariables.PlayerHealth:
                BalancingManager.Instance.SetPlayerHealth(float.Parse(val.Replace('.', ',')));
                break;
            case BalancingVariables.PlayerRegen:
                BalancingManager.Instance.SetPlayerRegen(float.Parse(val.Replace('.', ',')));
                break;
            case BalancingVariables.HealthDrops:
                BalancingManager.Instance.SetHealthOrbsValue(float.Parse(val.Replace('.', ',')));
                break;
            case BalancingVariables.PlayerAttack:
                BalancingManager.Instance.SetPlayerAttack(float.Parse(val.Replace('.', ',')));
                break;
            default:
                break;
        }
    }
}
