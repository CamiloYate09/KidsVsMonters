
using UnityEngine;


// script para el movimiento de energia del enemigo 
public class UIDirectionControl : MonoBehaviour {

    public bool m_UseRelativeRotation = true;// variable si aplicamos la rotacion de la energia o no 

    private Quaternion m_RelativeRotation;

    private void Start() {

        m_RelativeRotation = transform.parent.localRotation;
    }

    private void Update() {

        if (m_UseRelativeRotation)
            transform.rotation = m_RelativeRotation;
    }


}
