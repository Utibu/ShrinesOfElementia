// Author: Joakim Ljung, Co-author Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int healthRegeneration;
    [SerializeField] private int regenerationInterval;
    private float regenerationCountdown = 0;

    [Header("Fireball attributes")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed, fireballCooldown;
    private Vector3 fireballSpawnLocation;
    private float fireballTimer;
    [SerializeField] private GameObject fireballCooldownButton;

    [Header("Fire Infusion attributes")]
    [SerializeField] private GameObject fireParticles;
    [SerializeField] private GameObject sword;
    [SerializeField] private int damageIncrease;

    [Header("Geyser attributes")]
    [SerializeField] private GameObject geyserPrefab;
    [SerializeField] private float geyserCooldown;
    //[SerializeField] private GameObject geyserSpawnLocation; // make this simpler? gsl = player.position + forward * 2f eller ngt baserat på kameran
    [SerializeField] private Vector3 geyserSpawnLocation;
    [SerializeField]private float moistRange = 6f;
    [SerializeField] private float geyserRange;
    private float geyserTimer;
    [SerializeField] private GameObject geyserCooldownButton;

    [Header("Spell Indicator")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject indicatorProjector;

    [Header("Wind Blade Attributes")]
    [SerializeField] private GameObject windBladePrefab;
    [SerializeField] private float windBladeCooldown, windBladeSpeed;
    private float windBladeTimer;
    [SerializeField] private GameObject windBladeCooldownButton;


    [Header("Earth Spikes Attributes")]
    [SerializeField] private GameObject earthSpikesPrefab;
    [SerializeField] private float earthSpikesCooldown;
    private float earthSpikesTimer;
    [SerializeField] private GameObject earthSpikesCooldownButton;


    [Header("Touch of Nature")]
    [SerializeField] private int healthRegenerationIncrease;


    [Header("Elemental Abilities (Serialized For Testing)")]
    [SerializeField] private bool hasFire;
    [SerializeField] private bool hasWater;
    [SerializeField] private bool hasEarth;
    [SerializeField] private bool hasWind;
    [SerializeField] private bool noCooldown;

    
    
    private void Start()
    {
        fireballTimer = 0.0f;
        geyserTimer = 0.0f;
        windBladeTimer = 0.0f;
        earthSpikesTimer = 0.0f;
        EventManager.Current.RegisterListener<ShrineEvent>(unlockElement);
        indicatorProjector.SetActive(false);
        if (noCooldown)
        {
            fireballCooldown = 1;
            geyserCooldown = 1;
            windBladeCooldown = 1;
            earthSpikesCooldown = 1;
        }
    }

    private void Update()
    {
        fireballTimer -= Time.deltaTime;
        geyserTimer -= Time.deltaTime;
        windBladeTimer -= Time.deltaTime;
        earthSpikesTimer -= Time.deltaTime;
        regenerationCountdown += Time.deltaTime;
        //print(regenerationCountdown);
        if (!GetComponent<CombatManager>().InCombat && regenerationCountdown >= regenerationInterval)
        {
            RegenerateHealth();
        }

        if(fireballTimer > 0)
        {
            fireballCooldownButton.GetComponent<Image>().fillAmount -= 1/fireballCooldown * Time.deltaTime;
        }

        if(geyserTimer > 0)
        {
            geyserCooldownButton.GetComponent<Image>().fillAmount -= 1 / geyserCooldown * Time.deltaTime;
        }

        if (windBladeTimer > 0)
        {
            windBladeCooldownButton.GetComponent<Image>().fillAmount -= 1 / windBladeCooldown * Time.deltaTime;
        }

        if (earthSpikesTimer > 0)
        {
            earthSpikesCooldownButton.GetComponent<Image>().fillAmount -= 1 / earthSpikesCooldown * Time.deltaTime;
        }
    }


    public void CheckFireBall()
    {
        if (hasFire && fireballTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);

            Player.Instance.Animator.SetTrigger("CastFireball");
        }
    }

    private void CastFireBall()
    {
        Player.Instance.Animator.SetBool("InCombat", true);
        fireballSpawnLocation = gameObject.transform.position + Vector3.up.normalized * 1.5f + gameObject.transform.forward * 1.8f;
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation, gameObject.transform.rotation);
        fireball.GetComponent<Rigidbody>().AddForce(CameraReference.Instance.transform.forward * fireballSpeed, ForceMode.VelocityChange);
        fireballTimer = fireballCooldown;
        fireballCooldownButton.GetComponent<Image>().fillAmount = 1;
    }

    public void ToggleAim(bool aimOn)
    {
        if (hasWater && geyserTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);
            indicatorProjector.SetActive(aimOn);
        }
    }
    public void CheckGeyser()
    {

        /**
         * CHECK RAYCAST PROJECTOR
         * CAST GEYSER AT POINT
         */

        RaycastHit hit;
        Debug.DrawRay(indicatorProjector.transform.position, indicatorProjector.transform.forward);
        if(Physics.Raycast(indicatorProjector.transform.position, indicatorProjector.transform.forward, out hit, geyserRange, layerMask))
        {
            print(hit.collider.gameObject.name);
            if(geyserRange < Vector3.Distance(hit.point, Player.Instance.transform.position))
            {
                print("geyser hit");
                geyserSpawnLocation = hit.point;
            }
            geyserSpawnLocation = hit.point;

        }

        print("in cast");
        if (hasWater && geyserTimer <= 0f)
        {
            Player.Instance.Animator.SetTrigger("CastGeyser");
        }
    }

    private void CastGeyser()
    {
        print("casting");
        Player.Instance.Animator.SetBool("InCombat", true);
        Instantiate(geyserPrefab, geyserSpawnLocation, Quaternion.identity);
        geyserTimer = geyserCooldown;
        EventManager.Current.FireEvent(new GeyserCastEvent(geyserSpawnLocation, moistRange));
        geyserCooldownButton.GetComponent<Image>().fillAmount = 1;
    }

    public void CheckWindBlade()
    {
        if(hasWind && windBladeTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);
            Player.Instance.Animator.SetTrigger("OnWindBlade");
        }
    }

    private void CastWindBlade()
    {
        GameObject windBlade = Instantiate(windBladePrefab, gameObject.transform.position + Vector3.up.normalized + gameObject.transform.forward * 2f, gameObject.transform.rotation);
        windBlade.GetComponent<Rigidbody>().AddForce(Player.Instance.transform.forward * windBladeSpeed, ForceMode.VelocityChange);
        windBladeTimer = windBladeCooldown;
        windBladeCooldownButton.GetComponent<Image>().fillAmount = 1;
    }

    public void CheckEarthSpikes()
    {
        if (hasEarth && earthSpikesTimer <= 0f)
        {
            print("casting earthspikes");
            Player.Instance.Animator.SetTrigger("CastEarthSpikes");
            Player.Instance.Animator.SetBool("InCombat", true);

        }
    }

    private void CastEarthSpikes()
    {
        Quaternion spikesRotation = Quaternion.Euler(-90, gameObject.transform.rotation.eulerAngles.y, 0);
        GameObject earthSpikes = Instantiate(earthSpikesPrefab, gameObject.transform.position + gameObject.transform.forward * 2f, spikesRotation);
        earthSpikes.GetComponent<ParticleSystem>().Play();
        earthSpikesTimer = earthSpikesCooldown;
        earthSpikesCooldownButton.GetComponent<Image>().fillAmount = 1;
    }

    private void EnableFireAbilities()
    {
        hasFire = true;
        fireParticles.SetActive(true);
        sword.GetComponent<Sword>().SetDamage(damageIncrease);
    }

    private void EnableWaterAbilities()
    {
        hasWater = true;
        Physics.IgnoreLayerCollision(9, 4, false);
    }

    private void EnableEarthAbilities()
    {
        hasEarth = true;
        healthRegeneration *= healthRegenerationIncrease;
    }

    private void EnableWindAbilities()
    {
        hasWind = true;
        Player.Instance.GetComponent<MovementInput>().ActivateGlide();
    }


    private void RegenerateHealth()
    {
        print("regenerating");
        Player.Instance.Health.CurrentHealth += healthRegeneration;
        regenerationCountdown = 0;
    }



    private void unlockElement(ShrineEvent eve)
    {
        switch (eve.Element)
        {
            case "Fire":
                EnableFireAbilities();
                fireballCooldownButton.GetComponent<Image>().fillAmount = 0;
                break;
            case "Water":
                EnableWaterAbilities();
                geyserCooldownButton.GetComponent<Image>().fillAmount = 0;
                break;
            case "Earth":
                EnableEarthAbilities();
                earthSpikesCooldownButton.GetComponent<Image>().fillAmount = 0;
                break;
            case "Wind":
                EnableWindAbilities();
                windBladeCooldownButton.GetComponent<Image>().fillAmount = 0;
                break;
        }
    }
}
