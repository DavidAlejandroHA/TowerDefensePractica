using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VidasTexto : MonoBehaviour
{
    public static TextMeshProUGUI objetoTexto;
    // Start is called before the first frame update
    //string textoOriginal;
    private void Awake()
    {
        objetoTexto = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
