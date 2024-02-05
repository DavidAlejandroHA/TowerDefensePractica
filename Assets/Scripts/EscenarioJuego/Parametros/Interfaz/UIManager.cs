using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objetoTextoVidas;
    [SerializeField] TextMeshProUGUI objetoTextoSegundos;
    string textoVidasOriginal;
    string textoSegundosOriginal;
    
    public static UIManager Instance { get; private set; }

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
        // Texto de las vidas
        //objetoTextoVidas = GetComponent<TextMeshProUGUI>();
        textoVidasOriginal = objetoTextoVidas.text;
        textoSegundosOriginal = objetoTextoSegundos.text;
        actualizarTextoVidas();
        //actualizarTextoSegundos();
    }

    // Update is called once per frame
    void Update()
    {
        actualizarTextoSegundos();
    }

    public void actualizarTextoVidas()
    {
        objetoTextoVidas.text = textoVidasOriginal + GameManager.Instance.vidas;
    }

    public void actualizarTextoSegundos()
    {
        objetoTextoSegundos.text = textoSegundosOriginal + GameManager.Instance.tiempoRestante.ToString("F1");
    }
}
