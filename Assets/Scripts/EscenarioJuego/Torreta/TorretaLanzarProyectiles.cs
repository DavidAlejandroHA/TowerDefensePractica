using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLanzarProyectiles : MonoBehaviour
{
    public int lanzamientos;
    int lanzamientosIniciales;
    public float cooldown;
    float temporizador;
    public float radio;
    public GameObject proyectil;
    public GameObject anchorDisparador; 
    int mascara = 1 << 6;

    //Barra de municion
    [SerializeField] GameObject barraDeMunicionObj;
    BarraMunicion barraDeMunicion;

    //Proyectiles
    public float velocidadLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
        lanzamientosIniciales = lanzamientos;
        barraDeMunicion = barraDeMunicionObj.GetComponent<BarraMunicion>();
        temporizador = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Esta lista almacenará el resultado de llamar a OverlapSphere
        Collider[] listaChoques;

        listaChoques = Physics.OverlapSphere(transform.position, radio, mascara);

        // se obtiene el enemigo más cercano a la torreta
        Transform enemigoMasCercano = obtenerPosEnemigoMasCercano(listaChoques);
        if (enemigoMasCercano != null)
        {
            // Temporizador para disparar cada x tiempo
            temporizador -= Time.deltaTime;
            if (temporizador <= 0)
            {
                temporizador = cooldown;
            }

            // Se dispara un proyectil cada vez que se reinicia el temporizador
            if (temporizador == cooldown)
            {
                dispararProyectil(enemigoMasCercano);
            }
            // La torreta va a estar siempre apuntando al enemigo para ser más realista
            transform.LookAt(enemigoMasCercano);
        }
    }

    Transform obtenerPosEnemigoMasCercano(Collider[] listaChoques)
    {
        Transform enemigoMasCercano = null;
        float menorDistancia = Mathf.Infinity;

        // Se comprueba y elige el enemigo con menor distancia
        if (listaChoques.Length > 0)
        {
            foreach (Collider choque in listaChoques)
            {
                float distanciaActual = Vector3.Distance(transform.position, choque.transform.position);
                if (distanciaActual < menorDistancia)
                {
                    /* Se detectan enemigos dentro del radio de acción pero hay que comprobar que
                     * no hay muros por delante*/
                    if (comprobarQueNoHayObstaculos(choque.transform))
                    {
                        menorDistancia = distanciaActual;
                        enemigoMasCercano = choque.transform;
                    }
                }
            }
        }
        return enemigoMasCercano; // puede llegar a ser nulo si no hay nada al rededor, hay que                    
    }                               // tenerlo en cuenta

    private bool comprobarQueNoHayObstaculos(Transform enemigoMasCercano)
    {
        bool enemigoVisible = true;
        if (enemigoMasCercano != null)
        {
            /* Primero intenté esta parte con raycastall pero al final solo funciono con un linecast, 
             * lo dejo como nota por si acaso*/

            RaycastHit hit;
            if (Physics.Linecast(transform.position, enemigoMasCercano.transform.position, out hit))
            {
                if (hit.transform.tag != "Proyectil" && hit.collider.gameObject.tag != "Enemigo" 
                    /*&& hit.collider.gameObject.tag != "Artilleria"*/)
                {
                    enemigoVisible = false;
                }
            }
        }

        return enemigoVisible;
    }

    void dispararProyectil(Transform enemigo)
    {
        if(lanzamientos > 0)
        {
            GameObject proyectil = Instantiate(this.proyectil, transform.position, transform.rotation);
            proyectil.SetActive(true);

            float velocidadMovimiento = proyectil.GetComponent<Proyectil>().velocidadMovimiento;
            // obtiene la velocidad que tiene el tipo de proyectil que se va a lanzar

            proyectil.transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0,
                velocidadLanzamiento + velocidadMovimiento));
            // se lanza dicho proyectil a la velocidad que le corresponde + la velocidad que tiene por defecto
            // la torreta

            proyectil.GetComponent<Proyectil>().destruirProyectil(); /* Destruye el proyectil una vez
                                                                      que pase su tiempo de vida*/
            // destruye el proyectil en los segundos que tiene asignado
            lanzamientos--;
            barraDeMunicion.actualizarBarraDeMunicion(lanzamientos, lanzamientosIniciales);
        } else
        {
            Destroy(this.transform.parent.gameObject); // elimina al padre de la torreta que es el que
                                                       // gestiona el área de colisiones y la torreta en si
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 192, 203); // Color rosa
        Gizmos.DrawWireSphere(transform.position, radio);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
