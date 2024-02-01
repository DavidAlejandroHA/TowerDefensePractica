using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VidasTexto : MonoBehaviour
{
    public static TextMeshProUGUI objetoTexto;
    // Start is called before the first frame update
    string textoOriginal;
    void Start()
    {
        objetoTexto = GetComponent<TextMeshProUGUI>();
        textoOriginal = objetoTexto.text;
        objetoTexto.text = textoOriginal + GameManager.Instance.vidas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
