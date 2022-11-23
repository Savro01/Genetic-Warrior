using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorLoad : MonoBehaviour
{
    public float nbWarriorInit;
    public GameObject warriorPrefab;

    int nbGen = 100;

    List<GameObject> listWarriorPostFight = new List<GameObject>();
    List<GameObject> listWarriorGenSuiv = new List<GameObject>();
    List<(int[], Weapon, bool)> listWarriorStatPostFight = new List<(int[], Weapon, bool)>();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 10f;
        generateGen1();
        nbGen--;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount == 10 && nbGen > 0)
        {
            Debug.Log("Nous avons des gagnants");
            Time.timeScale = 0f;
            getAllList();
            croisement();
            generateNGen();
            nbGen--;
           // Time.timeScale = 10f;
        }
        if (this.transform.childCount == 1 && nbGen <= 0)
            Debug.Log("Recupere le dernier guerrier !");
    }

    //Generate the first generation of the program
    void generateGen1()
    {
        //Vector2 origin = new Vector2(0, 0); //Commenter cette ligne si g�n�ration circulaire
        float angleSpawn = 360 / nbWarriorInit; //Commenter cette ligne si g�n�ration random
        float dist = 100; //Commenter cette ligne si g�n�ration random
        for (int i = 0; i < nbWarriorInit; i++)
        {
            // Vector2 posWarriorAnnulus = RandomPointInAnnulus(origin, 5, 105); //Commenter cette ligne si g�n�ration circulaire
            GameObject warrior = Instantiate(warriorPrefab);
            warrior.transform.parent = transform;
            warrior.name = warrior.name + i;
            warrior.transform.position = new Vector3(dist * Mathf.Cos(angleSpawn * i / (180f / Mathf.PI)), 1, dist * Mathf.Sin(angleSpawn * i / (180f / Mathf.PI))); //Commenter cette ligne si g�n�ration random
           // warrior.transform.position = new Vector3(posWarriorAnnulus.x, 1, posWarriorAnnulus.y); //Commenter cette ligne si g�n�ration circulaire
        }
    }

    //Generate the n generation of the program
    void generateNGen()
    {

    }

    //Accouple la génération n de guerrier pour donner la génération n+1
    void croisement()
    {
        

    }

    //Récupere les guerriers de la génération qui vient de combattre et met leurs stats dans une liste
    void getAllList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int[] statsWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().warrioStats;
            Weapon weaponWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().weapon;
            bool bouclierWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().bouclier;
            listWarriorStatPostFight.Add((statsWarrior, weaponWarrior, bouclierWarrior));
            Destroy(transform.GetChild(i).gameObject);
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
