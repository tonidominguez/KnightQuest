using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Enemigos : MonoBehaviour
{
    protected int vida;
    protected float velocidad;
    protected GameObject player;
    protected float distancia;
    protected Vector3 startPos;
    protected NavMeshAgent agent;

    protected float maxDistancia;
    protected float maxDistanciaPerseguir;
    protected float minDistancia;

    protected float fuerzaLanzamiento;

    protected GameObject acumulador;

    protected GameObject[] loot;
    
    protected abstract void Mover();
    public abstract void GenerarDanyo(int puntosDanyo);

    private void Awake()
    {
        player = (GameObject) GameObject.FindGameObjectWithTag("Player");
        acumulador = (GameObject) GameObject.FindGameObjectWithTag("Acumulador");
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        vida = 10;
        velocidad = 5.0f;
        maxDistancia = 100.0f;
        maxDistanciaPerseguir = 30.0f;
        minDistancia = 20.0f;
        fuerzaLanzamiento = 10.0f;
        startPos = transform.position;
    }

    public int GetVida()
    {
        return vida;
    }
    public void SetVida(int v)
    {
        vida = v;
    }

    public float GetVelocidad()
    {
        return velocidad;
    }
    public void SetVelocidad(float v)
    {
        velocidad = v;
    }

    public string GetLoot()
    {
        int rand = Random.Range(0, 100);
        if (rand < 20)
        {
            return "Salud";
        }
        else if (rand < 80)
        {
            return "Moneda";
        }
        else
        {
            return "Monedas";
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
