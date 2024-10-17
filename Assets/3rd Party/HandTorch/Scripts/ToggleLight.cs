using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{

    public GameObject Spot1ight;

    public bool onOff;
    public float maxDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Spot1ight.SetActive(onOff);

        // Lance un raycast à partir de la caméra du joueur
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Si le raycast touche cet objet, vérifie si le joueur appuie sur E
            if (hit.transform == transform && Input.GetKeyDown(KeyCode.E))
            {
                onOff = !onOff;
            }
        }
    }

    public void toggle ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            onOff = !onOff;
        }
    }
}
