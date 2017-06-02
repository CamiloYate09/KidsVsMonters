using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;// saber en todo momento la vida que tiene el jugador


    Animator anim;// referencia a la inpector


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)// cuando la vida del jugador sea menor que cero
        {
            anim.SetTrigger("GameOver");//sale la animacion de termino el juego
        }
    }
}
