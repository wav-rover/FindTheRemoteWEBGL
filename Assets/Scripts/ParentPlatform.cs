using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        {
            collision.transform.SetParent(transform, true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        {
            collision.transform.SetParent(null);
        }
    }
}
