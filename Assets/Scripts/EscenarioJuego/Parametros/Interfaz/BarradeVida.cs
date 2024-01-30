using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarradeVida : MonoBehaviour
{
    public GameObject entidad;
    float vidaMax;
    [SerializeField] private Slider slider;

    public void actualizarBarraDeVida(float vidaActual, float vidaMax)
    {
        slider.value = vidaActual / vidaMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        vidaMax = entidad.GetComponent<EnemigoIA>().getVidaMax();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Camera.main.transform.rotation;
        transform.rotation = lookRotation;
    }
}
