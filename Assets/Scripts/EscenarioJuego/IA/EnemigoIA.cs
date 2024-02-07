using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{

    NavMeshAgent agente;
    public Transform destino;

    //Vida
    public float vida;
    float vidaMax;

    // Dinero que devuelve al ser derrotado
    public float dineroADevolver;

    //Barra de vida
    [SerializeField] GameObject barraDeVidaObj;
    BarradeVida barraDeVida;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    public void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.destination = destino.position;
        vidaMax = vida;
        barraDeVida = barraDeVidaObj.GetComponent<BarradeVida>();
        barraDeVida.actualizarBarraDeVida(vida, vidaMax); // para asegurar que se actualiza
        // como es debido al volver a empezar
    }

    public void takeDamage(float damage)
    {
        vida -= damage;
        barraDeVida.actualizarBarraDeVida(vida, vidaMax);
        if (vida <= 0)
        {
            morir();
        }
    }

    void morir()
    {
        GameManager.Instance.aniadirDinero(dineroADevolver);
        GameManager.Instance.aniadirPuntos(dineroADevolver);
        GameManager.Instance.aniadirMuertes();
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getVidaMax()
    {
        return vidaMax;
    }
}
