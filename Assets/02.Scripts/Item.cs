using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private MonsterCtrl monsterCtrl;

    private void Awake()
    {
        monsterCtrl = GetComponent <MonsterCtrl>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PLAYER"))
        {
            Debug.Log("Eat Item!");

            StartCoroutine(AttackItem());
            Destroy(this.gameObject);
        }

        IEnumerator AttackItem()
        {
            Debug.Log("AttackItem");
            monsterCtrl.damage = 50;
            yield return new WaitForSeconds(3f);

            monsterCtrl.damage = 10;
            StopAllCoroutines();
        }
    }

}
