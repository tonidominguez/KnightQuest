using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Snake : Enemigos
{

    private Vector3 posPlayer;
    public bool estoyMuerto;
    private GameObject particlePuff;

    private GameObject soundSnakeDie;

    void Start()
    {
        estoyMuerto = false;
        vida = 40;
        velocidad = 9.0f;
        maxDistancia = 25.0f;
        maxDistanciaPerseguir = 15.0f;
        minDistancia = 7.0f;
        startPos = transform.position;
        particlePuff = (GameObject) GameObject.FindGameObjectWithTag("Puff");
        soundSnakeDie = (GameObject) GameObject.FindGameObjectWithTag("SonidoSnakeDie");
    }

    private void Update()
    {
        Mover();
    }

    protected override void Mover()
    {
        if(!estoyMuerto)
        {
            posPlayer = new Vector3(player.transform.position.x, this.gameObject.transform.position.y, player.transform.position.z);
            this.gameObject.transform.LookAt(posPlayer);
            distancia = Vector3.Distance(this.gameObject.transform.position, posPlayer);

            bool closeEnough = FastApproximately(startPos.x, transform.position.x, 3.5f)
                            && FastApproximately(startPos.z, transform.position.z, 3.5f);


            if (distancia <= maxDistancia && distancia >= minDistancia)
            {
                if(this.gameObject.GetComponent<Animator>()) this.gameObject.GetComponent<Animator>().SetInteger("estado", 1);
            }
            else if (distancia <= minDistancia) //Distancia para atacar
            {
                this.gameObject.transform.LookAt(startPos);
                this.gameObject.GetComponent<Animator>().SetTrigger("atacar");
            }
            else {
                this.gameObject.GetComponent<Animator>().SetInteger("estado", 0);
            }
            
            
            
        }
    }

    public override void GenerarDanyo(int puntosDanyo)
    {
        if(!estoyMuerto)
        {
            vida = vida - puntosDanyo;
            if (vida <= 0)
            {
                estoyMuerto = true;
                soundSnakeDie.GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("morir");
                Invoke("EliminarAnimator",3.20f);
            }
            else
            {
                if(this.gameObject.GetComponent<Animator>()) this.gameObject.GetComponent<Animator>().SetTrigger("golpear");
            }
        }
    }

    public void EliminarAnimator()
    {
        Vector3 damagePos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.0f, this.gameObject.transform.position.z);
        GameObject particles = Instantiate(particlePuff, damagePos, Quaternion.identity);
        Destroy(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(0).GetComponent<BoxCollider>());
        Destroy(this.gameObject.GetComponent<Animator>());
        Destroy(this.gameObject.GetComponent<EnemigosAI>());
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        Invoke("EliminarEnemigo", 2.0f);
    }

    public void EliminarEnemigo()
    {
        Destroy(this.gameObject.GetComponent<Snake>());
        GameObject loot = (GameObject) GameObject.FindGameObjectWithTag(GetLoot());
        Instantiate(loot, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public static bool FastApproximately(float a, float b, float threshold)
    {
            if (threshold > 0f)
            {
                return Mathf.Abs(a- b) <= threshold;
            }
            else
            {
                return Mathf.Approximately(a, b);
            }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "ArmaPlayer")
        {
            GenerarDanyo(General.GetDanyoArmaPlayer());
        }

    }

}
