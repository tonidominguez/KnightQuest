using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{

    public TextMeshProUGUI textoMonedas;
    public Slider barraVida;
    public GameObject[] UIReliquias;

    public GameObject panelFaltanReliquias;
    public GameObject panelTodasReliquias;
    public GameObject nieblaPueblo;
    public GameObject panelPausa;
    public GameObject panelGameOver;

    public GameObject sonidoVictoriaFinal;

    // Start is called before the first frame update
    void Start()
    {
        SetBarraDeVida();
        panelFaltanReliquias.SetActive(false);
        panelTodasReliquias.SetActive(false);
        panelPausa.SetActive(false);
        panelGameOver.SetActive(false);
        nieblaPueblo.SetActive(true);
        sonidoVictoriaFinal = (GameObject) GameObject.FindGameObjectWithTag("SonidoVictoriaFinal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBarraDeVida()
    {
        if(General.GetVida() > 100)
        {
            General.SetVida(100);
        }
        barraVida.GetComponent<Slider>().value = General.GetVida();
    }

    public void SetTextoMonedas()
    {
        textoMonedas.GetComponent<TextMeshProUGUI>().text = General.GetMonedas().ToString();
    }

    public void SetUIReliquias()
    {
        for(int i = 0; i < General.GetReliquiasRecogidas(); i++)
        {
            UIReliquias[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        panelGameOver.SetActive(true);
    }

    public void Pausar()
    {
        Time.timeScale = 0;
        panelPausa.SetActive(true);
    }

    public void CerrarPausa()
    {
        Time.timeScale = 1;
        panelPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
