//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject AbilityPrefab { get { return gameObject; } }
    public string CasterTag { get { return casterTag; } set { casterTag = value; } }
    protected string casterTag = "";
    [SerializeField] protected AbilityIndicator abilityIndicator;
    protected Vector3 abilitySpawnLocation;


    protected virtual void AimAbility()
    {

    }

    protected virtual void CastAbility()
    {
        //Player.Instance.Animator.SetBool("InCombat", true);
    }

    protected virtual void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
