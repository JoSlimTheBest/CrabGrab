using Game.Scripts.Core;
using Game.Scripts.Core.Player.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAtack : MonoBehaviour
{

    public GameObject trapEffect; // Ёффект, который будет воспроизводитьс€ при активации ловушки
    public void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.GetComponent<PlayerView>()!=null)
        {
            trapEffect.SetActive(true); // ¬оспроизводим эффект ловушки
            GameManager.Instance.SetInputEnabled(false);
            Debug.Log("Player hit by trap!");
        }
    }
}
