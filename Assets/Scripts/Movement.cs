using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;

    private Rigidbody rb;
    [SerializeField] Camera playerCamera;

    private float xRotation = 0f;

    void Start()
    {
        // Obtém o Rigidbody anexado ao jogador e a câmera do jogador
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Lê a entrada do mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotação no eixo Y do corpo do personagem
        transform.Rotate(Vector3.up * mouseX);

        // Rotação no eixo X da câmera (movimento vertical)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita a rotação vertical para não girar 360 graus
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void FixedUpdate()
    {
        // Lê as entradas de movimento (WASD ou setas)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula a direção do movimento baseado na orientação da câmera
        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveDirection *= moveSpeed;

        // Move o personagem aplicando força ao Rigidbody
        Vector3 velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = velocity;
    }
}