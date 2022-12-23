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
}
