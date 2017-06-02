using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;// variable de velocidad de movimiento 6 unidades por segundo 


    Vector3 movement; // define el movimiento hacia donde queremos avanzar== variable privada

    Animator anim; // variables del jugador en inspector 
    Rigidbody playerRigibody;// variables del jugador en inspector 

    int floorMask;
    float camRayLength = 100f; // la longuitud del rayo , que tenemos que lanzar para ver si colisiona con el suelo




    void Awake() { // refernecias del mismo componente

        anim = GetComponent<Animator>();
        playerRigibody = GetComponent<Rigidbody>();

        floorMask = LayerMask.GetMask("Floor");// de herarquia se manda a llamar al elemento suelo o floor 


    
    
    }

    void FixedUpdate() // se ejecuta para el motor de fisica
    {
        //aqui va todo lo relacionado con la fisica del objeto

        //controles de teclas del jugador
        //aqui sabemos si el personaje se esta moviendo o esta quieto 
        float h = Input.GetAxisRaw("Horizontal");// movimiento en la horizontal y que me devuelva -1 si se pulsa hacia abajo, 0 si no se esta pulsando, 1 uno sii se esta pulsando
        float v = Input.GetAxisRaw("Vertical"); // de igual manera a la descripcion de arrba

        //llamamos los metodos
        Move(h, v);
        Turning();
        Animating(h, v);


    
    
    }

    void Move(float h, float v) {
        //llamando al metodo anterior con los valores que nos dan en el movimiento
        movement.Set(h, 0f, v); //se definen los tres puntos X Y Z 
        movement = movement.normalized * speed * Time.deltaTime; // siempre va tener una longuitud de uno //vector 
        playerRigibody.MovePosition(transform.position + movement);//tenemos al personaje movimiendose


    
    }


    void Turning() //el giro del objeto con el mouseç
    { 
    // nos crea un rayo de referencia , que nos nosotros indiquemos con el mouse
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit; //floorHIt
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//devuelve  verdadero si ha colisionado con algo el rayo
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;// para que nuestro jugador no mire hacia abajo en el eje Y


            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // define una rotacion// mira algo concreto
            playerRigibody.MoveRotation(newRotation);

        }
    }


    void Animating(float h, float v) {

        bool Walking = !((v == 0) && (h == 0)); //cuando el jugador esta quieto vale cero
        anim.SetBool("EstaCaminado", Walking);
    }

}
