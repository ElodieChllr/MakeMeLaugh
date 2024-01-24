using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu1 : MonoBehaviour
{
    public GameObject GO_Image;
    public MaskableGraphic image;
    public Text txt_instruction;
    private int joueurtourActuel = 1;
    private int joueursAyantVote = 0;
    private int joueursAyantRepondu = 0;
    private int numeroJoueurActuel = 0;

    public InputField iF_Reponses;

    int[] votes = new int[4];

    public GameObject boutonJoueurPrefab;

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

    [Header("ScorePlayer")]
    public Text playerScore1;
    public Text playerScore2;        
    public Text playerScore3;
    public Text playerScore4;

    [Header("ParentButton")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

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


        boutonJoueur1.onClick.AddListener(() => Vote(1));
        boutonJoueur2.onClick.AddListener(() => Vote(2));
        boutonJoueur3.onClick.AddListener(() => Vote(3));
        boutonJoueur4.onClick.AddListener(() => Vote(4));


        InitieButton();

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
            txt_instruction.text = "Fin du jeu";
        }
    }

    private IEnumerator GererDelaiAvantChangement()
    {
        MettreAJourAffichageTour();


        txt_instruction.text = "Find a caption to this meme !";
        anim_Image.SetTrigger("Down");


        yield return new WaitForSeconds(5f);


        txt_instruction.text = "Time's up, give your answer !";
        iF_Reponses.gameObject.SetActive(true);
        yield return new WaitUntil(() => TousLesJoueursOntRepondu());
        yield return new WaitForSeconds(2f);
        iF_Reponses.gameObject.SetActive(false);
        txt_instruction.text = "Let's see everybody's answers !";
        anim_Reponse.SetTrigger("UpForReponse");
        //txt_instruction.text = joueursDatas[0].score.ToString();

        //yield return new WaitForSeconds(5f);
        


        txt_instruction.text = "It's time to vote !";
        yield return new WaitForSeconds(1f);
        
        

        //button1.SetActive(true);
        //button2.SetActive(true);
        //button3.SetActive(true);
        //button4.SetActive(true);
        yield return new WaitUntil(() => TousLesJoueursOntVote());

        int joueurGagnant = TrouverJoueurGagnant();
        Debug.Log(joueurGagnant);
        AttribuerPoints(joueurGagnant);
        txt_instruction.text = "C'est " + joueurGagnant + " qui a gagner cette manche !";


        anim_Image.SetTrigger("Up");
        anim_Reponse.SetTrigger("DownReponse");
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

    private void InitieImage(ImageMemeData data)
    {
        SetMaskableGraphicValue(ref image, data.imageMeme);
    }

    private void InitieButton()
    {
        boutonJoueur1.GetComponentInChildren<Text>().text = joueursDatas[0].JoueursName;
        boutonJoueur2.GetComponentInChildren<Text>().text = joueursDatas[1].JoueursName;
        boutonJoueur3.GetComponentInChildren<Text>().text = joueursDatas[2].JoueursName;
        boutonJoueur4.GetComponentInChildren<Text>().text = joueursDatas[3].JoueursName;

        playerScore1.text = joueursDatas[0].score.ToString();
        playerScore2.text = joueursDatas[1].score.ToString();
        playerScore3.text = joueursDatas[2].score.ToString();
        playerScore4.text = joueursDatas[3].score.ToString();
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
        //txt_instruction.text = "C'est au tour de " + JoueursDataBaseRef.datas[joueurtourActuel - 1].JoueursName + " de voter.";
    }

    private void ShowAnswer()
    {
        //Random.Range(0,joueursDatas.Count)]

    }

    private void Vote(int numeroJoueur)
    {
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
            iF_Reponses.text = ""; // Effacez le champ de saisie pour le joueur suivant
        }
        else
        {
            txt_instruction.text = "All the players gived their answers !";
            joueursAyantRepondu = 4;
            // Vous pouvez ajouter ici une logique pour commencer le jeu
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
        //txt_instruction.text = "Joueur " + joueurGagnant + " remporte les points!";
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
        }
    }


}
