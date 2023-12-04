using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance { get; private set; }
    private Dictionary<string, bool> achievementsUnlocked = new Dictionary<string, bool>();

    AudioManager audioManager;

    [SerializeField] GameObject AchievementUIPlayer1;
    [SerializeField] GameObject AchievementUIPlayer2;

    public TextMeshProUGUI firstBloodTextPlayer1;
    public TextMeshProUGUI firstBloodTextPlayer2;

    private void Awake()
    {
        // only one instance of Achievement Manager is running
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        achievementsUnlocked["First Blood"] = false;
        //achievementsUnlocked["Office Warrior"] = false;
        achievementsUnlocked["Survivor"] = false;
    }


    public bool IsAchievementUnlocked(string achievementName)
    {
        if (achievementsUnlocked.ContainsKey(achievementName))
        {
            return achievementsUnlocked[achievementName];
        }
        return false;
    }

    public void AwardFirstBloodAchievement(PlayerDamage player, string achievementName)
    {
        if (!IsAchievementUnlocked(achievementName))
        {
            Debug.Log($"Achievement Unlocked: {achievementName}");
            if (player.gameObject.name == "Player1")
            {
                StartCoroutine(ShowAchievement(AchievementUIPlayer2));
            }
            else if (player.gameObject.name == "Player2")
            {
                StartCoroutine(ShowAchievement(AchievementUIPlayer1));
            }
        }


        
    }

    private IEnumerator ShowAchievement(GameObject AchievementUIPlayer)
    {
        AchievementUIPlayer.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        AchievementUIPlayer.gameObject.SetActive(false);
    }

}