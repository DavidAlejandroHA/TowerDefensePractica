using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrasdeVida : MonoBehaviour
{
    public GameObject entidad;
    float vidaMax;
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
