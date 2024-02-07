using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objetoTextoVidas;
    [SerializeField] TextMeshProUGUI objetoTextoSegundos;
    [SerializeField] TextMeshProUGUI objetoTextoDinero;
    [SerializeField] TextMeshProUGUI objetoTextoGanar;
    string textoVidasOriginal;
    string textoSegundosOriginal;
    string textoDineroOriginal;
    string textoGanarOriginal;

    public GameObject panelGanar;
    public GameObject panelPerder;
    
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
        // Texto de las vidas
        textoVidasOriginal = objetoTextoVidas.text;
        textoSegundosOriginal = objetoTextoSegundos.text;
        textoDineroOriginal = objetoTextoDinero.text;
        textoGanarOriginal = objetoTextoGanar.text;
    }

    // Start is called before the first frame update
    void Start()
    {
        actualizarTextoVidas();
        actualizarTextoDinero();
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
        objetoTextoSegundos.text = textoSegundosOriginal + GameManager.Instance.tiempoRestante.ToString("F1") + "s";
    }

    public void actualizarTextoDinero()
    {
        objetoTextoDinero.text = textoDineroOriginal + GameManager.Instance.dinero + "$";
    }

    public void actualizarTextoGanar()
    {
        objetoTextoGanar.text = textoGanarOriginal + 
            (GameManager.Instance.enemigosMuertos * 2 + GameManager.Instance.dinero) + " puntos";
    }

    public void mostrarPanelGanar()
    {
        actualizarTextoGanar();
        panelGanar.SetActive(true);
    }

    public void mostrarPanelPerder()
    {
        panelPerder.SetActive(true);
    }
}
