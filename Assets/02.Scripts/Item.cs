using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PLAYER"))
        {
            Debug.Log("Eat Item!");
            Destroy(this.gameObject);
        }


    }

}
