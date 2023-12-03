using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pauseMenu;

    bool paused = false;
    
    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;   
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    
    public void Home(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Office Stage 1");
    }
    

    void Update()
    {
        if (Input.GetKeyDown("escape")&&(paused==false))
        {
            Pause();
        }else{
            if(Input.GetKeyDown("escape")&&(paused==true)){
            Resume();
            }
        }
        if(Input.GetKeyDown("m")&&(paused==true)){
            Home();
        }
        if(Input.GetKeyDown("n")&&(paused==true)){
            Restart();
        }
        

    }

}
