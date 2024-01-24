using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu1 : MonoBehaviour
{
    public GameObject GO_Image;
    public GameObject buttonContinueJeu2;
    public MaskableGraphic image;
    public Text txt_instruction;
    private int joueurtourActuel = 1;
    private int joueursAyantVote = 0;
    private int joueursAyantRepondu = 0;
    private int numeroJoueurActuel = 0;
    //private float HoldTimer = 9;


    private Text timer;
    public InputField iF_Reponses;

    public Image DirectionUI;
    private bool isShaderAnimationPlaying = false;
    private bool playShaderAnim = false;

    public bool miniJeu1Active = true;
    public bool miniJeu2Active = false;
    private bool goForJeu2 = false;

    int[] votes = new int[4];
    List<int> excludesNumber = new List<int>();

    public GameObject classementFinal;

    public Animator anim_Classement;
    public Animator anim_Reponse;
    public Animator anim_Image;

    [Header("Data")]
    public ImageMemeDataBase ImageMemeDataBaseRef;
    [SerializeField] public static List<ImageMemeData> imageDatas = new();

    public JoueursDataBase joueursDataBaseRef;
    [SerializeField] public static List<JoueursData> joueursDatas = new();

    private HashSet<ImageMemeData> imagesUtilisees = new HashSet<ImageMemeData>();
    private ImageMemeData imageCourante;

   

    [Header("Button")]
    public Button boutonJoueur1;
    public Button boutonJoueur2;
    public Button boutonJoueur3;
    public Button boutonJoueur4;

    public Button[] boutonJoueurs;
    

    [Header("ScorePlayers")]
    public Text playerScore1;
    public Text playerScore2;        
    public Text playerScore3;
    public Text playerScore4;

    [Header("ParentButton")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    [Header("PlayersName")]
    public Text playerName1;
    public Text playerName2;
    public Text playerName3;
    public Text playerName4;

    private bool TousLesJoueursOntVote()
    {
        return joueursAyantVote == 4;
    }
    private bool TousLesJoueursOntRepondu()
    {
        return joueursAyantRepondu == 4;
    }


    private void Start()
    {
        imageDatas.AddRange(ImageMemeDataBaseRef.datas);
        joueursDatas.AddRange(joueursDataBaseRef.datas);
        AfficherNouvelleImage();



        /*
        boutonJoueur1.onClick.AddListener(() => Vote(1));
        boutonJoueur2.onClick.AddListener(() => Vote(2));
        boutonJoueur3.onClick.AddListener(() => Vote(3));
        boutonJoueur4.onClick.AddListener(() => Vote(4));
        */

        InitieInfo();
        DirectionUI = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        //HoldTimer = -Time.fixedDeltaTime;
        //DirectionUI.material.SetFloat("Anim", 1 - HoldTimer / 3);


        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    // Activer l'animation ShaderGraph
        //    isShaderAnimationPlaying = true;
        //    DirectionUI.material.SetFloat("Anim", 0f); // Réinitialiser le temps d'animation
        //}

        //if (isShaderAnimationPlaying)
        //{
        //    // Mettre à jour le temps d'animation dans le shader
        //    float animationTime = DirectionUI.material.GetFloat("Anim");
        //    animationTime += Time.deltaTime;
        //    DirectionUI.material.SetFloat("Anim", animationTime);

        //    // Vérifier si l'animation est terminée
        //    if (animationTime >= 1.0f)
        //    {
        //        // L'animation est terminée, faire quelque chose
        //        isShaderAnimationPlaying = false;
        //        //DoSomethingAtEndOfAnimation();
        //    }
        //}
    }

    private void AfficherNouvelleImage()
    {
        List<ImageMemeData> imagesNonUtilisees = imageDatas.FindAll(img => !imagesUtilisees.Contains(img));
        if (imagesNonUtilisees.Count > 0)
        {
            imageCourante = imagesNonUtilisees[Random.Range(0, imagesNonUtilisees.Count)];
            InitieImage(imageCourante);


            StartCoroutine(GererDelaiAvantChangement());
        }
        else
        {
            //remonter image
            anim_Image.SetTrigger("Up");
            //afficher texte pour dire : voici le score, 
            txt_instruction.text = "And now evreybody's scores !";
            //afficher tous les joueurs en ligne avec leurs "logo" et leurs scores en dessous d'eux
            anim_Classement.SetTrigger("ClassementActive");
            txt_instruction.text = "Fin du jeu";

            miniJeu2Active = true;
            StartCoroutine(MiniJeu2());
        }
    }

    private IEnumerator GererDelaiAvantChangement()
    {
        //MettreAJourAffichageTour();
        if(miniJeu1Active == true)
        {
            txt_instruction.text = "Find a caption to this meme !";
            anim_Image.SetTrigger("Down");
            //playShaderAnim = true;


            yield return new WaitForSeconds(5f);

            //playShaderAnim = false;
            txt_instruction.text = "Time's up, give your answer !";
            iF_Reponses.gameObject.SetActive(true);
            yield return new WaitUntil(() => TousLesJoueursOntRepondu());
            yield return new WaitForSeconds(2f);
            iF_Reponses.gameObject.SetActive(false);
            txt_instruction.text = "Let's see everybody's answers !";
            anim_Reponse.SetTrigger("UpForReponse");

            foreach (int numberAnswer in excludesNumber)
            {
                txt_instruction.text = joueursDataBaseRef.datas[numberAnswer].answer;
                yield return new WaitForSeconds(8f);
            }





            txt_instruction.text = "It's time to vote !";
            yield return new WaitForSeconds(1f);
            anim_Image.SetTrigger("UpImageReponse");


            anim_Reponse.SetTrigger("DownReponse");
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
            txt_instruction.text = "";
            yield return new WaitForSeconds(1f);
            MettreAJourAffichageTour();
            yield return new WaitUntil(() => TousLesJoueursOntVote());

            int joueurGagnant = TrouverJoueurGagnant();
            Debug.Log(joueurGagnant);
            AttribuerPoints(joueurGagnant);
            txt_instruction.text = "C'est " + joueurGagnant + " qui a gagner cette manche !";


            anim_Image.SetTrigger("Up");
            imagesUtilisees.Add(imageCourante);
            yield return new WaitForSeconds(2f);
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);

            yield return new WaitForSeconds(5f);
            AfficherNouvelleImage();
            joueursAyantVote = 0;
        }        
    }

    

    private void InitieInfo()
    {
        foreach(Button boutonJoueur in boutonJoueurs)
        {
            int randomNumberPos = randomNumberExclude(0, 4, excludesNumber);
            excludesNumber.Add(randomNumberPos);
            boutonJoueur.GetComponentInChildren<Text>().text = joueursDataBaseRef.datas[randomNumberPos].answer;
            boutonJoueur.onClick.RemoveAllListeners();
            boutonJoueur.onClick.AddListener(() => Vote(randomNumberPos));
        }
        //excludesNumber.Clear();
        /*
        boutonJoueur1.GetComponentInChildren<Text>().text = joueursDatas[0].answer;
        boutonJoueur2.GetComponentInChildren<Text>().text = joueursDatas[1].answer;
        boutonJoueur3.GetComponentInChildren<Text>().text = joueursDatas[2].answer;
        boutonJoueur4.GetComponentInChildren<Text>().text = joueursDatas[3].answer;
        */
        playerScore1.text = joueursDatas[0].score.ToString();
        playerScore2.text = joueursDatas[1].score.ToString();
        playerScore3.text = joueursDatas[2].score.ToString();
        playerScore4.text = joueursDatas[3].score.ToString();


        playerName1.text = joueursDatas[0].JoueursName.ToString();
        playerName2.text = joueursDatas[1].JoueursName.ToString();
        playerName3.text = joueursDatas[2].JoueursName.ToString();
        playerName4.text = joueursDatas[3].JoueursName.ToString();

    }
    private void InitieImage(ImageMemeData data)
    {
        SetMaskableGraphicValue(ref image, data.imageMeme);
    }

    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            //case Text txt: txt.text = value.ToString(); break;
            case Image img: img.sprite = value as Sprite; break;
        }
    }

    private void MettreAJourAffichageTour()
    {
        //MarchePas
        Debug.Log("C'est au tour de" + joueursDataBaseRef.datas[joueurtourActuel - 1].JoueursName + " de voter.");
        txt_instruction.text = "C'est au tour de " + joueursDataBaseRef.datas[joueurtourActuel - 1].JoueursName + " de voter.";
    }


    private void Vote(int numeroJoueur)
    {
        //MettreAJourAffichageTour();
        
        if (numeroJoueur != joueurtourActuel)
        {
            Debug.Log("Joueur " + joueurtourActuel + " a voté pour le joueur " + numeroJoueur);

            // Mettre à jour le tableau votes
            votes[numeroJoueur - 1]++;

            joueurtourActuel++;
            if (joueurtourActuel > 4)
            {
                txt_instruction.text = "Tous les joueurs ont voté";
                joueursAyantVote = 4;
            }

            if (joueurtourActuel == 1)
            {
                joueursAyantVote++;
            }
        }
        else
        {
            Debug.Log("Le joueur " + joueurtourActuel + " ne peut pas voter pour lui-même.");
        }
    }

    public void SoumettreReponse()
    {
        string reponseJoueur = iF_Reponses.text;
        EnregistrerReponses(reponseJoueur);

        numeroJoueurActuel++;
        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            joueursAyantRepondu++;
            MettreAJourInstruction();
            iF_Reponses.text = "";
        }
        else
        {
            txt_instruction.text = "All the players gived their answers !";
            joueursAyantRepondu = 4;
        }
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

        if (joueurGagnant >= 1 && joueurGagnant <= joueursDatas.Count)
        {
            joueursDatas[joueurGagnant - 1].score += 500;
            playerScore1.text = joueursDatas[0].score.ToString();
            playerScore2.text = joueursDatas[1].score.ToString();
            playerScore3.text = joueursDatas[2].score.ToString();
            playerScore4.text = joueursDatas[3].score.ToString();
        }
    }



    private void MettreAJourInstruction()
    {
        iF_Reponses.text = "Joueur " + (numeroJoueurActuel + 1) + " enter your name";
    }


    private void EnregistrerReponses(string answer)
    {
        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            joueursDataBaseRef.datas[numeroJoueurActuel].answer = answer;
            Debug.Log(numeroJoueurActuel + " a repondu : " + answer);
        }
    }

    int randomNumberExclude(int min, int max, List<int> Exclude)
    {
        int number = Random.Range(min, max);
        while (Exclude.Contains(number))
        {
            number = Random.Range(min, max);
        }
        return number;
    }


    private IEnumerator MiniJeu2()
    {
        yield return new WaitForSeconds(3f);

        if (miniJeu2Active == true)
        {
            anim_Reponse.SetTrigger("UpForReponse");
            txt_instruction.text = "Now it's Time for our second game !";
            yield return new WaitForSeconds(3f);
            txt_instruction.text = "Are you ready ?";

            buttonContinueJeu2.SetActive(true);

            if(goForJeu2 == true)
            {
                txt_instruction.text = " Now let's do a little acting :)";
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
