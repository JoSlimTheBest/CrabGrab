using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObjStart : MonoBehaviour
{
    public float startCloseTime = 0.001f;
    void Start()
    {
        Invoke("Close", startCloseTime);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

   
}
