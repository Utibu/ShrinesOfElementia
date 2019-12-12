using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{

    [SerializeField] private GameObject[] bosses = new GameObject[4];
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.Instance.Level;
        GameObject.Instantiate(bosses[level], gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
