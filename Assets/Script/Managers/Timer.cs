using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text txt_timer;
    [SerializeField] float remainingTime;

    public bool NoTime = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 30)
        {
            //remainingTime = 0;
            txt_timer.color = Color.red;
        }
        else if( remainingTime < 0)
        {
            NoTime = true;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        txt_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
