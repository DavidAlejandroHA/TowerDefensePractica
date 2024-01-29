using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLanzarProyectiles : MonoBehaviour
{
    public int lanzamientos;
    public float cooldown;
    float temporizador;
    public float radio;
    public GameObject proyectil;
    public GameObject anchorTorreta; 
    int mascara = 1 << 6;

    //Proyectiles
    public float velocidadLanzamiento;
    // Start is called before the first frame update
    void Start()
    {
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
            // temporizador para disparar cada x tiempo
            temporizador -= Time.deltaTime;
            if (temporizador <= 0)
            {
                temporizador = cooldown;
            }

            // se dispara un proyectil cada vez que se reinicia el temporizador
            if (temporizador == cooldown)
            {
                dispararProyectil(enemigoMasCercano);
            }
            // la torreta va a estar siempre apuntando al enemigo para ser más realista
            transform.LookAt(enemigoMasCercano);
        }
    }

    Transform obtenerPosEnemigoMasCercano(Collider[] listaChoques)
    {
        Transform enemigoMasCercano = null;
        float menorDistancia = Mathf.Infinity;

        // se comprueba y elige el enemigo con menor distancia
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
        }
        return enemigoMasCercano; // puede llegar a ser nulo si no hay nada al rededor, hay que
                                  // tenerlo en cuenta
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

            proyectil.GetComponent<Proyectil>().destruirProyectil();
            // destruye el proyectil en los segundos que tiene asignado
            lanzamientos--;
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
