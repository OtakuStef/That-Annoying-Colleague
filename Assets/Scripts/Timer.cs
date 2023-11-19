using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float Timeleft;
    // Start is called before the first frame update

    public void setTimeLeft(float time){
        Timeleft = 60*time;
    }

    public void increaseTimeLeft(float time){
        Timeleft += time;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Timeleft>0){
            Timeleft -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timeleft/60);
            int seconds = Mathf.FloorToInt(Timeleft%60);
            timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        }
        else{
            timerText.text = string.Format("{0:00}:{1:00}",0,0);
        }       
    }
}
