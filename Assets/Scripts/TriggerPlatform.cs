using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    PlatformMoving platform;

    private void Start()
    {
        platform = GetComponent<PlatformMoving>();
    }

    private void OnTriggerEnter(Collider other)
    {
        platform.moving = true; // Définir moving à true au lieu de canMove
    }
}
