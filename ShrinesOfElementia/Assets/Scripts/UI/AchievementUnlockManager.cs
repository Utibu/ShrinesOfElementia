using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUnlockManager : MonoBehaviour
{
    //[SerializeField] private Sprite[] achievementTextures;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private Image achievementImage;
    [SerializeField] private AnimationClip animationClip;
    private Animator animator;
    

    private void Start()
    {
        EventManager.Instance.RegisterListener<AchievementEvent>(OnAchievementEvent);
        animator = GetComponent<Animator>();
    }

    private void OnAchievementEvent(AchievementEvent eve)
    {
        foreach(Sprite s in sprites)
        {
            print(s.name);
            if (s.name.Equals(eve.AchievementName))
            {
                print("showing achievement");
                achievementImage.sprite = s;
                achievementImage.enabled = true;
                animator.SetTrigger("OnAchievement");
                return;
            }
        }
    }
}
