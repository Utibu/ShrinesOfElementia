// Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    private BoxCollider shieldCollider;
    [SerializeField] private Slider blockMeter;
    [SerializeField] private float maxBlockAmount;
    [SerializeField] private float drainDelay;
    [SerializeField] private float drainAmount;
    private float staggerTime = 1f;
    private float staggerTimer;
    private bool isStaggered = false;
    private float currentBlockAmount;
    private float drainTimer;


    private void Start()
    {
        shieldCollider = GetComponent<BoxCollider>();
        shieldCollider.enabled = false;
        EventManager.Instance.RegisterListener<BlockEvent>(OnBlock);
        blockMeter.value = 0;
        blockMeter.maxValue = maxBlockAmount;
        blockMeter.gameObject.SetActive(false);
        currentBlockAmount = 0;
        drainTimer = 0;
    }

    private void Update()
    {
        blockMeter.value = currentBlockAmount;

        

        if(currentBlockAmount > 0)
        {
            blockMeter.gameObject.SetActive(true);
        }

        if (IsBlocking())
        {
            drainTimer = drainDelay;
        }

        else if(!IsBlocking() && currentBlockAmount < 0)
        {
            blockMeter.gameObject.SetActive(false);
        }

        if(currentBlockAmount > 0 && !IsBlocking())
        {
            drainTimer -= Time.deltaTime;
            if(drainTimer <= 0)
            {
                currentBlockAmount -= drainAmount * Time.deltaTime;
            }
        }

        Stagger(isStaggered);

    }

    private void OnBlock(BlockEvent eve)
    {
        gameObject.GetComponentInParent<Animator>().SetTrigger("OnBlock");
        //drainTimer = drainDelay;
        currentBlockAmount += eve.BlockAmount;
        print(currentBlockAmount + " event block: " + eve.BlockAmount);
        if(currentBlockAmount >= maxBlockAmount)
        {
            print("out of block juice");
            gameObject.GetComponentInParent<Animator>().SetTrigger("OnBlockStagger");
            isStaggered = true;
            currentBlockAmount = 0;
            staggerTimer = staggerTime;
        }
    }

    private void Stagger(bool staggered)
    {
        if (staggered)
        {
            staggerTimer -= Time.deltaTime;
            if (staggerTimer > 0)
            {
                gameObject.GetComponentInParent<Animator>().SetLayerWeight(1, 0);
            }
            else
            {
                gameObject.GetComponentInParent<Animator>().SetLayerWeight(1, 1);
                isStaggered = false;
                staggerTimer = 0;
            }
        }
    }

    private bool IsBlocking()
    {
        return gameObject.GetComponentInParent<Animator>().GetBool("IsBlocking");
    }
}
