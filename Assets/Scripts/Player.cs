using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int consecutiveWins { get; private set; }

    public void ResetConsecutiveWins()
    {
        consecutiveWins = 0;
    }

    public void IncrementConsecutiveWins()
    {
        consecutiveWins++;
    }

    public int Health { get; private set; }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            RoundManager.Instance.EndRound();
        }
    }

    private List<string> achievements = new List<string>();

    public bool HasAchievement(string achievement)
    {
        return achievements.Contains(achievement);
    }

    public void AddAchievement(string achievement)
    {
        if (!HasAchievement(achievement))
        {
            achievements.Add(achievement);
            
        }
    }

    // part of the method that processes a hit on a player
    /* 
    public void OnObjectHitPlayer(Player attackedPlayer, Player attackingPlayer, GameObject hitObject)
    {

        if (attackedPlayer.Health < attackedPlayer.Health)
        {
            AchievementsManager.Instance.AwardAchievement(attackingPlayer, "First Blood");
        }
    }
    */
}
