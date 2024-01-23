using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniJeu1 : MonoBehaviour
{
    //public GameObject GO_Image;
    //public MaskableGraphic image;
    //public Text txt_instruction;
    //private int joueurtourActuel = 1;
    //private int joueursAyantVote = 0;

    //public GameObject boutonJoueurPrefab;

    //public Animator animator;

    //[Header("Data")]
    //public ImageMemeDataBase ImageMemeDataBaseRef;
    //[SerializeField] public static List<ImageMemeData> imageDatas = new();

    //public JoueursDataBase JoueursDataBaseRef;
    //[SerializeField] public static List<JoueursData> joueursDatas = new();

    //private HashSet<ImageMemeData> imagesUtilisees = new HashSet<ImageMemeData>();
    //private ImageMemeData imageCourante;


    //[Header("Button")]
    //public Button boutonJoueur1;
    //public Button boutonJoueur2;
    //public Button boutonJoueur3;
    //public Button boutonJoueur4;

    //private bool TousLesJoueursOntVote()
    //{
    //    return joueursAyantVote == 4;
    //}

    //private void Start()
    //{
    //    imageDatas.AddRange(ImageMemeDataBaseRef.datas);
    //    AfficherNouvelleImage();

    //    boutonJoueur1.onClick.AddListener(() => Vote(1));
    //    boutonJoueur2.onClick.AddListener(() => Vote(2));
    //    boutonJoueur3.onClick.AddListener(() => Vote(3));
    //    boutonJoueur4.onClick.AddListener(() => Vote(4));
    //}

    //private void AfficherNouvelleImage()
    //{
    //    List<ImageMemeData> imagesNonUtilisees = imageDatas.FindAll(img => !imagesUtilisees.Contains(img));
    //    if (imagesNonUtilisees.Count > 0)
    //    {
    //        imageCourante = imagesNonUtilisees[Random.Range(0, imagesNonUtilisees.Count)];
    //        InitieImage(imageCourante);


    //        StartCoroutine(GererDelaiAvantChangement());
    //    }
    //    else
    //    {

    //        txt_instruction.text = "Fin du jeu";
    //    }
    //}

    //private IEnumerator GererDelaiAvantChangement()
    //{
    //    MettreAJourAffichageTour();


    //    txt_instruction.text = "Find a caption to this meme !";
    //    animator.SetTrigger("Down");


    //    yield return new WaitForSeconds(5f);


    //    txt_instruction.text = "Time's up, give your answer to everybody !";


    //    yield return new WaitForSeconds(5f);
    //    animator.SetTrigger("Up");

    //    txt_instruction.text = "It's time to vote !";



    //    boutonJoueur1.gameObject.SetActive(true);
    //    boutonJoueur2.gameObject.SetActive(true);
    //    boutonJoueur3.gameObject.SetActive(true);
    //    boutonJoueur4.gameObject.SetActive(true);
    //    yield return new WaitUntil(() => TousLesJoueursOntVote());

    //    int joueurGagnant = TrouverJoueurGagnant();
    //    AttribuerPoints(joueurGagnant);


    //    imagesUtilisees.Add(imageCourante);


    //    yield return new WaitForSeconds(5f);
    //    AfficherNouvelleImage();
    //    joueursAyantVote = 0;
    //}

    //private void InitieImage(ImageMemeData data)
    //{
    //    SetMaskableGraphicValue(ref image, data.imageMeme);
    //}

    //private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    //{
    //    switch (mg)
    //    {
    //        //case Text txt: txt.text = value.ToString(); break;
    //        case Image img: img.sprite = value as Sprite; break;
    //    }
    //}

    //private void MettreAJourAffichageTour()
    //{
    //    //MarchePas
    //    txt_instruction.text = "C'est au tour de " + JoueursDataBaseRef.datas[joueurtourActuel - 1].JoueursName + " de voter.";
    //}

    //private void Vote(int numeroJoueur)
    //{
    //    if (numeroJoueur != joueurtourActuel)
    //    {
    //        Debug.Log("Joueur " + joueurtourActuel + " a voté pour le joueur " + numeroJoueur);

    //        joueurtourActuel++;
    //        if (joueurtourActuel > 4)
    //        {
    //            joueurtourActuel = 1;
    //        }

    //        if (joueurtourActuel == 1)
    //        {


    //            joueursAyantVote++;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Le joueur " + joueurtourActuel + " ne peut pas voter pour lui-même.");
    //    }

    //}


    ////private void MettreAJourNomsBoutons()
    ////{
    ////    boutonJoueur1.GetComponentInChildren<Text>().text = joueursDatas[0].JoueursName;
    ////    boutonJoueur2.GetComponentInChildren<Text>().text = joueursDatas[1].JoueursName;
    ////    boutonJoueur3.GetComponentInChildren<Text>().text = joueursDatas[2].JoueursName;
    ////    boutonJoueur4.GetComponentInChildren<Text>().text = joueursDatas[3].JoueursName;
    ////}

    //private int TrouverJoueurGagnant()
    //{
    //    int[] votes = new int[4];
    //    int joueurGagnant = 0;

    //    for (int i = 1; i < votes.Length; i++)
    //    {
    //        if (votes[i] > votes[joueurGagnant])
    //        {
    //            joueurGagnant = i;
    //        }
    //    }

    //    return joueurGagnant + 1;
    //}

    //private void AttribuerPoints(int joueurGagnant)
    //{
    //    Debug.Log("Joueur " + joueurGagnant + " remporte les points!");

    //    if (joueurGagnant >= 1 && joueurGagnant <= joueursDatas.Count)
    //    {
    //        JoueursDataBaseRef.datas[joueurGagnant].score += 500;
    //    }
    //}

    public GameObject GO_Image;
    public MaskableGraphic image;
    public Text txt_instruction;
    private int tourActuel = 1;
    private int joueursAyantVote = 0;

    public GameObject boutonJoueurPrefab;

    public Animator animator;

    [Header("Data")]
    public ImageMemeDataBase ImageMemeDataBaseRef;
    [SerializeField] public static List<ImageMemeData> imageDatas = new();

    public JoueursDataBase JoueursDataBaseRef;
    [SerializeField] public static List<JoueursData> joueursDatas = new();

    private HashSet<ImageMemeData> imagesUtilisees = new HashSet<ImageMemeData>();
    private ImageMemeData imageCourante;

    private bool TousLesJoueursOntVote()
    {
        return joueursAyantVote == 4;
    }

    private void Start()
    {
        imageDatas.AddRange(ImageMemeDataBaseRef.datas);
        AfficherNouvelleImage();
    }

    private void AfficherNouvelleImage()
    {
        List<ImageMemeData> imagesNonUtilisees = imageDatas.FindAll(img => !imagesUtilisees.Contains(img));
        if (imagesNonUtilisees.Count > 0)
        {
            imageCourante = imagesNonUtilisees[Random.Range(0, imagesNonUtilisees.Count)];
            InitieImage(imageCourante);

            // Lancez la coroutine pour gérer le délai
            StartCoroutine(GererDelaiAvantChangement());
        }
        else
        {
            // Aucune image restante, le jeu est terminé
            txt_instruction.text = "Fin du jeu";
        }
    }

    private IEnumerator GererDelaiAvantChangement()
    {
        MettreAJourAffichageTour();

        // Affichez l'instruction initiale
        txt_instruction.text = "Find a caption to this meme !";
        animator.SetTrigger("Down");

        // Attendre 5 secondes
        yield return new WaitForSeconds(5f);

        // Changer l'instruction
        txt_instruction.text = "Time's up, give your answer to everybody !";

        // Attendre encore 5 secondes
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("Up");

        txt_instruction.text = "It's time to vote !";

        // Créer et afficher les boutons de vote
        CreerBoutonsDeVote();

        yield return new WaitUntil(() => TousLesJoueursOntVote());

        // Ajoutez l'image actuelle à l'ensemble des images utilisées
        imagesUtilisees.Add(imageCourante);

        // Afficher une nouvelle image
        yield return new WaitForSeconds(5f);
        AfficherNouvelleImage();
        joueursAyantVote = 0;
    }

    private void CreerBoutonsDeVote()
    {
        for (int i = 0; i < joueursDatas.Count; i++)
        {
            // Créez un bouton pour chaque joueur
            GameObject bouton = Instantiate(boutonJoueurPrefab, transform);

            // Assurez-vous que le texte du bouton est correct
            bouton.GetComponentInChildren<Text>().text = joueursDatas[i].JoueursName;

            // Ajoutez une fonction de vote au bouton (assurez-vous que cette fonction prend un argument pour le numéro du joueur)
            int numeroJoueur = i + 1;
            bouton.GetComponent<Button>().onClick.AddListener(() => Vote(numeroJoueur));
        }
    }

    private void InitieImage(ImageMemeData data)
    {
        SetMaskableGraphicValue(ref image, data.imageMeme);
    }

    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            case Text txt: txt.text = value.ToString(); break;
            case Image img: img.sprite = value as Sprite; break;
        }
    }

    private void MettreAJourAffichageTour()
    {
        txt_instruction.text = "C'est au tour de " + JoueursDataBaseRef.datas[tourActuel - 1].JoueursName + " de voter.";
    }

    private void Vote(int numeroJoueur)
    {
        if (numeroJoueur != tourActuel)
        {
            Debug.Log("Joueur " + numeroJoueur + " a voté pour le joueur " + tourActuel);

            int joueurGagnant = 0;

            tourActuel++;
            if (tourActuel > joueursDatas.Count)
            {
                joueurGagnant = TrouverJoueurGagnant();
                AttribuerPoints(joueurGagnant);
                joueursAyantVote++;

                // Réinitialisez tourActuel à 1 après chaque tour
                tourActuel = 1;
            }

            MettreAJourAffichageTour();
        }
        else
        {
            Debug.Log("Le joueur " + tourActuel + " ne peut pas voter pour lui-même.");
        }
    }

    private int TrouverJoueurGagnant()
    {
        int[] votes = new int[4];

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
        }
    }
}
