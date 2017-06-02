using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour

    //CLASE PARA LA PUNTUACION DEL JUEGO 
    
{
    public static int score;//ACEDER DE FORMA GROBAL


    Text text;//


    void Awake ()
    {
        text = GetComponent <Text> ();//LLAMAMOS AL COMPONENTE
        score = 0;// LA INICIALIZAMOS EN 0
    }


    void Update ()
    {
        text.text = "Score: " + score;// DE RESULTADO LA PUNTUACION 
    }
}
