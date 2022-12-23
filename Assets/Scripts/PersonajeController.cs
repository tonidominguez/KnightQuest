using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonajeController : MonoBehaviour
{

    private GameObject camara;
    private Ray rayo;
    private RaycastHit hit;
    private Vector3 puntoASeguir;
    private float distancia;
    private float fuerzaSalto;
    private GameObject playerMaterial;

    private GameObject soundHorseGallop;
    private GameObject soundCoin;
    private GameObject soundCoins;
    private GameObject soundPlayerDamage;
    private GameObject soundPlayerHit;
    private GameObject soundPlayerJump;
    private GameObject soundPlayerHealth;
    private GameObject soundPlayerDie;
    private GameObject soundReliquia;
 
    private GameObject acumulador;
    
    private GameObject personajeHablando;

    public CharacterController characterController;
    private Vector3 velocity;
    public float Speed;
    public float jump;
    public float Gravity;
    public float horizontal;
    public float vertical;
    public Vector3 move;
    private bool isJumping = false;
    public bool isAttacking = false;
    private bool isDamageable = true;

    private GameObject particlesMoneda;
    private GameObject particlesMonedas;
    private GameObject particlesHit;

    private void Awake()
    {
        camara = (GameObject) GameObject.FindGameObjectWithTag("MainCamera");
        acumulador = (GameObject) GameObject.FindGameObjectWithTag("Acumulador");

        soundHorseGallop = (GameObject) GameObject.FindGameObjectWithTag("HorseGallop");
        soundCoin   = (GameObject) GameObject.FindGameObjectWithTag("SonidoCoin");
        soundCoins  = (GameObject) GameObject.FindGameObjectWithTag("SonidoCoins");
        soundPlayerDamage = (GameObject) GameObject.FindGameObjectWithTag("SonidoPlayerDamage");
        soundPlayerHit = (GameObject) GameObject.FindGameObjectWithTag("SonidoPlayerHit");
        soundPlayerJump = (GameObject) GameObject.FindGameObjectWithTag("SonidoPlayerJump");
        soundPlayerHealth = (GameObject) GameObject.FindGameObjectWithTag("SonidoPlayerHealth");
        soundPlayerDie = (GameObject) GameObject.FindGameObjectWithTag("SonidoPlayerDie");
        soundReliquia = (GameObject) GameObject.FindGameObjectWithTag("SonidoReliquia");

        playerMaterial = (GameObject) GameObject.FindGameObjectWithTag("PlayerMaterial");
        particlesMoneda = (GameObject) GameObject.FindGameObjectWithTag("ParticlesMoneda");
        particlesMonedas = (GameObject) GameObject.FindGameObjectWithTag("ParticlesMonedas");
        particlesHit = (GameObject) GameObject.FindGameObjectWithTag("HitRock02");
        Speed = 10.0f;
        jump = 30.0f;
        Gravity = -80.0f;
        fuerzaSalto = 10.0f;
        horizontal = Input.GetAxis("Horizontal") * Speed;
        vertical = Input.GetAxis("Vertical") *Speed;
        move = transform.right * horizontal + transform.forward * vertical;
        General.SetVida(100);
        General.SetMonedas(0);
        General.SetReliquiasRecogidas(0);

    }

    private void Start()
    {
        //puntoASeguir = this.gameObject.transform.position;
    }

    private void Update()
    {

        //On click mouse, player jumps
        
        characterController.Move(move * Speed * Time.deltaTime);
        // for jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        // (-0.5) change this value according to your character y position + 1
        {
            velocity.y = jump;
            isJumping = true;
            this.GetComponent<Animator>().SetBool("saltar",true);
            soundPlayerJump.GetComponent<AudioSource>().Play();
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
        }
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(acumulador.GetComponent<UIController>().panelPausa.activeSelf == false)
            {
                acumulador.GetComponent<UIController>().Pausar();
            }
            else
            {
                acumulador.GetComponent<UIController>().CerrarPausa();
            }
        }
        
        if (characterController.isGrounded)
        {
            velocity.y = 0;
            isJumping = false;
            this.GetComponent<Animator>().SetBool("saltar",false);
            //this.GetComponent<Animator>().SetInteger("estadoPlayer", 0);
        }
        //Horizontal movement
        if  (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //this.gameObject.transform.LookAt(puntoASeguir);
            if(soundHorseGallop.GetComponent<AudioSource>().isPlaying == false)
            {
                soundHorseGallop.GetComponent<AudioSource>().Play();
            }
            //this.GetComponent<Animator>().SetBool("saltar",false);
            this.GetComponent<Animator>().SetInteger("estadoPlayer", 1);
        }
        else 
        {
            //this.GetComponent<Animator>().SetBool("saltar",false);
            this.GetComponent<Animator>().SetInteger("estadoPlayer", 0);
            soundHorseGallop.GetComponent<AudioSource>().Stop();
        }


        if (Input.GetAxis("Fire1") > 0 && !isAttacking)
        {
            soundPlayerHit.GetComponent<AudioSource>().Play();
            isAttacking = true;
            this.GetComponent<Animator>().SetTrigger("atacar_1");
            Invoke("StopAttacking", 0.5f);
        }
        else if (Input.GetAxis("Fire2") > 0 && !isAttacking)
        {
            soundPlayerHit.GetComponent<AudioSource>().Play();
            isAttacking = true;
            this.GetComponent<Animator>().SetTrigger("atacar_2");
            Invoke("StopAttacking", 0.5f);
        }
        else if (Input.GetAxis("Fire3") > 0 && !isAttacking)
        {
            soundPlayerHit.GetComponent<AudioSource>().Play();
            isAttacking = true;
            this.GetComponent<Animator>().SetTrigger("atacar_3");
            Invoke("StopAttacking", 0.5f);
        }

        //distancia = Vector3.Distance(this.gameObject.transform.position, puntoASeguir);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Terreno")
        {
            isJumping = false;
        }

        if (isDamageable && !isAttacking && (other.gameObject.tag == "ArmaDino" || other.gameObject.tag == "ArmaRogalic"))
        {
            SetDamage(5);
        }
        else if (isDamageable && !isAttacking && other.gameObject.tag=="ArmaDevil")
        {
            SetDamage(7);
        }
        else if (isDamageable && !isAttacking && other.gameObject.tag == "ArmaSnake")
        {
            SetDamage(3);
        }
        else if (isDamageable && !isAttacking && other.gameObject.tag == "Spider")
        {
            SetDamage(1);
        }

        if (other.gameObject.tag == "ReliquiaHacha" || other.gameObject.tag == "ReliquiaCofre" || other.gameObject.tag == "ReliquiaFrasco" || other.gameObject.tag == "ReliquiaEscudo")
        {
            soundReliquia.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            General.SetReliquiasRecogidas(General.GetReliquiasRecogidas() + 1);
            acumulador.GetComponent<UIController>().SetUIReliquias();
        }

        if (other.gameObject.tag == "Salud")
        {
            if (General.GetVida()<100)
            {
                SetHealth(15);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Moneda")
        {
            soundCoin.GetComponent<AudioSource>().Play();
            GameObject particles = Instantiate(particlesMoneda, other.gameObject.transform.position, Quaternion.identity);
            General.AddMonedas(1);
            acumulador.GetComponent<UIController>().SetTextoMonedas();
            Destroy(other.gameObject);
            Debug.Log("Monedas: " + General.GetMonedas());
            Destroy(particles, 2.0f);
        }
        else if (other.gameObject.tag == "Monedas")
        {
            soundCoins.GetComponent<AudioSource>().Play();
            Instantiate(particlesMonedas, other.gameObject.transform.position, Quaternion.identity);
            General.AddMonedas(25);
            acumulador.GetComponent<UIController>().SetTextoMonedas();
            Destroy(other.gameObject);
            Debug.Log("Monedas: " + General.GetMonedas());
        }
        else if (other.gameObject.tag == "Caldero")
        {
            if(General.GetReliquiasRecogidas() == 4)
            {
                Victoria();
            }
            else
            {
                TextoFaltanReliquias();
            }
        }
        
    }

    public void Victoria()
    {
        acumulador.GetComponent<UIController>().panelTodasReliquias.SetActive(true);
        acumulador.GetComponent<UIController>().nieblaPueblo.SetActive(false);
        acumulador.GetComponent<UIController>().sonidoVictoriaFinal.GetComponent<AudioSource>().Play();
    }

    public void TextoFaltanReliquias()
    {
        //Faltan reliquias
        acumulador.GetComponent<UIController>().panelFaltanReliquias.SetActive(true);
        Invoke("QuitarTextoFaltanReliquias", 3.0f);
    }

    public void QuitarTextoFaltanReliquias()
    {
        acumulador.GetComponent<UIController>().panelFaltanReliquias.SetActive(false);
        //Faltan reliquias
    }

    public void SetHealth(int health)
    {
        if (General.GetVida() + health > 100)
        {
            General.SetVida(100);
        }
        else
        {
            General.SetVida(General.GetVida() + health);
        }
        //Debug.Log(acumulador.GetComponent<UIController>().barraVida.value);
        acumulador.GetComponent<UIController>().SetBarraDeVida();
        soundPlayerHealth.GetComponent<AudioSource>().Play();
        this.gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Renderer>().material.color = new Color(0, 0.78f, 0.78f, 0.25f);
        Invoke("Damageable", 0.5f);
    }

    public void SetDamage(int damage)
    {
        soundPlayerDamage.GetComponent<AudioSource>().Play();
        General.SetVida(General.GetVida() - damage);
        Vector3 damageZone = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
        Instantiate(particlesHit, damageZone, Quaternion.identity);
        acumulador.GetComponent<UIController>().SetBarraDeVida();
        Debug.Log("Vida: " + General.GetVida());
        this.GetComponent<Animator>().SetTrigger("golpear");
        isDamageable = false;
        this.gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Renderer>().material.color = new Color(0.78f, 0, 0, 0.25f);
        if(General.GetVida()<=0)
        {
            Morir();
        }
        else
        {
            Invoke("Damageable", 1.0f);
        }
    }

    public void Morir()
    {
        this.gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        soundPlayerDie.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().SetTrigger("morir");
        Invoke("AbrirPanelGameOver", 2.0f);
    }

    public void AbrirPanelGameOver()
    {
        acumulador.GetComponent<UIController>().panelGameOver.SetActive(true);
    }

    public void Damageable()
    {
        CleanColor();
        isDamageable = true;
    }

    public void CleanColor()
    {
        this.gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
    }

    public void StopAttacking()
    {
        isAttacking = false;
    }

}
