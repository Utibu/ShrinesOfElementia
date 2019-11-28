//Author: Sofia Kauko
using UnityEngine;


//[CreateAssetMenu(menuName = "Giant States/Attack States/FireballState")]
public class GiantFireballState : GiantCombatState
{

    [SerializeField] private GameObject fireballPrefab;
    private float fireballSpeed = 9f;
    public override void Enter()
    {
        owner.Animator.SetTrigger("Sweep"); // until we find a proper animation 

        //turn to player and cast fireball with a small delay, to match animation better
        TimerManager.Current.SetNewTimer(owner.gameObject, 0.8f, CastFireball);
        owner.transform.LookAt(Player.Instance.transform);

        //set avaliability to false and a timer that resets avaliability after 10 seconds.
        //owner.FireballAvaliable = false;
        //TimerManager.Current.SetNewTimer(owner.gameObject, owner.FireballCooldown, ResetFireball);
        base.Enter();
    }


    private void CastFireball()
    {
        foreach (Collider collider in owner.GetComponents<Collider>())
        {
            collider.enabled = false;
        }
        //TimerManager.Current.SetNewTimer(owner.gameObject, 1f, );

        Vector3 fireballSpawnLocation = owner.transform.position + Vector3.up.normalized * 1.5f + owner.gameObject.transform.forward * 2f;
        Vector3 direction = owner.transform.forward * fireballSpeed;

        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation, owner.transform.rotation);
        fireball.GetComponent<Fireball>().Caster = owner.gameObject;
        fireball.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
        fireball.transform.localScale *= 2f;
    }

    public void ResetFireball()
    {
        //owner.FireballAvaliable = true;
    }

    public override void Leave()
    {
        foreach (Collider collider in owner.GetComponents<Collider>())
        {
            collider.enabled = true;
        }
        base.Leave();

    }

}
