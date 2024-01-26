using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu2Aled : MonoBehaviour
{
    public AudioSource classement;

    public Text txt_instruction;
    public Text ancienTxt;

    public GameObject GO_particleSystem;

    public Animator anim_Reponse;
    public Animator anim_Classement;

    private int numeroJoueurActuel = 0;

    private int nombreDeManchesJouees = 0;

    public Image logoJoueur;

    private int joueurtourActuel = 1;
    private int joueursAyantVote2 = 0;
    int[] votes = new int[4];

    public Animator anim_Themes;
    public MaskableGraphic txt_Themes;


    public Timer timerRef;
    public GameObject GO_timer;

    private HashSet<ThemeActingData> ThemesUtilise = new HashSet<ThemeActingData>();
    private ThemeActingData themeCourant;

    public ThemeActingDataBase ThemeActingDataRef;
    [SerializeField] public static List<ThemeActingData> themesDatas = new();

    public JoueursDataBase joueursDataBaseRef;
    [SerializeField] public static List<JoueursData> joueursDatas = new();



    [Header("ParentButton")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    [Header("Button")]
    public Button boutonJoueur1;
    public Button boutonJoueur2;
    public Button boutonJoueur3;
    public Button boutonJoueur4;


    private bool TousLesJoueursOntVote2() => joueursAyantVote2 == 4;

    void Start()
    {
        //InitieImage();
        ancienTxt.gameObject.SetActive(false);
        txt_instruction.gameObject.SetActive(true);
        //StartCoroutine(Final());
        themesDatas.AddRange(ThemeActingDataRef.datas);
        joueursDatas.AddRange(joueursDataBaseRef.datas);
        AfficherNouveauThemes();


        boutonJoueur1.onClick.AddListener(() => Vote(1));
        boutonJoueur2.onClick.AddListener(() => Vote(2));
        boutonJoueur3.onClick.AddListener(() => Vote(3));
        boutonJoueur4.onClick.AddListener(() => Vote(4));

        InitieInfo();
    }

    
    void Update()
    {
        
    }

    private void AfficherNouveauThemes()
    {
        List<ThemeActingData> themesNonUtilisees = themesDatas.FindAll(txt => !ThemesUtilise.Contains(txt));
        if(themesNonUtilisees.Count > 0 )
        {
            themeCourant = themesNonUtilisees[Random.Range(0,themesNonUtilisees.Count)];
            InitieThemes(themeCourant);
            StartCoroutine(JouerDelaiAvantChangementQuatreFois());  

        }
        else
        {
            txt_instruction.text = "End of the game";

            StartCoroutine(Final());


        }
    }

    private IEnumerator JouerDelaiAvantChangementQuatreFois()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return StartCoroutine(miniJeu2());
        }

        // Ici, vous pouvez effectuer des actions après avoir joué la coroutine 4 fois
        // par exemple, afficher un message de fin de jeu.
        Debug.Log("Le jeu est terminé !");
    }


    private IEnumerator miniJeu2()
    {
        if (nombreDeManchesJouees != 2)
        {
            yield return new WaitForSeconds(8f);
            //debut
            anim_Themes.SetTrigger("ThemesGoDown");
            int joueur1Index = Random.Range(0, joueursDatas.Count);
            int joueur2Index = Random.Range(0, joueursDatas.Count);
            anim_Reponse.SetTrigger("UpForReponse");

            if (joueursDatas.Count > 0)
            {
                
                txt_instruction.text = "Its " + joueursDatas[joueur1Index].JoueursName + " and " + joueursDatas[joueur2Index].JoueursName + " who will act";
                yield return new WaitForSeconds(8f);
            }
            else
            {
                Debug.LogError("La liste de joueurs est vide !");
            }
            GO_timer.SetActive(true);
            timerRef.startTimer = true;
            timerRef.remainingTime = 30;
            yield return new WaitForSeconds(30f);
            GO_timer.SetActive(false);
            timerRef.startTimer = false;


            //vote
            txt_instruction.text = "It's time to vote !";
            yield return new WaitForSeconds(2f);
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
            txt_instruction.text = "";
            yield return new WaitForSeconds(1f);
            logoJoueur.gameObject.SetActive(true);
            InitieInfo();
            yield return new WaitUntil(() => TousLesJoueursOntVote2());
            txt_instruction.text = "Everyone has voted";
            logoJoueur.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);

            //anim_Reponse.SetTrigger("IntructionDown");
            int joueurGagnant = TrouverJoueurGagnant();
            Debug.Log(joueurGagnant);
            AttribuerPoints(joueurGagnant);

            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);

            anim_Themes.SetTrigger("ThemesGoUp");
            //fin et on recommence
            ThemesUtilise.Add(themeCourant);
            yield return new WaitForSeconds(5f);
            joueursAyantVote2 = 0;            
            joueurtourActuel = 0;
            nombreDeManchesJouees++;
            AfficherNouveauThemes();
        }
           
    }

    private void InitieThemes(ThemeActingData data)
    {
        SetMaskableGraphiqueValue(ref txt_Themes, data.themes);
    }

    private void SetMaskableGraphiqueValue(ref MaskableGraphic mg , object value)
    {
        switch (mg)
        {
            case Text txt: txt.text = value.ToString(); break;
                //case Image img: img.sprite = value as Sprite; break;
        }
    }


    private IEnumerator Final()
    {
        yield return new WaitForSeconds(3f);
        txt_instruction.text = "And the game is over , let's see the results !";
        yield return new WaitForSeconds(5f);
        GO_particleSystem.SetActive(true);
        classement.Play();
        anim_Classement.SetTrigger("Classement");
    }

    private void Vote(int numeroJoueur)
    {
        //MettreAJourAffichageTour();
        
        if (numeroJoueur != joueurtourActuel)
        {
            Debug.Log("Joueur " + joueurtourActuel + " a voté pour le joueur " + numeroJoueur);

            // Mettre à jour le tableau votes
            votes[numeroJoueur - 1]++;
            InitieImage();
            if (joueurtourActuel == 4)
            {                
                joueursAyantVote2 = 4;
                return;
            }

            joueurtourActuel++;
            if (joueurtourActuel == 5)
            {
                joueursAyantVote2++;
                joueurtourActuel = 1;
            }
        }
        else
        {
            Debug.Log("Le joueur " + joueurtourActuel + " ne peut pas voter pour lui-même.");
        }
    }


    private void InitieInfo()
    {
        boutonJoueur1.GetComponentInChildren<Text>().text = joueursDatas[0].JoueursName;
        boutonJoueur2.GetComponentInChildren<Text>().text = joueursDatas[1].JoueursName;
        boutonJoueur3.GetComponentInChildren<Text>().text = joueursDatas[2].JoueursName;
        boutonJoueur4.GetComponentInChildren<Text>().text = joueursDatas[3].JoueursName;
    }


    private int TrouverJoueurGagnant()
    {

        int joueurGagnant = 0;

        for (int i = 1; i < votes.Length; i++)
        {
            if (votes[i] > votes[joueurGagnant])
            {
                joueurGagnant = i;
            }
        }

        return joueurGagnant + 1;
    }

    private void AttribuerPoints(int joueurGagnant)
    {
        Debug.Log("Joueur " + joueurGagnant + " remporte les points!");
        
        txt_instruction.text = joueurGagnant + " won !";

        if (joueurGagnant >= 1 && joueurGagnant <= joueursDatas.Count)
        {
            joueursDatas[joueurGagnant - 1].score += 500;
            //playerScore1.text = joueursDatas[0].score.ToString();
            //playerScore2.text = joueursDatas[1].score.ToString();
            //playerScore3.text = joueursDatas[2].score.ToString();
            //playerScore4.text = joueursDatas[3].score.ToString();
        }

        //anim_Reponse.SetTrigger("IntructionUp");
    }


    private void InitieImage()
    {
        if (joueurtourActuel < joueursDataBaseRef.datas.Count)
        {
            JoueursData joueurActuel = joueursDataBaseRef.datas[joueurtourActuel];
            if (logoJoueur != null && joueurActuel != null)
            {
                logoJoueur.sprite = joueurActuel.logoJoueur;
            }
        }
    }
}

