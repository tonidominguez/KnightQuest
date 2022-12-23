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

    

    void Start()
    {
        
    }

    void Update()
    {
        //this.gameObject.transform.position = new Vector3(player.transform.position.x, this.gameObject.transform.position.y, player.gameObject.transform.position.z+20.0f);

    }

}
