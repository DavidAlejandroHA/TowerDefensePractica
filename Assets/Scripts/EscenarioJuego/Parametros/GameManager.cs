using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int vidas;
    public float dinero;
    //public int dinero;
    public float tiempoRestante;
    int enemigosMuertos;
    
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
            // En caso contrario, yo me asocio como instancia �nica y global
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Se empieza con 10 puntos para hacer posible defenderse al jugador
        //aniadirDinero(10f);
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

    public void aniadirMuertes()
    {
        enemigosMuertos++;
    }
    public void aniadirDinero(float dinero)
    {
        this.dinero += dinero;
        UIManager.Instance.actualizarTextoDinero();
        ButtonManager.Instance.resetImagesColor();
    }

    public void quitarDinero(float dinero)
    {
        this.dinero -= dinero;
        ButtonManager.Instance.checkEnoughMoney(dinero);
        UIManager.Instance.actualizarTextoDinero();
        ButtonManager.Instance.resetImagesColor();
    }

    public void quitarVida()
    {
        vidas--;
        UIManager.Instance.actualizarTextoVidas();
        //puntos += 5;
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
