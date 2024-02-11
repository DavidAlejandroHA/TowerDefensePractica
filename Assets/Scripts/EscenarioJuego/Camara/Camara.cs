using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    static int mascaraSuelo = 1<<7;
    float velocidadScroll = 10f;

    public static float velocidadArrastre = 0.1f;
    private static Vector3 dragOrigen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlarZoomCamara();
        controlarPosCamara();
    }

    public void controlarPosCamara()
    {
        
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigen = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigen);
        Vector3 move = new Vector3(pos.x * velocidadArrastre, 0, pos.y * velocidadArrastre);
        transform.Translate(move, Space.World);
    }

    public static void colocarObjeto(GameObject obj, bool destruir, float cooldownDestruccion)
    {
        /* Lanzar un rayo, e instanciar un obstáculo en el punto donde se golpee */
        /* Hay que reconstruir la superficie, buscar un método que se encargue de ello */
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rayo, out hit, 10000f, mascaraSuelo))
        {
            GameObject objetoAColocarCopia =
                Instantiate(obj, hit.point, obj.transform.localRotation);
            objetoAColocarCopia.SetActive(true);
            if (destruir)
            {
                Destroy(objetoAColocarCopia, cooldownDestruccion);
            }
            GameManager.Instance.quitarDinero(obj.GetComponent<ParametrosObjetos>().precio);
        }
    }

    public static void colocarObjeto(GameObject obj)
    {
        colocarObjeto(obj, false, 0f);
    }

    public void controlarZoomCamara()
    {
        if (Camera.main.orthographic) /* Esto no hace falta pero igual lo pongo por si en el futuro me serviría para algo
                                                  y lo tengo como apunte */
        {
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * velocidadScroll;
        }
        else // A partir de aquí si porque este es el modo principal y actual de la cámara
        {
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * velocidadScroll;
            //Debug.Log(Camera.main.fieldOfView);
        }
    }
}
