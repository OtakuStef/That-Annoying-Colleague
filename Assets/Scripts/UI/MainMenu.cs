using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Playgame(){
        SceneManager.LoadScene("Office Stage 1");
    }

    public void Quitgame(){
        Debug.Log("QUIT!");
        Application.Quit();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
