using UnityEngine;

public class EnemyManager : MonoBehaviour
{//ENERANDO ENEMIGOS
    public PlayerHealth playerHealth;//HACE REFERENCIA PARA CONTROLAR LA VIDA DEL JUGADOR
    public GameObject enemy;//
    public float spawnTime = 3f;//EL TIEMPO QUE TIENE QUE PASAR PARA CREAR UN NUEVO ENEMIGO EN EL MAPA
    public Transform[] spawnPoints;// ESTO ES UN VECTOR UN ARRAY !!!MUY IMPORTATNE //MULTIPLES DE DONDE QUEREMOS QUE APARESCA UN ENEMIGO CONCRETO, UN SOLO PUNTO


    void Start ()//
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);//LLAMAR A LA VARIABLES Y SEGUIRLA LLAMADA DESPUES DE 3 SEGUNDOS 
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)// COMPROBAR SI EL JUGADOR A MUERTO, CON LA VIDA DEL JUGADOR
        {
            return;//SALIMOS DE LA FUNCION
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);//ELEGIR ALEATORIAMENTE UNA POSICION DE TODOS LOS ELEMENTOS QUE TENGAMOS EN ESTE VECTOR
        //

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);// 
    }
}
