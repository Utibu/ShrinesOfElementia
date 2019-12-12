//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantPassiveManager : MonoBehaviour
{
    [SerializeField] private string ElementalType;
    [SerializeField] private GameObject giantPassivePrefab;
    [SerializeField] private float timeUntilActivation;
    public float TimeUntilActivation { get => timeUntilActivation; set => timeUntilActivation = value; }
    public bool IsReady { get; private set; }
    public GameObject instantiatedPrefab { get; set; }
    

    private Dictionary<string, System.Action> passives;
    private GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.RegisterListener<GeyserCastEvent>(DestroyFirePassive);

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
            timer = TimerManager.Instance.SetNewTimer(gameObject, TimeUntilActivation, ResetReady);
        }
    }

    

    //called from animation
    private void ActivatePassive()
    {
        IsReady = false;
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
        if (Vector3.Distance(gameObject.transform.position, ev.affectedPosition) < ev.effectRange)
        {
            Destroy(instantiatedPrefab);
        }
    }


    private void CastWaterPassive()
    {

    }


    private void CastWindPassive()
    {

    }


    private void CastEarthPassive()
    {
        Debug.Log("Vines passive activated");
        instantiatedPrefab = GameObject.Instantiate(giantPassivePrefab, gameObject.transform.position, gameObject.transform.rotation);
        instantiatedPrefab.transform.SetParent(gameObject.transform);
    }

    


}
