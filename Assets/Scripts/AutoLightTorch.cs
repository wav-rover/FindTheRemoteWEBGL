using UnityEngine;

public class AutoLightTorch : MonoBehaviour
{
    public Transform joueur; // Référence au transform du joueur
    public float distanceAllumage = 5f; // Distance à laquelle la lumière s'allume
    public float vitesseAllumage = 2f; // Vitesse d'allumage de la lumière

    private Light lumiere; // Référence à la composante Light de l'enfant
    private float luminositeCible; // Luminosité cible de la torche

    void Start()
    {
        // Récupère la composante Light de l'enfant
        lumiere = GetComponentInChildren<Light>();

        // Assure-toi que la lumière est éteinte au début
        lumiere.enabled = false;
    }

    void Update()
    {
        // Vérifie si la distance entre la torche et le joueur est inférieure à la distance d'allumage
        if (Vector3.Distance(transform.position, joueur.position) < distanceAllumage)
        {
            lumiere.enabled = true;
            // Calcule la luminosité cible en fonction de la distance entre la torche et le joueur
            luminositeCible = 3f;

            // Interpole la luminosité actuelle de la torche vers la luminosité cible
            lumiere.intensity = Mathf.MoveTowards(lumiere.intensity, luminositeCible, Time.deltaTime * vitesseAllumage);
        }
        else
        {
            lumiere.enabled = false;
            // Si le joueur est hors de portée, éteint progressivement la lumière
            luminositeCible = 0f;
            lumiere.intensity = Mathf.MoveTowards(lumiere.intensity, luminositeCible, Time.deltaTime * vitesseAllumage);
        }

        // Active ou désactive la lumière en fonction de la luminosité
        lumiere.enabled = lumiere.intensity > 0f;
    }
}
