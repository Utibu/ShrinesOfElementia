// Author: Joakim Ljung
// Co-Authors: Sofia Kauko, Bilal El Medkouri

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int healthRegeneration;
    [SerializeField] private int regenerationInterval;
    private float regenerationCountdown = 0f;

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
    public bool hasWater; //temporary public
    [SerializeField] private bool hasEarth;
    [SerializeField] private bool hasWind;
    [SerializeField] private bool noCooldown;

    
    
    private void Start()
    {
        fireballTimer = 0.0f;
        geyserTimer = 0.0f;
        windBladeTimer = 0.0f;
        earthSpikesTimer = 0.0f;
        EventManager.Current.RegisterListener<ShrineEvent>(UnlockElement);
        indicatorProjector.SetActive(false);
        if (noCooldown)
        {
            fireballCooldown = 1f;
            geyserCooldown = 1f;
            windBladeCooldown = 1f;
            earthSpikesCooldown = 1f;
        }
    }

    private void Update()
    {
        if (!GetComponent<CombatManager>().InCombat && regenerationCountdown >= regenerationInterval)
        {
            RegenerateHealth();
        }

        regenerationCountdown += Time.deltaTime;

        if (fireballTimer > 0f)
        {
            fireballCooldownButton.GetComponent<Image>().fillAmount -= 1f /fireballCooldown * Time.deltaTime;
            fireballTimer -= Time.deltaTime;
        }

        if(geyserTimer > 0f)
        {
            geyserCooldownButton.GetComponent<Image>().fillAmount -= 1f / geyserCooldown * Time.deltaTime;
            geyserTimer -= Time.deltaTime;
        }

        if (windBladeTimer > 0f)
        {
            windBladeCooldownButton.GetComponent<Image>().fillAmount -= 1f / windBladeCooldown * Time.deltaTime;
            windBladeTimer -= Time.deltaTime;
        }

        if (earthSpikesTimer > 0f)
        {
            earthSpikesCooldownButton.GetComponent<Image>().fillAmount -= 1f / earthSpikesCooldown * Time.deltaTime;
            earthSpikesTimer -= Time.deltaTime;
        }
    }

    public void CheckFireBall()
    {
        if (hasFire && fireballTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);

            Player.Instance.Animator.SetTrigger("CastFireball");
            fireballTimer = fireballCooldown;
            fireballCooldownButton.GetComponent<Image>().fillAmount = 1f;
        }
    }

    private void CastFireBall()
    {
        Player.Instance.Animator.SetBool("InCombat", true);
        fireballSpawnLocation = gameObject.transform.position + Vector3.up.normalized * 1.5f + gameObject.transform.forward * 1.8f;
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnLocation, gameObject.transform.rotation);
        fireball.GetComponent<Fireball>().Caster = gameObject;
        fireball.GetComponent<Rigidbody>().AddForce(CameraReference.Instance.transform.forward * fireballSpeed, ForceMode.VelocityChange);
    }

    public void ToggleAim()
    {
        if (hasWater && geyserTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);
            indicatorProjector.SetActive(true);
        }
        else
        {
            indicatorProjector.SetActive(false);
        }
    }
    public void CheckGeyser()
    {

        /**
         * CHECK RAYCAST PROJECTOR
         * CAST GEYSER AT POINT
         */
        if (hasWater && geyserTimer <= 0f)
        {
            RaycastHit hit;
            Debug.DrawRay(indicatorProjector.transform.position, indicatorProjector.transform.forward);
            if (Physics.Raycast(indicatorProjector.transform.position, indicatorProjector.transform.forward, out hit, geyserRange, layerMask))
            {
                print(hit.collider.gameObject.name);
                if (geyserRange < Vector3.Distance(hit.point, Player.Instance.transform.position))
                {
                    print("geyser hit");
                    geyserSpawnLocation = hit.point;
                }
                geyserSpawnLocation = hit.point;

            }

            print("in cast");
            geyserTimer = geyserCooldown;
            geyserCooldownButton.GetComponent<Image>().fillAmount = 1f;
            Player.Instance.Animator.SetTrigger("CastGeyser");
        }
    }

    private void CastGeyser()
    {
        print("casting");

        Player.Instance.Animator.SetBool("InCombat", true);
        GameObject geyser = Instantiate(geyserPrefab, geyserSpawnLocation, Quaternion.identity);
        geyser.GetComponent<Geyser>().Caster = gameObject;
        EventManager.Current.FireEvent(new GeyserCastEvent(geyserSpawnLocation, moistRange));
    }

    public void CheckWindBlade()
    {
        if(hasWind && windBladeTimer <= 0f)
        {
            Player.Instance.Animator.SetBool("InCombat", true);
            Player.Instance.Animator.SetTrigger("OnWindBlade");
            windBladeTimer = windBladeCooldown;
            windBladeCooldownButton.GetComponent<Image>().fillAmount = 1f;
        }
    }

    private void CastWindBlade()
    {
        GameObject windBlade = Instantiate(windBladePrefab, gameObject.transform.position + Vector3.up.normalized + gameObject.transform.forward * 2f, gameObject.transform.rotation);
        windBlade.GetComponent<WindBlade>().Caster = gameObject;
        windBlade.GetComponent<Rigidbody>().AddForce(Player.Instance.transform.forward * windBladeSpeed, ForceMode.VelocityChange);
    }

    public void CheckEarthSpikes()
    {
        if (hasEarth && earthSpikesTimer <= 0f)
        {
            print("casting earthspikes");
            Player.Instance.Animator.SetTrigger("CastEarthSpikes");
            Player.Instance.Animator.SetBool("InCombat", true);
            earthSpikesTimer = earthSpikesCooldown;
            earthSpikesCooldownButton.GetComponent<Image>().fillAmount = 1f;
        }
    }

    private void CastEarthSpikes()
    {
        Quaternion spikesRotation = Quaternion.Euler(-90f, gameObject.transform.rotation.eulerAngles.y, 0f);
        GameObject earthSpikes = Instantiate(earthSpikesPrefab, gameObject.transform.position + gameObject.transform.forward * 2f, spikesRotation);
        earthSpikes.GetComponent<EarthSpikes>().Caster = gameObject;
        print(earthSpikes.GetComponent<EarthSpikes>().Caster);
        earthSpikes.GetComponent<ParticleSystem>().Play();
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
        regenerationCountdown = 0f;
    }



    private void UnlockElement(ShrineEvent shrineEvent)
    {
        switch (shrineEvent.Element)
        {
            case "Fire":
                EnableFireAbilities();
                fireballCooldownButton.GetComponent<Image>().fillAmount = 0f;
                break;
            case "Water":
                EnableWaterAbilities();
                geyserCooldownButton.GetComponent<Image>().fillAmount = 0f;
                break;
            case "Earth":
                EnableEarthAbilities();
                earthSpikesCooldownButton.GetComponent<Image>().fillAmount = 0f;
                break;
            case "Wind":
                EnableWindAbilities();
                windBladeCooldownButton.GetComponent<Image>().fillAmount = 0f;
                break;
        }
    }
}
