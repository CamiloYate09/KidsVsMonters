using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

//clase para pausar el juego 
public class PauseManager : MonoBehaviour {


    public Slider MusicVolumeSlider;
    public Slider FXVolumeSlider;// slider de referncia del menu de pause 
    
    Canvas canvas;// variable privada 

    //metodo para cargar la configuracion de los slider 
    void LoadState() {

        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        FXVolumeSlider.value = PlayerPrefs.GetFloat("FXVolume, 1f");
    }
    

    //metodo para guardar valores del valor del slider del volumen y el FX

    void SaveState() {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);// el valor que queremos guardar de es variable
        PlayerPrefs.SetFloat("FXVolume", MusicVolumeSlider.value);// el valor que queremos guardar de es variable

    
    }
    void Start()
    {
        canvas = GetComponent<Canvas>();//obtenemos la referncia al segundo canvas que vamos a realizar 
        canvas.enabled = false;// desabilitamos el canvas
        LoadState();// va llamar al metodo con la imformacion que tengamos cargada en esta con el slider de volumen y efectos de sonido 
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))// cuando pulsamos la tecla escape llamamos a la funcion Pause
        {
            Pause();
        }
    }
    
    public void Pause()// la funcion llamado desde un boton 
    {
        canvas.enabled = !canvas.enabled;// va activar el canvas si esta desabilitado o habilitar si estuviera desabilitado 
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;//va a detener el juego si estuviera funcionando o del mismo sentido pero alcontrario
        if (!canvas.enabled)// cuando no esta activo el boton de canvas
        {

            SaveState();// guardamos el estado de volumen  con su pause 
        }
    
    }
    
    public void Quit()// metodo llamadado desde un boton 
    {

        // llamamos al metodo y su valor guardado

        SaveState();

        #if UNITY_EDITOR //
        EditorApplication.isPlaying = false;//termine la ejecucion del juego 
        #else 
        Application.Quit();
        #endif
    }
}