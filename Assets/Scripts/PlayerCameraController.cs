using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float sensitivity = 1.0f; // Sensibilité de la souris pour la rotation de la caméra
    public Transform playerBody; // Référence au corps du joueur pour la rotation horizontale
    public float normalFOV = 60f; // FOV normal
    public float boostedFOV = 70f; // FOV boosté lorsque Shift est enfoncé
    public float fovChangeSpeed = 5f; // Vitesse de changement de FOV

    private float targetFOV; // Valeur cible de la FOV
    private float rotationX = 0.0f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
        targetFOV = normalFOV; // FOV initial
    }

    void Update()
    {
        // Rotation verticale de la caméra (regarder vers le haut et vers le bas)
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limite la rotation verticale

        // Appliquer la rotation à la caméra
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f); 

        // Rotation horizontale du joueur
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        playerBody.Rotate(Vector3.up * mouseX);

        // Détection de la touche Shift et ajustement de la FOV cible
        targetFOV = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? boostedFOV : normalFOV;

        // Interpolation de la FOV actuelle vers la FOV cible
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * fovChangeSpeed);
    }
}
