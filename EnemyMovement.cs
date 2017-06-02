using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;//obtener la referencia  de la posicion del juegador y de esta manera saber donde esta //persiguiendo
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;//componente de la inteligencia artificial 


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;//referencia del juegador (TAG == player)
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();//llamada de referencia del componenete 
    }


    void Update ()//movimiento del enemigo
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)//la vida de los dos elementos 
        {
            nav.SetDestination (player.position);//donde esta en ese mismo momento el jugador 
        }
        else
        {
           nav.enabled = false;//desactivamos el componenete de inteligencia artificial 
        }
    }
}
