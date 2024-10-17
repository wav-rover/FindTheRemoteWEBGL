using UnityEngine;

public class OuvertureCoffre : MonoBehaviour
{
    public PressurePad vérifRedKey; // Référence au script de vérification de la RedKeyCrystal
    public PressurePad2 vérifGreenKey; // Référence au script de vérification de la GreenKeyCrystal
    public PressurePad3 vérifBlueKey; // Référence au script de vérification de la BlueKeyCrystal

    private bool lesTroisSontVrais = false;

    private void Update()
    {
        // Vérifier si les trois conditions sont vraies
        if (vérifRedKey.hasRedKey && vérifGreenKey.hasGreenKey && vérifBlueKey.hasBlueKey)
        {
            lesTroisSontVrais = true;
        }
        else
        {
            lesTroisSontVrais = false;
        }

        // Si toutes les conditions sont vraies, déplacer l'objet
        if (lesTroisSontVrais)
        {
            DéplacerObjet();
        }
        else
        {
            AnnulerDéplacement();
        }
    }

    private void DéplacerObjet()
    {
        // Déplacer l'objet vers la destination
        transform.rotation = Quaternion.Euler(-66.81f, 105.8f, 0f);
    }

    private void AnnulerDéplacement()
    {
        // Annuler le déplacement de l'objet
        transform.rotation = Quaternion.Euler(0f, 105.8f, 0f);
    }
}
