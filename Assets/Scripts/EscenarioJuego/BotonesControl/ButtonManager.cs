using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ButtonManager : MonoBehaviour
{
    bool modoColocarObjeto = false;
    bool modoDestruirCooldownObjeto = false;
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

    public static ButtonManager Instance { get; private set; }

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
            modoDestruirCooldownObjeto = true;
            objetoAColocar = muro;
            imagenTorreta.GetComponent<RawImage>().color = blanco;
            //imagenObjetoActual = imagenMuro;
            imagenMuro.GetComponent<RawImage>().color = rojo;
        }
        if(key == KeyCode.Alpha2)
        {
            modoColocarObjeto = true;
            modoDestruirCooldownObjeto = false;
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
            if (modoDestruirCooldownObjeto)
            {
                float tiempoCooldown = 0f;
                if (objetoAColocar.GetComponent<Muro>() != null)
                {
                    tiempoCooldown = objetoAColocar.GetComponent<Muro>().cooldownDestruccion;
                }
                Camara.colocarObjeto(objetoAColocar, true, tiempoCooldown);
            } else
            {
                Camara.colocarObjeto(objetoAColocar); // aquí entra en las torretas pero estas se destruyen despues
                // de disparar n veces
            }
        }
    }

    void resetImagesColor()
    {
        imagenTorreta.GetComponent<RawImage>().color = blanco;
        imagenMuro.GetComponent<RawImage>().color = blanco;
    }

    /*void actualizarPaths()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject obj in enemigos)
        {
            obj.GetComponent<NavMeshAgent>().ResetPath();
        }
    }*/
}
