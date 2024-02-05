using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int vidas;
    public float puntos;
    public int dinero;
    public float tiempoRestante;
    
    bool partidaActiva = true;

    private void Awake()
    {
        // Si hay alguna instancia, y dicha instancia no soy yo, me la cargo

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // En caso contrario, yo me asocio como instancia única y global
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (partidaActiva)
        {
            tiempoRestante -= Time.deltaTime;
            
        }

        if (tiempoRestante <= 0)
        {
            terminarPartida();
        }
        
    }

    public void aniadirPuntos(float puntos)
    {
        this.puntos += puntos;
        UIManager.Instance.actualizarTextoDinero();
    }

    public void quitarVida()
    {
        vidas--;
        UIManager.Instance.actualizarTextoVidas();

        //TODO: Finalizar juego al perder todas las vidas
        if (vidas <= 0)
        {
            terminarPartida();
        }
    }

    public bool getPartidaActiva()
    {
        return partidaActiva;
    }

    public void terminarPartida()
    {
        partidaActiva = false;
        Time.timeScale = 0f;
    }
}
