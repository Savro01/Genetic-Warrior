using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorLoad : MonoBehaviour
{
    public float nbWarriorInit;

    // Start is called before the first frame update
    void Start()
    {
        generateGen1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateGen1()
    {
        float angleSpawn = 360 / nbWarriorInit;
        float dist = 100;
        for (int i = 0; i < nbWarriorInit; i++)
        {
            GameObject warrior = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            warrior.transform.parent = transform;
            warrior.name = warrior.name + i;
            warrior.transform.position = new Vector3(dist * Mathf.Cos(angleSpawn * i / (180f / Mathf.PI)), 1, dist * Mathf.Sin(angleSpawn * i / (180f / Mathf.PI)));
            warrior.AddComponent<WarriorBehaviour>();
        }
    }
}
