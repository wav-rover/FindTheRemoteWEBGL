using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalTrigger : MonoBehaviour
{
    public string sceneToLoad;  // Nom de la scène à charger
    public bool isActivated = false;
    public GameObject loadingScreen;  // Optionnel : Écran de chargement

    public void ActivatePortal()
    {
        isActivated = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet qui est entré dans la sphère de détection est le joueur
        if (other.gameObject.tag == "Player" && isActivated)
        {
            // Lancer le chargement asynchrone de la nouvelle scène
            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        // Optionnel : Afficher un écran de chargement pour masquer le processus de chargement
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // Charger la scène en arrière-plan
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Optionnel : Désactiver l'activation immédiate de la scène (si tu veux plus de contrôle sur quand elle s'affiche)
        // asyncLoad.allowSceneActivation = false;

        // Attendre que la scène soit entièrement chargée
        while (!asyncLoad.isDone)
        {
            // Optionnel : Si tu veux afficher la progression, tu peux accéder à asyncLoad.progress ici
            // asyncLoad.progress est compris entre 0 (0%) et 0.9 (90%)
            yield return null;
        }

        // Optionnel : Si tu utilises un écran de chargement, le désactiver après le chargement de la scène
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        // Optionnel : Si tu utilises allowSceneActivation, active la scène ici une fois prête
        // asyncLoad.allowSceneActivation = true;
    }
}
