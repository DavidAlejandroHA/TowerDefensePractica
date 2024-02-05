using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    bool modoColocarObjeto = false;
    public NavMeshSurface superficie;
    public GameObject muro;
    public GameObject torreta;
    public GameObject canion;
    GameObject objetoAColocarGlobal;

    // Imágenes
    public GameObject imagenMuro;
    public GameObject imagenTorreta;
    public GameObject imagenCanion;
    //public NavMeshAgent agente;

    // Colores
    Color rojo = new Color32(255, 50, 50, 255);
    Color blanco = new Color32(255, 255, 255, 255);
    Color gris = new Color32(150, 150, 150, 255);
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

    /*Cambia el color de las imágenes de los objetos según si se está usando para colocar un objeto y
     activa el modo de colocar objetos (mediante una asignación a un objeto compartido por toda la clase
    para posteriormente colocarlo (objetoAColocarGlobal) )*/
    void comprobarYColocar(KeyCode key, GameObject objetoAColocar)
    {
        if (Input.GetKeyDown(key))
        {
            modoColocarObjeto = true;

            cambiarColorImagen(imagenMuro, blanco);
            cambiarColorImagen(imagenTorreta, blanco);
            cambiarColorImagen(imagenCanion, blanco);

            if (objetoAColocar == muro)
            {
                objetoAColocarGlobal = muro;
                cambiarColorImagen(imagenMuro, rojo);
            } else if (objetoAColocar == torreta)
            {
                Debug.Log("T");
                objetoAColocarGlobal = torreta;
                cambiarColorImagen(imagenTorreta, rojo);
            } else if (objetoAColocar == canion)
            {
                objetoAColocarGlobal = canion;
                cambiarColorImagen(imagenCanion, rojo);
            }
        }
        /*if(key == KeyCode.Alpha2)
        {
            modoColocarObjeto = true;
            objetoAColocar = torreta;
            cambiarColorImagen(imagenTorreta, rojo);
            cambiarColorImagen(imagenMuro, blanco);
        }*/
    }

    void cambiarColorImagen(GameObject imagen, Color color)
    {
        imagen.GetComponent<RawImage>().color = color;
    }

    void comprobarClickDerechoReset()
    {
        if (Input.GetMouseButtonDown(1))
        {
            modoColocarObjeto = false;
            resetImagesColor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.getPartidaActiva()) // Si la partida no ha terminado todo sigue
        {
            controlarOpcion(imagenMuro, muro, KeyCode.Alpha1, 5f);
            controlarOpcion(imagenTorreta, torreta, KeyCode.Alpha2, 10f);
            controlarOpcion(imagenCanion, canion, KeyCode.Alpha3, 20f);

            comprobarClickDerechoReset();

            //------------------------------------------

            if (Input.GetMouseButtonDown(0) && modoColocarObjeto)
            {
                float tiempoCooldown = -1f;

                if (objetoAColocarGlobal.GetComponent<DestruibleCooldown>() != null &&
                    objetoAColocarGlobal.GetComponent<DestruibleCooldown>().cooldownDestruccion != -1f)
                    /* Si (por si acaso aunque nunca debería de ser nulo
                     * en realidad) el objeto no tiene el script
                     * de DestruibleCooldown yel objeto no tiene cooldown de
                     * destrucción asociado (-1 seg)*/
                {
                    Debug.Log("PRE-ENTRO");
                    tiempoCooldown = objetoAColocarGlobal.GetComponent<DestruibleCooldown>().cooldownDestruccion;
                    
                    Camara.colocarObjeto(objetoAColocarGlobal, true, tiempoCooldown);
                    Debug.Log("ENTRO");
                }
                else
                {
                    Camara.colocarObjeto(objetoAColocarGlobal);/* aquí entran en las torretas y los cañones
                                                                 * pero estas se destruyen despues
                                                                 * de disparar n veces*/
                }
            }
        }
    }

    // Maneja si se pulsa una tecla para usar esa opción de compra
    void controlarOpcion(GameObject imagen, GameObject objetoAColocar, KeyCode tecla, float puntos)
    {
        if (GameManager.Instance.puntos >= puntos) /* Si hay dinero disponible para realizar la compra de
                                                    dicha opción*/
        {
            if (Input.GetKeyDown(tecla))
            {
                comprobarYColocar(tecla, objetoAColocar);
            }
        }
        else
        {
            cambiarColorImagen(imagen, gris);
        }
    }

    void resetImagesColor()
    {
        cambiarColorImagen(imagenTorreta, blanco);
        cambiarColorImagen(imagenMuro, blanco);
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
