using UnityEngine;
//EN PRUEBA NO ES ORIGINAL
//using UnityEngine.UI;
//EN PRUEBA NO ES ORIGINAL

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;//la vida del enemigo
    public int currentHealth;//esta se va ir decrementando
    public float sinkSpeed = 2.5f;// la velocidad con la que se unda el enemigo en el suelo 
    public int scoreValue = 10;//los puntos que dan cuando el jugador los mate
    public AudioClip deathClip;// sonido cuando muere
    public Sprite icon;//variable con relacion a la imagen del enemigo en canvas 

    //EN PRUEBA NO ES ORIGINAL 
    //public Slider m_Slider;
    //public Image m_FillImage;
    //public Color m_FullHealtColor = Color.green;
    //public Color m_ZeroHealthColor = Color.red;
    //EN PRUEBA NO ES ORIGINAL 



    Animator anim;//referencia a los componenetes 
    AudioSource enemyAudio;
    ParticleSystem hitParticles;//sistema de particulas 
    CapsuleCollider capsuleCollider;//colisiones sobre el rando del enemigo 
    bool isDead;//esta muerto
    bool isSinking;//el enemigo tiene que undirse en el suelo

    //EN PRUEBA NO ES ORIGINAL
    //private float m_CurrentHealth;
    //private bool m_Dead;
    //EN PRUEBA NO ES ORIGINAL


    void Awake ()
    {//componentes inspector
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }

    // este metodo nos permite calcular la vida del personaje, de acuerdo al ataque que reciba

    public int GetHealthPercentage() {

        return 100 * currentHealth / startingHealth;// la vida actual divido la vida inicial 

    
    }

    //EN PRUEBA NO ES ORIGINAL
 //   private void OnEnable() { 
    
   // m_CurrentHealth = m_st
    //}
    //EN PRUEBA NO ES ORIGINAL


    void Update ()//
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);// vamos a undir al enemigo al suelo
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)//punto concreto donde se ha disparado al enemigo 
    {
        if(isDead)//el enemigo esta muerto 
            return;//salimos del metodo

        enemyAudio.Play ();//el sonido del enemigo a sufrido daño

        currentHealth -= amount;//decrementamos la vida , de la cantidad que recibimos de daño
            
        hitParticles.transform.position = hitPoint;//
        hitParticles.Play();//efecto de particulas, la bala en el enemigo

        if(currentHealth <= 0)//saber si el enemigo a muerto
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;//desatibamos su colider

        anim.SetTrigger ("Muerto");// se manda a llamara la animacion de ANIMACIONES 

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()//hacer que el enemigo empieze a desender
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;//acedemos a ala inteligencia artificial //el enemigo dejara de moverse
        GetComponent <Rigidbody> ().isKinematic = true;//cuando es verdadero es que nosotros controlamos por codigo
        isSinking = true;
        ScoreManager.score += scoreValue;//ACCEDE AL SCORE 
        Destroy (gameObject, 2f);//destruir el objeto que esta este script, destruimos al enemigo, dentro de 2 segundos 
    }
}
