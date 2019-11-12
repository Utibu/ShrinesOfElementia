// Author: Bilal El Medkouri

using UnityEngine;

[CreateAssetMenu(menuName = "Giant States/SpawnState")]
public class GiantSpawnState : GiantBaseState
{
    public override void Enter()
    {
        MonoBehaviour.print("Spawn");

        //owner.Animator.SetTrigger("Spawn");

        base.Enter();
    }
}