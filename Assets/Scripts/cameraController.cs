using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float sensitivity = 1.0f; // Sensibilité de la souris pour la rotation de la caméra
    public Transform playerBody; // Référence au corps du joueur pour la rotation horizontale
    public float normalFOV = 60f; // FOV normal
    public float boostedFOV = 70f; // FOV boosté lorsque Shift est enfoncé
    public float fovChangeSpeed = 5f; // Vitesse de changement de FOV

    private float targetFOV; // Valeur cible de la FOV

    float rotationX = 0.0f;
    float rotationZ = 0.0f; // Rotation sur l'axe Z de la caméra

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
        targetFOV = normalFOV; // FOV initial
    }

    void Update()
    {
        // Rotation verticale de la caméra (regarder vers le haut et vers le bas)
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limite la rotation verticale pour éviter les retournements

        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, rotationZ); // Applique la rotation à la caméra

        // Rotation horizontale du joueur (regarder à gauche et à droite)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        // Détection de la touche Shift et ajustement de la FOV cible
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            targetFOV = boostedFOV;
        }
        else
        {
            targetFOV = normalFOV;
        }

        // Interpolation de la FOV actuelle vers la FOV cible
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
    }
}