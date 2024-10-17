using UnityEngine;

public class DeplacementCoffre : MonoBehaviour
{

    public void DeplacerObjet()
    {
        // Déplacez l'objet vers la destination
        transform.rotation = Quaternion.Euler(-76.4f, 105.8f, 0f);
    }
}
