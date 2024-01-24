using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu2 : MonoBehaviour
{

    
    public bool miniJeu2Active = false;
    private bool goForJeu2 = false;

    public Text txt_instruction;

    public GameObject buttonContinueJeu2;

    public Animator anim_Reponse;

    List<int> excludesNumber = new List<int>();

    public ThemeActingDataBase themesDataBaseRef;
    [SerializeField] public static List<ThemeActingData> themesDatas = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Enum_MiniJeu2());
    }




    private IEnumerator Enum_MiniJeu2()
    {
        if (miniJeu2Active == true)
        {
            yield return new WaitForSeconds(3f);
            anim_Reponse.SetTrigger("UpForReponse");
            txt_instruction.text = "Now it's Time for our second game !";
            yield return new WaitForSeconds(3f);
            txt_instruction.text = "Are you ready ?";

            buttonContinueJeu2.SetActive(true);

            if (goForJeu2 == true)
            {
                txt_instruction.text = " Now let's do a little acting :)";


                foreach (int numberThemes in excludesNumber)
                {
                    txt_instruction.text = themesDataBaseRef.datas[numberThemes].themes;
                    yield return new WaitForSeconds(8f);
                }
            }
        }
    }


    private void NextJeu()
    {
        miniJeu2Active = true;
    }

    private void ContinueJeu2()
    {
        buttonContinueJeu2.SetActive(false);
        goForJeu2 = true;
    }
}
