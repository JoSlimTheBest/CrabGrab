using UnityEngine;
using UnityEngine.UI;

public class KillButtons : MonoBehaviour
{
    [Header("UI Images")]
    [SerializeField] private Image imageW;
    [SerializeField] private Image imageA;
    [SerializeField] private Image imageS;
    [SerializeField] private Image imageD;

    [Header("Lit Sprites (что поставить после нажатия)")]
    [SerializeField] private Sprite litW;
    [SerializeField] private Sprite litA;
    [SerializeField] private Sprite litS;
    [SerializeField] private Sprite litD;

    private bool usedW;
    private bool usedA;
    private bool usedS;
    private bool usedD;

    private bool finished;

    private void Update()
    {
        if (finished)
            return;

        if (Input.GetKeyDown(KeyCode.W) && !usedW)
        {
            imageW.sprite = litW;
            usedW = true;
            CheckCompletion();
        }

        if (Input.GetKeyDown(KeyCode.A) && !usedA)
        {
            imageA.sprite = litA;
            usedA = true;
            CheckCompletion();
        }

        if (Input.GetKeyDown(KeyCode.S) && !usedS)
        {
            imageS.sprite = litS;
            usedS = true;
            CheckCompletion();
        }

        if (Input.GetKeyDown(KeyCode.D) && !usedD)
        {
            imageD.sprite = litD;
            usedD = true;
            CheckCompletion();
        }
    }

    private void CheckCompletion()
    {
        if (usedW && usedA && usedS && usedD)
        {
            finished = true;
            Invoke(nameof(DestroyObj), 0.5f);
        }
    }

    private void DestroyObj()
    {
        CanvasGroupFader fader = GetComponent<CanvasGroupFader>();
        if (fader != null)
            fader.FadeOut();

        Invoke(nameof(DestroyObjects), 3f);
    }

    private void DestroyObjects()
    {
        Destroy(gameObject);
    }
}