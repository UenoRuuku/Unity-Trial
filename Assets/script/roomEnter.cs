using System.Xml.Serialization;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomEnter : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject virtualCamera;

    void OnTriggerEnter2D(Collider2D other)
    {
        virtualCamera.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        virtualCamera.gameObject.SetActive(false);
    }
}
