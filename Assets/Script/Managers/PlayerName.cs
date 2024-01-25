using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public InputField champDeSaisie;
    private int numeroJoueurActuel = 0;
    public JoueursDataBase joueursDataBaseRef;
    public Text texteInstruction;

    public Image img_logoJoueurs; // Ajoutez cette référence dans l'éditeur Unity

    void Start()
    {
        MettreAJourInstruction();
        InitieImage();
    }

    private void MettreAJourInstruction()
    {
        texteInstruction.text = "Joueur " + (numeroJoueurActuel + 1) + " enter your name";
    }

    public void SoumettreNom()
    {
        string nomJoueur = champDeSaisie.text;
        EnregistrerNomDansDataBase(nomJoueur);

        numeroJoueurActuel++;

        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            MettreAJourInstruction();
            champDeSaisie.text = ""; // Effacez le champ de saisie pour le joueur suivant
            InitieImage(); // Mettez à jour le logo après le passage au joueur suivant
        }
        else
        {
            texteInstruction.text = "All players have entered their names. The Game is about to start !";
            // Vous pouvez ajouter ici une logique pour commencer le jeu
        }
    }

    private void EnregistrerNomDansDataBase(string nom)
    {
        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            joueursDataBaseRef.datas[numeroJoueurActuel].JoueursName = nom;
        }
    }

    private void InitieImage()
    {
        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            JoueursData joueurActuel = joueursDataBaseRef.datas[numeroJoueurActuel];
            if (img_logoJoueurs != null && joueurActuel != null)
            {
                img_logoJoueurs.sprite = joueurActuel.logoJoueur;
            }
        }
    }
}
