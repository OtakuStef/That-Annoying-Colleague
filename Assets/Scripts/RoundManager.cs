using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }
    public PlayerDamage player1;
    public PlayerDamage player2;

    public TextMeshProUGUI winner;

    [SerializeField] GameObject GameOverUI;
    bool paused = false;

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
        //Debug.Log("Round Started, round: " + RoundCount.ToString());
    }

    public void EndRound()
    {
        Time.timeScale = 0f;
        paused = true;
        GameOverUI.SetActive(true);
        AudioManager.Instance.StopSFX();
        if (player1 != null && player2 != null)
        {
            //Debug.Log("Round finished.");
            if (player1.playerHealth > player2.playerHealth)
            {
                WinnerOfLastRound = player1;
                winner.text = "Winner is Player 1";
                CheckForSurvivor(player1);
                //Debug.Log("Winner is Player 1.");
            }
            else if (player2.playerHealth > player1.playerHealth)
            {
                WinnerOfLastRound = player2;
                winner.text = "Winner is Player 2";
                CheckForSurvivor(player2);
                //Debug.Log("Winner is Player 2.");
            }
            else
            {
                // tie
                WinnerOfLastRound = null;
            }
        }
        else
        {
            Debug.LogError("Player references not set in RoundManager.");
        }
    }

    public bool healthBelowZero()
    {
        if (player1 != null && player2 != null)
        {
            if (player1.playerHealth <= 0 || player2.playerHealth <= 0)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckForSurvivor(PlayerDamage player)
    {
        float tenPercentHealth = player.playerHealth * 0.1f;
        if (player.playerHealth <= tenPercentHealth)
        {
            AchievementsManager.Instance.AwardAchievement(player, "Survivor");
        }
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Office Stage 1");
    }
    void Update()
    {
        if (Input.GetKeyDown("m") && (paused == true))
        {
            Home();
        }
        if (Input.GetKeyDown("n") && (paused == true))
        {
            Restart();
        }


    }

}