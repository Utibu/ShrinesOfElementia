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
    public bool IsActive { get; set; }
    public bool IsReady { get; private set; }
    private GameObject instantiatedPrefab;
    

    private Dictionary<string, System.Action> passives;

    // Start is called before the first frame update
    void Start()
    {
        IsReady = true;
        passives = new Dictionary<string, System.Action>();
        passives.Add("Fire", CastFirePassive);
        passives.Add("Water", CastWaterPassive);
        passives.Add("Wind", CastWindPassive);
        passives.Add("Earth", CastEarthPassive);
    }


    //called from animation or somewher else.
    private void DisablePassive()
    {
        IsActive = false;
        Destroy(instantiatedPrefab);
    }

    //called from animation
    private void ActivatePassive()
    {
        IsActive = true;
        IsReady = false;
        passives[ElementalType]();
        TimerManager.Current.SetNewTimer(gameObject, timeUntilActivation, ResetReady);
    }

    private void ResetReady()
    {
        IsReady = true;
    }

    
    private void CastFirePassive()
    {
        Debug.Log("Burn damage Enabled");
        gameObject.GetComponentInChildren<BurnDamage>().EnableBurn();
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
    }

    


}
