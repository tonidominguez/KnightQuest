using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class General
{
    private static int vida = 100;
    private static int monedas = 0;
    private static int danyoArmaPlayer = 10;
    private static int totalReliquias = 4;
    private static int reliquiasRecogidas = 0;

    public static int GetReliquiasRecogidas()
    {
        return reliquiasRecogidas;
    }
    public static void SetReliquiasRecogidas(int r)
    {
        reliquiasRecogidas = r;
    }

    public static int GetTotalReliquias()
    {
        return totalReliquias;
    }

    public static int GetVida()
    {
        return vida;
    }
    public static void SetVida(int v)
    {
        vida = v;
    }

    public static int GetMonedas()
    {
        return monedas;
    }
    public static void SetMonedas(int m)
    {
        monedas = m;
    }
    public static void AddMonedas(int m)
    {
        monedas += m;
    }

    public static int GetDanyoArmaPlayer()
    {
        return danyoArmaPlayer;
    }
    public static void SetDanyoArmaPlayer(int d)
    {
        danyoArmaPlayer = d;
    }
    public static void AddDanyoArmaPlayer(int d)
    {
        danyoArmaPlayer += d;
    }


    /*private static bool cocktailRecogido = false;
    private static bool flowerRecogido = false;
    private static bool hayForRecogido = false;

    private static bool pregunta1Acertada = false;
    private static bool pregunta2Acertada = false;
    private static bool pregunta3Acertada = false;

    public static bool GetCocktailRecogido()
    {
        return cocktailRecogido;
    }
    public static void SetCocktailRecogido(bool value)
    {
        cocktailRecogido = value;
    }

    public static bool GetFlowerRecogido()
    {
        return flowerRecogido;
    }
    public static void SetFlowerRecogido(bool value)
    {
        flowerRecogido = value;
    }

    public static bool GetHayForkRecogido()
    {
        return hayForRecogido;
    }
    public static void SetHayForkRecogido(bool value)
    {
        hayForRecogido = value;
    }

    public static bool GetPregunta1Acertada()
    {
        return pregunta1Acertada;
    }
    public static void SetPregunta1Acertada(bool value)
    {
        pregunta1Acertada = value;
    }

    public static bool GetPregunta2Acertada()
    {
        return pregunta2Acertada;
    }
    public static void SetPregunta2Acertada(bool value)
    {
        pregunta2Acertada = value;
    }

    public static bool GetPregunta3Acertada()
    {
        return pregunta3Acertada;
    }
    public static void SetPregunta3Acertada(bool value)
    {
        pregunta3Acertada = value;
    }*/
}
