using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public string message;
    public GameObject toTurnObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LevelText(message);
            if (toTurnObject != null)
            {
                toTurnObject.SetActive(true);

            }
            this.gameObject.SetActive(false);
        }
    }
}
