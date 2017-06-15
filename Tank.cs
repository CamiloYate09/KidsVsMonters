using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    // variables publicas que se pueden cambiar desde el mismo editor de unity
    // clase para el movimiento del lider 
    public int m_PlayerNumber = 1;// el numero del jugador que controla este componente 
    public float m_Speed = 12f;//la velocidad, en unidades por segundos, donde el enemigo o jugador avanzara
    public float m_TurnSpeed = 180f;//EL NUMERO DE GRADOS QUE GIRA EL OBJETO //JUGADOR POR SEGUNDO
    public AudioSource m_MovementAudio;//REFERENCIA AL AUDIO // EL SONIDO DEL JUEGADOR
    public AudioClip m_EngineIdling;//EL SONIDO CUANDO EL JUGADOR ESTA PARADO
    public AudioClip m_EngineDriving;//EL SONIDO CUANDO EL JUGADOR ESTA CAMINADO
    public float m_PitchRange = 0.2f;//EL RANGO QUE VARIARA EL SONIDO DEL JUGADOR 

    private string m_MovementAxisName;//CADENA DE TEXTO, CONTIENE EL NOMBRE DEL EJE QUE CONTROLA 
    private string m_TurnAxisName;// NOMBRE DEL EJE PARA GIRAR EL ENEMIGO O JUGADOR 
    private Rigidbody m_Rigidbody;// PARA EL MOVIMIENTO DEL CUERPO
    private float m_MovementInputValue;// CONTIENEN EL VALOR DE ESE EJE MOVER // SOBRE LO QUE SE ESTE PULSANDO 
    private float m_TurnInputValue;// CONTIENEN EL VALOR DE ESE EJE GIRAR // SOBRE LO QUE SE ESTE PULSANDO
    // EN LAS DOS ANTERIORES VAN A TENER VALORES DE -1 HAS 1 DONDE SEA EL MOVIMIENTO Y 0 PARA CUANDO ESTE QUIETO
    private float m_OriginalPitch;// GUARDAR UNA COPIA DE SONIDO QUE TENGA EL SONIDO DEL JUGADOR 


    private void Awake()
    {
        //damos la referencia a los componetes 
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // este metodo se activa cuando se inicia la escena con el objeto
        m_Rigidbody.isKinematic = false;// activar su cuerpo fisicamente para su entorno, empieza a recibir ataques
        m_MovementInputValue = 0f;//valores de movimiento a cero, ya que no queremos que haiga ningun movimiento
        m_TurnInputValue = 0f;//valores de movimiento a cero, ya que no queremos que haiga ningun movimiento

    }
    private void OnDisable() {
        // este metodo se desactiva cuando se inicia la escena con el objeto

        m_Rigidbody.isKinematic = true;// desactivar su cuerpo fisicamente para su entorno, empieza a recibir ataques
    
    }

    private void Start() {
        //establecemos los controles de los jugadores en este caso dos
        // en edit//input
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;// guardamos el tono de sonido de los pasos del jugador 
    
    }

    private void Update() { 
    
    //guardar la entrada del jugador, entrada de teclado, el valor de lo que se este pulsando 
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    
    }

    private void EngineAudio() { 
    
    //saber que esta pulsando el jugador para generar el sonido

        //Abs==valor absoluto// si nos da el valor "m_MovementInputValue" negativo, lo convierte en positivo
        //verificamos tanto para el movimiento vomo el giro
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f) { 
        
        //no estoy pulsando ninguna tecla
            if (m_MovementAudio.clip == m_EngineDriving) {

                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange );
                m_MovementAudio.Play();
            }
           
        
        }
        else
        {

            //estamos pulsando alguna tecla (movimiento o giro)
            

                //seguir echando codigo minuto 25, 31 PENDIENTE
        }
    
    }



}
