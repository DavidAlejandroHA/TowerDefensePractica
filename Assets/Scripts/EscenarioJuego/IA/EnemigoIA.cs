using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{

    NavMeshAgent agente;
    public Transform destino;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.destination = destino.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
