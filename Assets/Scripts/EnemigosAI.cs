using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosAI : MonoBehaviour
{
    GameObject player;

    float enemySpeed;
    float dis;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis <= 8.0f)
        {
            chase();
        }
        else
        {
            goHome();
        }
    }

    void chase()
    {
        transform.LookAt(player.transform.position);
        //transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
        transform.Translate(0, 0, enemySpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
    void goHome()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, enemySpeed * Time.deltaTime);
    }
}
