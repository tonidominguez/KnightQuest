using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Rogalic : Enemigos
{

    private Vector3 posPlayer;
    public bool estoyMuerto;
    private GameObject player;
    private GameObject particlePuff;

    private GameObject soundRogalicDie;
    
    
    // private NavMeshAgent agent;

    private void Awake()
    {
        
    }

    void Start()
    {
        estoyMuerto = false;
        vida = 30;
        velocidad = 10.0f;
        maxDistancia = 40.0f;
        maxDistanciaPerseguir = 35.0f;
        minDistancia = 3.0f;
        startPos = transform.position;
        GetComponent<NavMeshAgent>().speed = velocidad;
        player = (GameObject) GameObject.FindGameObjectWithTag("Player");
        particlePuff = (GameObject) GameObject.FindGameObjectWithTag("Puff");
        soundRogalicDie = (GameObject) GameObject.FindGameObjectWithTag("SonidoRogalicDie");
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

            if (closeEnough)
            {
                this.gameObject.GetComponent<Animator>().SetInteger("estado", 0);
            }

            if (!closeEnough && distancia > maxDistancia)
            {
                this.gameObject.transform.LookAt(startPos);
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(startPos);
                this.gameObject.GetComponent<Animator>().SetInteger("estado", 1);
            } 
            else 
            {
                if (distancia <= maxDistanciaPerseguir && distancia >= minDistancia)
                {
                    if(this.gameObject.GetComponent<Animator>()) this.gameObject.GetComponent<Animator>().SetInteger("estado", 2);
                    if(this.gameObject.GetComponent<NavMeshAgent>()) this.gameObject.GetComponent<NavMeshAgent>().SetDestination(posPlayer);
                }
                else if (distancia <= minDistancia && distancia <= maxDistanciaPerseguir) //Distancia para atacar
                {
                    if(this.gameObject.GetComponent<NavMeshAgent>()) this.gameObject.GetComponent<NavMeshAgent>().SetDestination(transform.position);
                    if(this.gameObject.GetComponent<Animator>()) this.gameObject.GetComponent<Animator>().SetTrigger("atacar");
                }
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
                soundRogalicDie.GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("morir");
                Invoke("EliminarAnimator",1.50f);
            }
            else
            {
                if(this.gameObject.GetComponent<Animator>()) this.gameObject.GetComponent<Animator>().SetTrigger("golpear");
                if(this.gameObject.GetComponent<NavMeshAgent>()) this.gameObject.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
        }
    }

    public void EliminarAnimator()
    {
        Destroy(this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<BoxCollider>());
        Destroy(this.gameObject.GetComponent<Animator>());
        Destroy(this.gameObject.GetComponent<EnemigosAI>());
        Destroy(this.gameObject.GetComponent<NavMeshAgent>());
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        Invoke("EliminarEnemigo", 2.0f);
    }

    public void EliminarEnemigo()
    {
        Vector3 damagePos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.0f, this.gameObject.transform.position.z);
        GameObject particles = Instantiate(particlePuff, damagePos, Quaternion.identity);
        Destroy(this.gameObject.GetComponent<Rogalic>());
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
