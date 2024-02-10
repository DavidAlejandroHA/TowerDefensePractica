using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
        Debug.Log(Time.timeScale);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void salirJuego()
    {
        //Debug.Log("a");
        Application.Quit();
    }
}
