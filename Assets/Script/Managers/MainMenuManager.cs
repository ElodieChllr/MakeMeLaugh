using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    public GameObject pnl_MainMenu;
    public GameObject pnl_JoueursName;

    public GameObject backgroundSound;

    public Animator backToMainMenu;

    public GameObject logoJeux;

    [Header("Data")]
    public JoueursDataBase joueursDataBaseRef;
    //public JoueursData JoueursData;
    [SerializeField] public static List<JoueursData> joueursDatas = new ();

    private void Awake()
    {
        
    }
    void Start()
    {
        joueursDatas.AddRange(joueursDataBaseRef.datas);

        if (GameManager.backToMainMenu == true)
        {
            backToMainMenu.SetTrigger("OnMainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        
        pnl_MainMenu.SetActive(true);
        logoJeux.SetActive(true);
        pnl_JoueursName.SetActive(false);
    }
    public void GoToJoueursName()
    {
        pnl_MainMenu.SetActive(false);
        logoJeux.SetActive(false);
        pnl_JoueursName.SetActive(true);
    }

    public void PlayGame()
    {
        //StartCoroutine(wait());
        joueursDatas[0].score = 0;
        joueursDatas[1].score = 0;
        joueursDatas[2].score = 0;
        joueursDatas[3].score = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


    public void ExitGame()
    {
        Application.Quit();
    }





    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }
}
