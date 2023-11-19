using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }
    public Player player1;
    public Player player2;

    public int RoundCount { get; private set; } = 0;
    public Player WinnerOfLastRound { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNewRound()
    {
        
        RoundCount++;
        // round reset
        // ResetPlayerPositions(); ???
        // ResetPlayerHealth(); ???
    }

    public void EndRound()
    {
        // check
        if (player1 != null && player2 != null)
        {
            if (player1.Health > player2.Health)
            {
                WinnerOfLastRound = player1;
                player1.IncrementConsecutiveWins();
                player2.ResetConsecutiveWins();
                CheckForAchievement(player1);
                CheckForSurvivor(player1);
            }
            else if (player2.Health > player1.Health)
            {
                WinnerOfLastRound = player2;
                player2.IncrementConsecutiveWins();
                player1.ResetConsecutiveWins();
                CheckForAchievement(player2);
                CheckForSurvivor(player2);
            }
            else
            {
                // tie
                WinnerOfLastRound = null;
                player1.ResetConsecutiveWins();
                player2.ResetConsecutiveWins();
            }

            // ui?
        }
        else
        {
            Debug.LogError("Player references not set in RoundManager.");
        }
    }

    private void CheckForAchievement(Player player)
    {
        if (player.consecutiveWins == 5)
        {
            AchievementsManager.Instance.AwardAchievement(player, "Office Warrior");
        }
    }

    private void CheckForSurvivor(Player player)
    {
        float tenPercentHealth = player.Health * 0.1f;
        if (player.Health <= tenPercentHealth)
        {
            AchievementsManager.Instance.AwardAchievement(player, "Survivor");
        }
    }

}