using UnityEngine;

public class CameraThird : MonoBehaviour
{
    float rotationSpeed = 1.0f;    // Velocidad a la que se mueve la cámara según el ratón
    public Transform Target, Player;
    float mouseX, mouseY;

    public Transform Obstruction;
    float zoomSpeed = 2f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Obstruction = Target;

        Cursor.visible = false;    // Oculta el cursor del ratón durante el juego
        Cursor.lockState = CursorLockMode.Locked;   // Bloquea el cursor en el centro de la pantalla para que no salga de la ventana del juego

    }

    // Update is called once per frame
    void Update()
    {
        CamControl();
        ViewObstructed();
    }

    void CamControl()
    {
        // Suma el movimiento horizontal del ratón a mouseX
        // Input.GetAxis("Mouse X") : movimiento horizontal del ratón
        // Se multiplica por rotationSpeed para controlar sensibilidad
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        
        // Resta el movimiento vertical del ratón a mouseY
        // Invertido porque mover el ratón hacia arriba debería mirar hacia arriba
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -25, 20);     // Limita la rotación vertical para que no gire completamente

        transform.LookAt(Target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);

        }

    }

    void ViewObstructed() {
        RaycastHit hit;   // // variable para almacenar información sobre el objeto con el que el rayo colisiona

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f)) // Lanzamos un rayo desde la posición de la cámara hacia el Target, con longitud máxima 4.5 unidades
        {                                                                                             // Si el rayo golpea algo, la información se guarda en 'hit' y entra en el if
            if (hit.collider.gameObject.tag != "Player")  // Si el objeto golpeado no no tiene tag "Player"
            {
                Obstruction = hit.transform;    //  objeto que está bloqueando la vista se guarda en en 'Obstruction'
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;   // Accedemos al MeshRenderer del objeto y hacemos que solo proyecte sombras (oculta objetos pero mantiene sombra)
                /*
                if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)    // Si la distancia entre la cámara y el objeto bloqueador es mayor o igual a 3 // y la distancia entre la cámara y el Target es mayor o igual a 1.5
                                                                                                                                                          // entonces movemos la cámara hacia adelante (acercándola al Target)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);

                }
                */
            }
            else {
                // Si el objeto golpeado es el jugador (tag "Player")Restauramos las sombras del objeto, ya que no bloquea la vista

                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                /*
                if (Vector3.Distance(Obstruction.position, Target.position) < 4.5f) 
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);

                }
                */
            }
        
        }
    
    }
}
