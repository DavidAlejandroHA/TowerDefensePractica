using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLanzarProyectiles : MonoBehaviour
{
    public float cooldown;
    float temporizador;
    public float radio;
    public GameObject proyectil;
    //public Transform 
    int mascara = 1 << 6;

    //Proyectiles
    public float velocidadLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        temporizador = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // Esta lista almacenará el resultado de llamar a OverlapSphere
        Collider[] listaChoques;

        listaChoques = Physics.OverlapSphere(transform.position, radio, mascara);
        Transform enemigoMasCercano = obtenerPosEnemigoMasCercano(listaChoques);
        if (enemigoMasCercano != null)
        {
            temporizador -= Time.deltaTime;
            if (temporizador <= 0)
            {
                temporizador = cooldown;
            }

            if (temporizador == cooldown)
            {
                dispararProyectil(enemigoMasCercano);
            }
            transform.LookAt(enemigoMasCercano);
        }
    }

    Transform obtenerPosEnemigoMasCercano(Collider[] listaChoques)
    {
        Transform enemigoMasCercano = null;
        float menorDistancia = Mathf.Infinity;
        if (listaChoques.Length > 0)
        {
            foreach (Collider choque in listaChoques)
            {
                float distanciaActual = Vector3.Distance(transform.position, choque.transform.position);
                if (distanciaActual < menorDistancia)
                {
                    menorDistancia = distanciaActual;
                    enemigoMasCercano = choque.transform;
                }
            }
            // comprobar y elegir el enemigo con menor distancia
            // ir hacia el
        }
        return enemigoMasCercano; // puede llegar a ser nulo si no hay nada al rededor, hay que
                                  // tenerlo en cuenta
    }

    void dispararProyectil(Transform enemigo)
    {
        GameObject proyectil = Instantiate(this.proyectil, transform.position, transform.rotation);
        proyectil.transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, velocidadLanzamiento));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 192, 203); // Color rosa
        Gizmos.DrawWireSphere(transform.position, radio);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
