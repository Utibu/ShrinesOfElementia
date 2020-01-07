// Author: Bilal El Medkouri
// Co-Author: Joakim Ljung

using UnityEngine;

public class GiantAttackCollisionHandler : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool playerHit;
    private Vector3 hitPoint;

    /*  Attack collision with collider (no trigger)
    private void OnCollisionEnter(Collision collision)
    {
        if (Giant.Instance.AttackHit)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            attackCollider.enabled = false;
            Giant.Instance.AttackHit = true;
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + collision.gameObject, damage, gameObject, collision.gameObject);
            EventManager.Current.FireEvent(damageEvent);
        }
    }
    */

    //Attack collision with trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
            EventManager.Instance.FireEvent(new BlockEvent("bossBlock", 35));
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            hitPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position); //used to find impact point
            DamageEvent damageEvent = new DamageEvent(gameObject + " has dealt " + damage + " damage to " + other.gameObject, damage, gameObject, other.gameObject);
            EventManager.Instance.FireEvent(damageEvent);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(hitPoint, .2f);
    }
}
