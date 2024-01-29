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
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.destination = destino.position;
        vidaMax = vida;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public float getVidaMax()
    {
        return vidaMax;
    }
}
