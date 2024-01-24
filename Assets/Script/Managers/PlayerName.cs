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

    public MaskableGraphic img_logoJoueurs;

    void Start()
    {
        //joueursDataBaseRef = GetComponent<JoueursDataBase>();
        MettreAJourInstruction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MettreAJourInstruction()
    {
        texteInstruction.text = "Joueur " + (numeroJoueurActuel + 1) + " enter your name";
    }
    public void SoumettreNom()
    {

        string nomJoueur = champDeSaisie.text;
        EnregistrerNomDansDataBase(nomJoueur);
        //InitieImage();

        numeroJoueurActuel++;

        if (numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            MettreAJourInstruction();
            champDeSaisie.text = ""; // Effacez le champ de saisie pour le joueur suivant
        }
        else
        {
            texteInstruction.text = "All players have entered their names. The Game is about to start !";
            // Vous pouvez ajouter ici une logique pour commencer le jeu
        }
    }

    private void EnregistrerNomDansDataBase(string nom)
    {
        if(numeroJoueurActuel < joueursDataBaseRef.datas.Count)
        {
            joueursDataBaseRef.datas[numeroJoueurActuel].JoueursName = nom;
            
        }
    }


    private void SetMaskableGraphicValue(ref MaskableGraphic mg, object value)
    {
        switch (mg)
        {
            
            case Image img: img.sprite = value as Sprite; break;
        }
    }

    private void InitieImage(JoueursData data)
    {
        SetMaskableGraphicValue(ref img_logoJoueurs, data.logoJoueur);
    }

}
