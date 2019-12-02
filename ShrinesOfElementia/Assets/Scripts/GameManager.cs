//Author: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    
    public int Level { get; set; }
    public int PlayerLevel { get; set; }
    public string[] ShrinesUnlocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void loadLevel()
    {
        //load boss and spawn enemies according to nr of srines unlocked and what level is laoding.
    }
}
