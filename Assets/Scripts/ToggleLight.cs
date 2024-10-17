using UnityEngine;

public class TogglingLight : MonoBehaviour
{
    public float intensity = 1f; // Intensité de la lumière lorsque activée

    private bool lightActivated = false; // Indique si la lumière est activée ou non

    private void Update()
    {
        // Vérifie si l'objet avec le tag "toggle light" est au milieu de l'écran
        if (IsObjectAtCenter() && Input.GetMouseButtonDown(0))
        {
            ToggleLight(); // Active ou désactive la lumière
        }
    }

    private bool IsObjectAtCenter()
    {
        // Obtient la position de l'objet dans l'espace écran
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Vérifie si l'objet est proche du centre de l'écran
        return screenPosition.x > Screen.width * 0.45f && screenPosition.x < Screen.width * 0.55f &&
               screenPosition.y > Screen.height * 0.45f && screenPosition.y < Screen.height * 0.55f;
    }

    private void ToggleLight()
    {
        Light objectLight = GetComponentInChildren<Light>(); // Obtient le composant lumière enfant

        if (objectLight != null)
        {
            if (lightActivated)
            {
                objectLight.intensity = 0f; // Réduit l'intensité de la lumière à zéro
            }
            else
            {
                objectLight.intensity = intensity; // Définit l'intensité de la lumière
            }
            lightActivated = !lightActivated; // Inverse l'état de la lumière
        }
    }
}
