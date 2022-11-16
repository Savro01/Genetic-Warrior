using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorLoad : MonoBehaviour
{
    public float nbWarriorInit;
    public GameObject warriorPrefab;

    List<GameObject> listWarrior = new List<GameObject>();

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
        //Vector2 origin = new Vector2(0, 0); //Commenter cette ligne si génération circulaire
        float angleSpawn = 360 / nbWarriorInit; //Commenter cette ligne si génération random
        float dist = 100; //Commenter cette ligne si génération random
        for (int i = 0; i < nbWarriorInit; i++)
        {
            // Vector2 posWarriorAnnulus = RandomPointInAnnulus(origin, 5, 105); //Commenter cette ligne si génération circulaire
            GameObject warrior = Instantiate(warriorPrefab);
            warrior.transform.parent = transform;
            warrior.name = warrior.name + i;
            warrior.transform.position = new Vector3(dist * Mathf.Cos(angleSpawn * i / (180f / Mathf.PI)), 1, dist * Mathf.Sin(angleSpawn * i / (180f / Mathf.PI))); //Commenter cette ligne si génération random
           // warrior.transform.position = new Vector3(posWarriorAnnulus.x, 1, posWarriorAnnulus.y); //Commenter cette ligne si génération circulaire
            listWarrior.Add(warrior);
        }
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }

}
