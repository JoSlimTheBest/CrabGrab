using Game.Scripts.Core;
using Game.Scripts.Core.Player.Controllers;
using UnityEngine;

public class ToggleCanvasOnEsc : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;

    private bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenUp();
           
           
        }
    }


    public void OpenUp()
    {
        isOpen = !isOpen;

        if (canvasObject != null)
        {
            canvasObject.SetActive(isOpen);

            // ВАЖНО: инверсия
            GameManager.Instance.SetInputEnabled(!isOpen);
        }
    }
}