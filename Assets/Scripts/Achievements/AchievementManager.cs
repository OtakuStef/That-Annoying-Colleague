using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static AchievementsManager Instance { get; private set; }
    private Dictionary<string, bool> achievementsUnlocked = new Dictionary<string, bool>();


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

    public void AwardAchievement(PlayerDamage player, string achievementName)
    {
        if (!IsAchievementUnlocked(achievementName))
        {
            achievementsUnlocked[achievementName] = true;
            // player.AddAchievement(achievementName);
            Debug.Log($"Achievement Unlocked: {achievementName}");
            // ui?
        }
    }

}