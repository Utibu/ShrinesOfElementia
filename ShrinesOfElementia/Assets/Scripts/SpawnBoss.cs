//Authoe: Sofia Kauko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{

    [SerializeField] private GameObject[] bosses = new GameObject[4];
    [SerializeField] private GameObject[] bossAreabounds = new GameObject[4];

    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        level = GameManager.Instance.Level;
        GameObject.Instantiate(bosses[level], gameObject.transform.position, gameObject.transform.rotation);
        bossAreabounds[level].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
