using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public int vidas;
    public float puntos;
    public int dinero;

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
        
    }

    public void aniadirPuntos(float puntos)
    {
        this.puntos += puntos;
    }

    public void quitarVida()
    {
        vidas--;
    }
}
