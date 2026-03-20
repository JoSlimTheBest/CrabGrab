using Game.Scripts.Core;
using Game.Scripts.Core.Player.Controllers;
using UnityEngine;

public class ToggleCanvasOnEsc : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;

    private bool isOpen;

    public GameObject canvasio;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenUp();
           
           
        }
    }


    public void OpenUp()
    {
        if (canvasio.activeSelf == true)
        {
            return;
        }
        isOpen = !isOpen;

        if (canvasObject != null)
        {
            canvasObject.SetActive(isOpen);

            // ВАЖНО: инверсия
            GameManager.Instance.SetInputEnabled(!isOpen);
        }
    }
}