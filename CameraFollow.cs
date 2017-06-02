using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;//el objeto que queremos que la camara siga
    public float smoothing = 5f;//indicar el nivel o la suavidad que valla siguiendo al personaje
    Vector3 offset;  // esta variable tendra el vector que separa al persona de la camara


    //metodo
    void Start() {

        offset = transform.position - target.position;

    
    }

    void FixedUpdate() {

        Vector3 targetCamPos = target.position + offset; /// variable que va a indicar el punto de destino donde queremos poner la camara

        //iremos avanzando los fotoramas de poco a poco 
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);//actualizar la posicion de la camara // Lerp() interpolacion 
    }
}
