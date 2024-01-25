using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator anim_Transition;
    public static bool backToMainMenu = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMainMenu()
    {
        backToMainMenu = false;
        anim_Transition.SetTrigger("BackToMainMenu");
        backToMainMenu = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
}
