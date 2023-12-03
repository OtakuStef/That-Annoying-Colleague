using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }
    public PlayerDamage player1;
    public PlayerDamage player2;

    public int RoundCount { get; private set; } = 0;
    public PlayerDamage WinnerOfLastRound { get; private set; }

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
        Debug.Log("Round Started, round: " + RoundCount.ToString());
        // round reset
        // ResetPlayerPositions(); ???
        // ResetPlayerHealth(); ???
    }

    public void EndRound()
    {
        // check
        if (player1 != null && player2 != null)
        {
            Debug.Log("Round finished.");
            if (player1.playerHealth > player2.playerHealth)
            {
                WinnerOfLastRound = player1;
                //player1.IncrementConsecutiveWins();
                //player2.ResetConsecutiveWins();
                //CheckForAchievement(player1);
                CheckForSurvivor(player1);
                Debug.Log("Winner if Player 1.");
            }
            else if (player2.playerHealth > player1.playerHealth)
            {
                WinnerOfLastRound = player2;
                //player2.IncrementConsecutiveWins();
                //player1.ResetConsecutiveWins();
                //CheckForAchievement(player2);
                CheckForSurvivor(player2);
                Debug.Log("Winner if Player 2.");
            }
            else
            {
                // tie
                WinnerOfLastRound = null;
                //player1.ResetConsecutiveWins();
                //player2.ResetConsecutiveWins();
            }

            // ui?
        }
        else
        {
            Debug.LogError("Player references not set in RoundManager.");
        }
    }

    /*
    private void CheckForAchievement(Player player)
    {
        if (player.consecutiveWins == 5)
        {
            AchievementsManager.Instance.AwardAchievement(player, "Office Warrior");
        }
    }
    */
    private void CheckForSurvivor(PlayerDamage player)
    {
        float tenPercentHealth = player.playerHealth * 0.1f;
        if (player.playerHealth <= tenPercentHealth)
        {
            AchievementsManager.Instance.AwardAchievement(player, "Survivor");
        }
    }

}