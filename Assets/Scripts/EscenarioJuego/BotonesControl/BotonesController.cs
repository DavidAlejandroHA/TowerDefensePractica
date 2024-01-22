using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BotonesController : MonoBehaviour
{
    bool modoColocarObjeto = false;
    public NavMeshSurface superficie;
    public GameObject muro;
    public GameObject torreta;
    GameObject objetoAColocar;
    public GameObject imagenMuro;
    public GameObject imagenTorreta;
    //public NavMeshAgent agente;

    // Colores
    Color rojo = new Color32(255, 50, 50, 255);
    Color blanco = new Color32(255, 255, 255, 255);
    //GameObject imagenObjetoActual;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void comprobarTecla(KeyCode key)
    {//KeyCode.Alpha1
        /*if (Input.GetKeyDown(key) != modoColocarObjeto)
        { 
            modoColocarObjeto = true;
        }
        else
        {
            modoColocarObjeto = !modoColocarObjeto;
        }*/

        //if (modoColocarObjeto)

        if (key == KeyCode.Alpha1)
        {
            modoColocarObjeto = true;
            objetoAColocar = muro;
            imagenTorreta.GetComponent<RawImage>().color = blanco;
            //imagenObjetoActual = imagenMuro;
            imagenMuro.GetComponent<RawImage>().color = rojo;
        }
        if(key == KeyCode.Alpha2)
        {
            modoColocarObjeto = true;
            objetoAColocar = torreta;
            imagenTorreta.GetComponent<RawImage>().color = rojo;
            imagenMuro.GetComponent<RawImage>().color = blanco;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            comprobarTecla(KeyCode.Alpha1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            comprobarTecla(KeyCode.Alpha2);
        }
        //Debug.Log(modoColocarObjeto);
        if (Input.GetMouseButtonDown(1))
        {
            modoColocarObjeto = false;
            resetImagesColor();
        }

        if (Input.GetMouseButtonDown(0) && modoColocarObjeto)
        {
            /* Lanzar un rayo, e instanciar un obstáculo en el punto donde se golpee */
            /* Hay que reconstruir la superficie, buscar un método que se encargue de ello */
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit))
            {
                GameObject objetoAColocarCopia =
                    Instantiate(objetoAColocar, hit.point, objetoAColocar.transform.localRotation);
                Destroy(objetoAColocarCopia, 2f);
                //Invoke("actualizarPaths",2f);
            }
        }
    }

    void resetImagesColor()
    {
        imagenTorreta.GetComponent<RawImage>().color = blanco;
        imagenMuro.GetComponent<RawImage>().color = blanco;
    }

    void actualizarPaths()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject obj in enemigos)
        {
            obj.GetComponent<NavMeshAgent>().ResetPath();
        }
    }
}
