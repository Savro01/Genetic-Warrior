using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBehaviour : MonoBehaviour
{

    int pv = 10;
    int maxStatPoint = 50;

    List<GameObject> listEnnemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        getEnnemyList();
        Debug.Log(listEnnemy[Random.Range(0,100)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getEnnemyList()
    {
        foreach (Transform child in transform.parent.transform)
        {
            listEnnemy.Add(child.gameObject);
        }
    }
}
