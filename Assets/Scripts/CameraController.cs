using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = (GameObject) GameObject.FindGameObjectWithTag("Player");
    }

}
