using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text txt_timer;
    public float remainingTime;

    public AudioSource timerSound;
    public bool startTimer = false;

    
    public bool NoTime = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (startTimer == true)
        {
            timerSound.Play();
            remainingTime -= Time.deltaTime;

            if (remainingTime < 5)
            {
                remainingTime = 0;
                NoTime = true;
                timerSound.Stop();

                // Ajoutez ici toute logique supplémentaire lorsque le temps atteint 0
            }            
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        txt_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
