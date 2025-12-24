using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 120f;   // Sensibilidad del ratón
    public Transform playerBody;            // Se usa para girar el personaje en horizontal

    float xRotation = 0f;               // Guarda la rotación vertical acumulada de la cámara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;      // Bloquea el cursor en el centro de la pantalla.  Evita que el ratón salga de la ventana del juego
        Cursor.visible = false;                       // Oculta el cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;     // Lee el movimiento horizontal del ratón // Lo multiplica por la sensibilidad y por deltaTime para que sea suave
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (cámara)
        xRotation -= mouseY;                                             // Restamos el movimiento vertical del ratón. Esto hace que mover el ratón arriba mire hacia arriba
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);                   // Limita la rotación vertical. Evita que la cámara:dé la vuelta completa
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (personaje)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}