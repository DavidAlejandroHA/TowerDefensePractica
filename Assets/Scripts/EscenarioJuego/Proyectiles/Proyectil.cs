using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float duracionVidaProyectil;
    public float velocidadMovimiento;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destruirProyectil()
    {
        Destroy(this, duracionVidaProyectil);
        // destruye el proyectil en los segundos que tiene asignado
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            // hacer daño al enemigo
            // usar el damage
        }
        Destroy(this.gameObject);
    }
}
