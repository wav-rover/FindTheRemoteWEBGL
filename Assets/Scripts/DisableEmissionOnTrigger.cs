using UnityEngine;

public class DisableEmissionOnTrigger : MonoBehaviour
{
    public Material emissionMaterial; // Le matériau dont vous souhaitez désactiver l'émission
    public float interactionRange = 2f; // La portée de l'interaction avec le bouton

    private bool buttonActivated = false; // Ajout de l'initialisation de la variable buttonActivated

    void Update()
    {
        // Si la touche "E" est enfoncée et le bouton n'est pas déjà activé
        if (Input.GetKeyDown(KeyCode.E) && !buttonActivated)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange); // Recherche les objets à proximité

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player")) // Vérifie si le joueur est à proximité
                {
                    ActivateButton(); // Active le bouton
                    break;
                }
            }
        }
    }

    void ActivateButton()
    {
        buttonActivated = true;

        Renderer[] renderers = FindObjectsOfType<Renderer>(); // Trouve tous les renderers dans la scène

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;

            foreach (Material mat in materials)
            {
                if (mat == emissionMaterial) // Vérifie si c'est le matériau que vous voulez désactiver
                {
                    mat.SetColor("_EmissionColor", Color.black); // Désactive l'émission en réglant la couleur d'émission sur noir
                }
            }
        }
    }
}
