using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InteractableRaycast : MonoBehaviour
{
    public float interactDistance = 5f; // Distância máxima para interação
    public KeyCode interactKey = KeyCode.E; // Tecla para interagir
    public LayerMask interactableLayer; // Layer dos objetos Interactables

    [SerializeField] private TextMeshProUGUI interactText; // Referência ao objeto de texto da UI
    [SerializeField] private Camera playerCamera; // Referência à câmera do jogador
    [SerializeField] private LineRenderer lineRenderer; // Referência ao LineRenderer

    private void Update()
    {
        // Realiza o Raycast a partir do centro da câmera na direção em que ela está olhando
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Atualiza a posição inicial do LineRenderer para o início do Raycast (câmera)
        lineRenderer.SetPosition(0, ray.origin);

        // Se o Raycast colidir com um objeto na layer Interactable e dentro da distância
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Atualiza a posição final do LineRenderer para a posição de colisão
            lineRenderer.SetPosition(1, hit.point);

            // Verifica se o objeto colidido tem a tag "Interactable"
            if (hitObject.CompareTag("Interactable"))
            {
                // Habilita o texto de interação na UI
                interactText.gameObject.SetActive(true);

                // Verifica se a tecla de interação foi pressionada
                if (Input.GetKeyDown(interactKey))
                {
                    // Chama a função correspondente ao nome do objeto
                    InteractWithObject(hitObject.name);
                }
            }
            else
            {
                // Se colidir com algo que não é interagível, desativa o texto de interação
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Se o Raycast não colidir com nada, desativa o texto de interação e estende o LineRenderer para o máximo da distância
            lineRenderer.SetPosition(1, ray.origin + ray.direction * interactDistance);
            interactText.gameObject.SetActive(false);
        }
    }

    // Função que será chamada com base no nome do objeto interagível
    private void InteractWithObject(string objectName)
    {
        switch (objectName)
        {
            case "TEST_Interactable01":
                Debug.Log("Interagiu com o objeto 01");
                break;
            case "TEST_Interactable02":
                Debug.Log("Interagiu com o objeto 02");
                break;
            case "TEST_Interactable03":
                Debug.Log("Interagiu com o objeto 03");
                break;
            default:
                Debug.Log("Interagiu com um objeto desconhecido: " + objectName);
                break;
        }
    }
}
