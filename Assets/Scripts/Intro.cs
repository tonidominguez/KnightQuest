using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Intro")
        {
            player.GetComponent<Animator>().SetInteger("estadoPlayer", 1);   
            Invoke("Jugar", 30.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EscenaIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Jugar()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
