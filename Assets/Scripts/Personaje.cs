using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    private GameObject protagonista;

    private void Update()
    {
        if (protagonista!=null)
        {
            this.gameObject.transform.LookAt(protagonista.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            protagonista = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        protagonista = null;

    }

}
