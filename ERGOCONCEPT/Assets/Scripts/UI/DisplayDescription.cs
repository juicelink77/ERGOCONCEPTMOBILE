using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDescription : MonoBehaviour
{
    public InputField Title;
    public InputField Reference;
    public InputField Description;
    public InputField Specs;
    public InputField Options;
    public Image Image;

    private void OnEnable()
    {
        SetItemDescriptionFields();
    }

    public void SetItemDescriptionFields()
    {
        Title.text = "GAÏA – ROULETTES";
        Reference.text = "GA MS2V2 1DG2 GZ14";
        Description.text = "FAUTEUIL DE SOINS MOBILE À HAUTEUR FIXE\nGAIA est un fauteuil médical de convalescence mobile et à hauteur fixe. Il est remarquable par sa robustesse, sa polyvalence et son ergonomie.\n\nLe dossier et l’assise sont synchronisés.\n\nSes deux versions de synchronisation dossier / repose - jambes et ses nombreuses possibilités d’accessoirisation permettent une utilisation adaptée à toutes les situations de convalescence tout en optimisant le confort du patient.";
        Specs.text = "CARACTÉRISTIQUES\nÉquipement de série de la gamme médicale GAIA  :\n\nAppui - tête standard amovible pour faciliter le transfert, la mobilisation et les soins du patient\nAccoudoirs escamotables et réglables en hauteur pour sécuriser les transferts du patient\nAssise déclipsable et amovible pour simplifier l’entretien et une hygiène parfaite de votre fauteuil\nCoussin du repose - jambes coulissant et amovible pour s’adapter à toutes les morphologies et simplifier l’entretien\nFreinage centralisé des roues arrières pour un freinage rapide et sécurisé\nDeux versions sont proposées :\n\nDossier / Repose - jambes inclinables par vérins indépendants\nDossier / Repose - jambes inclinables en simultané\nDisponible sur patins.\n\nSes deux versions de synchronisation dossier / repose - jambes et ses nombreuses possibilités d’accessoirisation permettent une utilisation adaptée à toutes les situations de convalescence tout en optimisant le confort du patient.";
        Options.text = "EN OPTION\n\nAppui - tête avec cale nuque ergonomique réglable\nFreinage indépendants des roues avants\nAssise prévention des escarres\nRange document transparent avec support=\nRoulettes double galets Ø 125 mm";
    }
}