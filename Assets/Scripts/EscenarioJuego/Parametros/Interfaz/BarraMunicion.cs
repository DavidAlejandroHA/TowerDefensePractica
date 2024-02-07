using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraMunicion : MonoBehaviour
{
    public GameObject entidad;
    float municionMax;
    /*[SerializeField] private */Slider slider;

    public void actualizarBarraDeMunicion(float municionActual, float municionMax)
    {
        slider = this.GetComponent<Slider>();
        slider.value = municionActual / municionMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        municionMax = entidad.GetComponent<TorretaLanzarProyectiles>().lanzamientos;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Camera.main.transform.rotation;
        transform.rotation = lookRotation;
    }
}
