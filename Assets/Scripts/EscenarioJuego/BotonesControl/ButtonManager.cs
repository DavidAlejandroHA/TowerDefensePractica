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
    void comprobarYColocar(KeyCode key, GameObject objetoAColocar, float precio)
    {
        if (Input.GetKeyDown(key))
        {
            cambiarColorImagen(imagenMuro, blanco, true);
            cambiarColorImagen(imagenTorreta, blanco, true);
            cambiarColorImagen(imagenCanion, blanco, true);

            if (objetoAColocar == muro)
            {
                objetoAColocarGlobal = muro;
                cambiarColorImagen(imagenMuro, rojo, true);
            } else if (objetoAColocar == torreta)
            {
                objetoAColocarGlobal = torreta;
                cambiarColorImagen(imagenTorreta, rojo, true);
            } else if (objetoAColocar == canion)
            {
                objetoAColocarGlobal = canion;
                cambiarColorImagen(imagenCanion, rojo, true);
            }

            checkEnoughMoney(precio);
        }
        /*if(key == KeyCode.Alpha2)
        {
            modoColocarObjeto = true;
            objetoAColocar = torreta;
            cambiarColorImagen(imagenTorreta, rojo);
            cambiarColorImagen(imagenMuro, blanco);
        }*/
    }

    public void checkEnoughMoney(float precio)
    {
        if (GameManager.Instance.dinero >= precio)
        {
            modoColocarObjeto = true;
        }
        else
        {
            modoColocarObjeto = false;
        }
    }

    void cambiarColorImagen(GameObject imagen, Color color, bool sobreescribirImgSelec)
    {
        if (sobreescribirImgSelec)
        {
            imagen.GetComponent<RawImage>().color = color;
        }
        else
        {
            if (imagen.GetComponent<RawImage>().color != rojo)
            {
                imagen.GetComponent<RawImage>().color = color;
            }
        }
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
            controlarOpcion(imagenMuro, muro, KeyCode.Alpha1, muro.GetComponent<ParametrosObjetos>().precio);
            controlarOpcion(imagenTorreta, torreta, KeyCode.Alpha2, torreta.GetComponent<ParametrosObjetos>().precio);
            controlarOpcion(imagenCanion, canion, KeyCode.Alpha3, canion.GetComponent<ParametrosObjetos>().precio);

            comprobarClickDerechoReset();

            //------------------------------------------

            if (Input.GetMouseButtonDown(0) && modoColocarObjeto)
            {
                float tiempoCooldown = -1f;

                if (objetoAColocarGlobal.GetComponent<ParametrosObjetos>() != null &&
                    objetoAColocarGlobal.GetComponent<ParametrosObjetos>().cooldownDestruccion != -1f)
                    /* Si (por si acaso aunque nunca debería de ser nulo
                     * en realidad) el objeto no tiene el script
                     * de DestruibleCooldown yel objeto no tiene cooldown de
                     * destrucción asociado (-1 seg)*/
                {
                    tiempoCooldown = objetoAColocarGlobal.GetComponent<ParametrosObjetos>().cooldownDestruccion;
                    
                    Camara.colocarObjeto(objetoAColocarGlobal, true, tiempoCooldown);
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
    void controlarOpcion(GameObject imagen, GameObject objetoAColocar, KeyCode tecla, float precio)
    {
        if (GameManager.Instance.dinero >= precio) /* Si hay dinero disponible para realizar la compra de
                                                    dicha opción*/
        {
            if (Input.GetKeyDown(tecla))
            {
                comprobarYColocar(tecla, objetoAColocar, precio);
            }
        }
        else
        {
            cambiarColorImagen(imagen, gris, true);
        }
    }

    public void resetImagesColor()
    {
        if (modoColocarObjeto)
        {
            
            cambiarColorImagen(imagenTorreta, blanco, false);
            cambiarColorImagen(imagenMuro, blanco, false);
            cambiarColorImagen(imagenCanion, blanco, false);
        }
        else
        {
            cambiarColorImagen(imagenTorreta, blanco, true);
            cambiarColorImagen(imagenMuro, blanco, true);
            cambiarColorImagen(imagenCanion, blanco, true);
        }
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
