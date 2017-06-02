using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{

    // variables de la salud o vida del jugador 
    public int startingHealth = 100;// la vida inicial del del jugador en la escena
    public int currentHealth;//mantener la vida, decrementandose cada vez que el jugador reciba daño 
    public Slider healthSlider;//decrementando el valor de energia del jugador
    public Image damageImage;//pintaba toda la pantalla de un color 
    public AudioClip deathClip;//va a almacenar el sonido, cuando el personaje muere
    public float flashSpeed = 5f;// la velocidad del color que pinta toda la pantalla a hacerse transparente 
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);//el color con el cual se pinta la pantalla cuando el jugador 


    Animator anim;//cuando la vida del personaje valga cero, aparece la animacion del personaje muerto
    AudioSource playerAudio;//componente de audio del jugador
    PlayerMovement playerMovement;//al c# del movimiento 
    PlayerShooting playerShooting;//ya que el jugar muera, no queremos que dispare 
    bool isDead;//esta muerto
    bool damaged;//representar va valer verdadero, cada vez que un enemigo le haga daño a un personaje 


    void Awake ()//inicializar componenetes 
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;//salud actual a 100
    }


    void Update ()// este componente hace la animacion de volver trasparente la pantalla 
    {
        if(damaged)
        {
            damageImage.color = flashColour;//establecer el color de la imagen 
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)//hacer daño al personaje
    {
        damaged = true;// cada vez que el jugador reciba un ataque esta variable se va a volver verdadero

        currentHealth -= amount;//decrementar la salud actual de acuerdo a la condicion int amount 

        healthSlider.value = currentHealth;// actualizaremos el valor del slider 

        playerAudio.Play ();//el audio se reproduce que tiene el jugador

        if(currentHealth <= 0 && !isDead)//comprobar si ha muerto el jugador
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;//saber si el jugador ha muerto

        playerShooting.DisableEffects ();//que desabilite con efectos del disparo

        anim.SetTrigger ("Muere");// la animacion de muerte

        playerAudio.clip = deathClip;//actualizar el sonido 
        playerAudio.Play ();

        playerMovement.enabled = false;//desabilitamos al jugador
        playerShooting.enabled = false;//
    }

    public void ScheduleRestarLevel() {

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel() {
        yield return new WaitForSeconds(4);
        Application.LoadLevel(Application.loadedLevel);

    
    }

    //public void RestartLevel ()
    //{
      //  SceneManager.LoadScene (0);
    //}
}
