//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellManager : MonoBehaviour
{
    [SerializeField] private string elementType;
    public string ElementType { get => elementType; set => elementType = value; }
    

    [SerializeField] private GameObject spellPrefab;
    public GameObject SpellPrefab { get => spellPrefab; set => spellPrefab = value; }
    

    [SerializeField] private float castTime;
    public float CastTime { get => castTime; set => castTime = value; }
    

    [SerializeField] private float spellSpeedModifier;
    public float SpellSpeedModifier { get => spellSpeedModifier; set => spellSpeedModifier = value; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
