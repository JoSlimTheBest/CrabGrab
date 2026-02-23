using Game.Scripts.Core.Player.View;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerView>() != null)
        {
            SceneManager.LoadScene(2);
        }
    }
}
