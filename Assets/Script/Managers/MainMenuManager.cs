using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject pnl_NbJoueurs;
    public GameObject pnl_MainMenu;

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
        StartCoroutine(wait());
        UnityEngine.SceneManagement.SceneManager.LoadScene(idScene);

    }

    //public void _2Joueurs()
    //{
    //    StartCoroutine(wait());
    //    joueursDatas[1].Play = true;
    //}

    //public void _3Joueurs()
    //{
    //    joueursDatas[2].Play = true;
    //}

    //public void _4Joueurs()
    //{
    //    joueursDatas[3].Play = true;
    //}

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }
}
