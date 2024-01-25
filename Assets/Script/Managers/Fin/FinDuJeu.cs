using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinDuJeu : MonoBehaviour
{
    [Header("LogoJoueur")]
    public Image image1;  // Faites glisser et déposez les objets Image depuis l'inspecteur Unity
    public Image image2;
    public Image image3;
    public Image image4;

    [Header("Name")]
    public Text playerName1;  // Faites glisser et déposez les objets Text depuis l'inspecteur Unity
    public Text playerName2;
    public Text playerName3;
    public Text playerName4;

    [Header("Score")]
    public Text playerScore1;
    public Text playerScore2;
    public Text playerScore3;
    public Text playerScore4;


    public JoueursDataBase joueursDataBaseRef;
    [SerializeField] public static List<JoueursData> joueursDatas = new();

    void Start()
    {
        joueursDatas.AddRange(joueursDataBaseRef.datas);
        AfficherClassement();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void AfficherClassement()
    {
        // Trier la liste des joueurs en fonction de leur score (du plus grand au plus petit)
        joueursDatas.Sort((a, b) => b.score.CompareTo(a.score));

        // Afficher le classement avec les logos, noms et scores
        for (int i = 0; i < joueursDatas.Count; i++)
        {
            // Accéder aux objets UI correspondants
            Image logoImage = GetLogoImageForRank(i + 1); // i + 1 car les indices commencent à 0
            Text playerNameText = GetPlayerNameTextForRank(i + 1);
            Text playerScoreText = GetPlayerScoreTextForRank(i + 1);

            // Assigner les données du joueur dans l'UI
            logoImage.sprite = joueursDatas[i].logoJoueur;
            playerNameText.text = joueursDatas[i].JoueursName.ToString();
            playerScoreText.text = joueursDatas[i].score.ToString();
        }
    }

    // Obtient l'objet Image pour le logo correspondant au rang
    private Image GetLogoImageForRank(int rank)
    {
        switch (rank)
        {
            case 1: return image1; // Remplacez image1 par la référence à votre Image pour le 1er
            case 2: return image2; // Remplacez image2 par la référence à votre Image pour le 2ème
            case 3: return image3; // Remplacez image3 par la référence à votre Image pour le 3ème
            case 4: return image4; // Remplacez image4 par la référence à votre Image pour le 4ème
            default: return null;
        }
    }

    // Obtient l'objet Text pour le nom du joueur correspondant au rang
    private Text GetPlayerNameTextForRank(int rank)
    {
        switch (rank)
        {
            case 1: return playerName1; // Remplacez playerName1 par la référence à votre Text pour le 1er
            case 2: return playerName2; // Remplacez playerName2 par la référence à votre Text pour le 2ème
            case 3: return playerName3; // Remplacez playerName3 par la référence à votre Text pour le 3ème
            case 4: return playerName4; // Remplacez playerName4 par la référence à votre Text pour le 4ème
            default: return null;
        }
    }

    // Obtient l'objet Text pour le score du joueur correspondant au rang
    private Text GetPlayerScoreTextForRank(int rank)
    {
        switch (rank)
        {
            case 1: return playerScore1; // Remplacez playerScore1 par la référence à votre Text pour le 1er
            case 2: return playerScore2; // Remplacez playerScore2 par la référence à votre Text pour le 2ème
            case 3: return playerScore3; // Remplacez playerScore3 par la référence à votre Text pour le 3ème
            case 4: return playerScore4; // Remplacez playerScore4 par la référence à votre Text pour le 4ème
            default: return null;
        }
    }
}
