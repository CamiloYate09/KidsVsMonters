using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;//MUY IMPORTANTE !!! EL TIEMPO QUE TIENE QUE PASAR PARA QUE EL ENEMIGO VUELVA ATACAR
    public int attackDamage = 5;//EL DAÑO QUE LE GENERA AL JUGADRO


    Animator anim;// ANIMACION DE CAMBIO DE ESTAR MOVIENDOSE A ESTAR QUIETO 
    GameObject player;//
    PlayerHealth playerHealth;//LLAMARA AL METODO 
    EnemyHealth enemyHealth;
    bool playerInRange;//SABER SI EL JUGADOR VA A ESTAR AL LADO
    float timer;//NOS PERMITE SABER EL TIEMPO DESDE QUE EL ENEMIGO ATACO


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)//
        //CUANDO EL JUGADOR ESTA CERCA DEL ENEMIGO 
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    // CUANDO EL JUGADOR YA NO ESTA CERCA EN EL RANGO DEL ENEMIGO
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;//INCREMENTAMOS 

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("JugadorMuerto");//HACE REFERENCIA A LA ANIMACION DEL ENEMIGO 
        }
    }


    void Attack ()
    {
        timer = 0f;//reinicializar el componenete de tiempo 

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
