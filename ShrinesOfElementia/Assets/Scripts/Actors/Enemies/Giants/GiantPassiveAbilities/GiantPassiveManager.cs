//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantPassiveManager : MonoBehaviour
{
    
    [SerializeField] private GameObject giantPassivePrefab;
    [SerializeField] private float timeUntilActivation;
    public float TimeUntilActivation { get => timeUntilActivation; set => timeUntilActivation = value; }
    public bool IsReady { get; private set; }
    public GameObject instantiatedPrefab { get; set; }
    

    private Dictionary<string, System.Action> passives;
    private GameObject timer;
    private string ElementalType;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterListener<GeyserCastEvent>(DestroyFirePassive);
        EventManager.Instance.RegisterListener<FireAbilityEvent>(DestroyWindPassive);
        EventManager.Instance.RegisterListener<EarthAbilityEvent>(DestroyWaterPassive);

        ElementalType = GetComponent<Giant>().ElementalType;
        IsReady = true;
        passives = new Dictionary<string, System.Action>();
        passives.Add("Fire", CastFirePassive);
        passives.Add("Water", CastWaterPassive);
        passives.Add("Wind", CastWindPassive);
        passives.Add("Earth", CastEarthPassive);
    }

    void Update()
    {
        if(instantiatedPrefab == null && IsReady == false && timer == null)
        {
            IsReady = false;
            StopElementalParticles();
            timer = TimerManager.Instance.SetNewTimer(gameObject, TimeUntilActivation, ResetReady);
        }
    }

    

    //called from animation
    private void ActivatePassive()
    {
        IsReady = false;
        StartElementalParticles();
        passives[ElementalType]();
        
    }

    private void ResetReady()
    {
        IsReady = true;
    }

    
    private void CastFirePassive()
    {
        Debug.Log("Burn damage Enabled");
        instantiatedPrefab = GameObject.Instantiate(giantPassivePrefab, gameObject.transform.position, gameObject.transform.rotation);
        instantiatedPrefab.transform.SetParent(gameObject.transform);
    }
    private void DestroyFirePassive(GeyserCastEvent ev)
    {
        if (ElementalType == "Fire" && Vector3.Distance(gameObject.transform.position, ev.affectedPosition) < ev.effectRange)
        {
            Debug.Log("passive prefab is destroyed.");
            Destroy(instantiatedPrefab);
        }
    }


    private void CastWaterPassive()
    {
        /*
        instantiatedPrefab = GameObject.Instantiate(giantPassivePrefab, gameObject.transform.position, gameObject.transform.rotation);
        instantiatedPrefab.transform.SetParent(gameObject.transform);
        */
    }
    private void DestroyWaterPassive(EarthAbilityEvent ev)
    {
        /*
        if (ElementalType == "Water" && Vector3.Distance(gameObject.transform.position, ev.PointOfOrigin) < 7f)
        {
            Debug.Log("Water passive prefab is destroyed.");
            Destroy(instantiatedPrefab);
        }
        */
    }


    private void CastWindPassive()
    {
        Debug.Log("wind passive activated");
        instantiatedPrefab = GameObject.Instantiate(giantPassivePrefab, gameObject.transform.position, gameObject.transform.rotation);
        instantiatedPrefab.transform.SetParent(gameObject.transform);
    }
    private void DestroyWindPassive(FireAbilityEvent ev)
    {
        if (ElementalType == "Wind" && Vector3.Distance(gameObject.transform.position, ev.PointOfOrigin) < ev.EffectRange * 2)
        {
            Destroy(instantiatedPrefab);
        }
    }


    private void CastEarthPassive()
    {
        Debug.Log("Vines passive activated");
        instantiatedPrefab = GameObject.Instantiate(giantPassivePrefab, gameObject.transform.position, gameObject.transform.rotation);
        instantiatedPrefab.transform.SetParent(gameObject.transform);
    }


    //stop and start the elemental partices when aura is on/off
    public void StopElementalParticles()
    {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }
    }

    public void StartElementalParticles()
    {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
    }


}
