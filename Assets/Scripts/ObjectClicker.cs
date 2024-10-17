using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public ParticleSystem openAnimation;
    public ParticleSystem openAnimation2;
    public ParticleSystem openAnimation3; // Système de particules pour l'ouverture du portail
    public PortalTrigger portalTrigger; // Référence au script PortalTrigger
    public GameObject portalBg; // Objet à activer
    public DoorOpen doorOpen; // Référence au script DoorOpen

    public void OnMouseDown()
    {
        // Activer l'animation d'ouverture du portail
        if (openAnimation != null)
        {
            openAnimation.gameObject.SetActive(true);
            openAnimation.Play();
        }

        if (openAnimation2 != null)
        {
            openAnimation2.gameObject.SetActive(true);
            openAnimation2.Play();
        }

        if (openAnimation3 != null)
        {
            openAnimation3.gameObject.SetActive(true);
            openAnimation3.Play();
        }

        // Activer le portail
        if (portalTrigger != null)
        {
            portalTrigger.ActivatePortal();
        }
        else
        {
            Debug.LogWarning("PortalTrigger not assigned.");
        }

        // Activer l'objet
        if (portalBg != null)
        {
            portalBg.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate not assigned.");
        }

        // Activer le script DoorOpen
        if (doorOpen != null)
        {
            doorOpen.StartRotation(); // Call StartRotation() instead of RotateDoor()
        }
    }
}