using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    static int mascaraSuelo = 1<<7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

    public static void colocarObjeto(GameObject obj)
    {
        colocarObjeto(obj, false, 0f);
    }
}
