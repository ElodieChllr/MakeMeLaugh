using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject pnl_NbJoueurs;
    public GameObject pnl_MainMenu;
    public GameObject pnl_JoueursName;

    [Header("Data")]
    public JoueursDataBase joueursDataBaseRef;
    //public JoueursData JoueursData;
    [SerializeField] public static List<JoueursData> joueursDatas = new ();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoToNbJoueurs()
    {
        pnl_NbJoueurs.SetActive(true);
        pnl_MainMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        pnl_MainMenu.SetActive(true);
        pnl_NbJoueurs.SetActive(false);
    }

    public void PlayGame()
    {
        //StartCoroutine(wait());
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }


    public void GoToJoueursName()
    {
        pnl_NbJoueurs.SetActive(false);
        pnl_JoueursName.SetActive(true);
    }
    public void GoToNbJoueursViaName()
    {
        pnl_NbJoueurs.SetActive(true);
        pnl_JoueursName.SetActive(false);
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }
}
