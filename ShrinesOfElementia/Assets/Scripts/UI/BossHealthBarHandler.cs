// Author: Bilal El Medkouri

using UnityEngine;

public class BossHealthBarHandler : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;

    private void Awake()
    {
        bossHealthBar.SetActive(false);
    }
    private void Start()
    {
        //BossEvents.Instance.OnBossFightAreaTriggerEnter += OnBossAreaEnter;
        print(gameObject + " subscribed to BossFightAreaTriggerEnter");
    }

    public void OnBossAreaEnter()
    {
        bossHealthBar.SetActive(true);
        //BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }

    private void OnDestroy()
    {
        //BossEvents.Instance.OnBossFightAreaTriggerEnter -= OnBossAreaEnter;
    }
}
