using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            GameManager.Instance.quitarVida();
            Destroy(other.gameObject);
        }
    }
}
