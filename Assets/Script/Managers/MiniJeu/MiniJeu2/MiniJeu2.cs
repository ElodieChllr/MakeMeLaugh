using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu2 : MonoBehaviour
{

    
    public bool miniJeu2Active = false;
    //private bool goForJeu2 = false;

    public Text txt_instruction;
    public MaskableGraphic txt_enoncerTheme;
    public GameObject miniJeu1Ref;

    public GameObject buttonContinueJeu2;

    [Header("Animator")]
    public Animator anim_Reponse;
    public Animator anim_themes;

    

    private HashSet<ThemeActingData> ThemesUtilise = new HashSet<ThemeActingData>();
    private ThemeActingData themeCourant;

    public ThemeActingDataBase themesDataBaseRef;
    [SerializeField] public static List<ThemeActingData> themesDatas = new();
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Enum_MiniJeuDebut2());
        themesDatas.AddRange(themesDataBaseRef.datas);
    }

    // Update is called once per frame
    void Update()
    {
        //themesDatas.AddRange(themesDataBaseRef.datas);
        StartCoroutine(Enum_MiniJeuDebut2());

        if(miniJeu2Active == true)
        {
            miniJeu1Ref.SetActive(false);
        }


        
    }




    private IEnumerator Enum_MiniJeuDebut2()
    {
        if (miniJeu2Active == true)
        {
            yield return new WaitForSeconds(3f);
            anim_Reponse.SetTrigger("UpForReponse");
            txt_instruction.text = "Now it's Time for our second game !";

            //buttonContinueJeu2.SetActive(true);

            AfficherTheme();
            //if (goForJeu2 == true)
            //{
            //    txt_instruction.text = " Now let's do a little acting :)";
            //    miniJeu2Active = false;
                
            //    //StartCoroutine(Enum_MiniJeu2());
            //}

        }
    }
    private IEnumerator Enum_MiniJeu2()
    {
        yield return new WaitForSeconds(3f);

        //yield return new WaitForSeconds(2f);
        //txt_instruction.text = "";
        anim_themes.SetTrigger("ThemesGoDown");
        yield return new WaitForSeconds(5f);
        anim_themes.SetTrigger("ThemesGoUp");
        ThemesUtilise.Add(themeCourant);
        yield return new WaitForSeconds(2f);
        AfficherTheme();
        
    }



    private void NextJeu()
    {
        miniJeu2Active = true;
    }

    public void ContinueJeu2()
    {
        //goForJeu2 = true;
        buttonContinueJeu2.SetActive(false);
    }

    private void AfficherTheme()
    {
        List<ThemeActingData> themesNonUtilisees = themesDatas.FindAll(txt => !ThemesUtilise.Contains(txt));
        if(themesNonUtilisees.Count > 0)
        {
            themeCourant = themesNonUtilisees[Random.Range(0, themesNonUtilisees.Count)];
            InitieTheme(themeCourant);

            StartCoroutine(Enum_MiniJeu2());
        }
        else
        {
            txt_instruction.text = "Fin du jeu";
        }
    }

    private void InitieTheme(ThemeActingData data)
    {
        SetMaskableGraphicValue(ref txt_enoncerTheme, data.themes);
    }
    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            case Text txt: txt.text = value.ToString(); break;
            //case Image img: img.sprite = value as Sprite; break;
        }
    }
}
