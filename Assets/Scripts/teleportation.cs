using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Nom de la scène à charger

    // Méthode appelée lors du clic sur le bouton
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // Charge la nouvelle scène
    }
}
