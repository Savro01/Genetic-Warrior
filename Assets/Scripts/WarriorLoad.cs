using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorLoad : MonoBehaviour
{
    public float nbWarriorInit;
    public GameObject warriorPrefab;

    int nbGen = 100;

    List<(int[], Weapon, bool)> listWarriorNextFight = new List<(int[], Weapon, bool)>();

    float timeScaleAccelerator = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScaleAccelerator;
        generateGen1();
        nbGen--;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount <= 10 && nbGen > 0)
        {
            Debug.Log("Nous avons des gagnants");
            Time.timeScale = 0f;
            getSurvivingWarrior();
            croisement();
            generateNGen();
            nbGen--;
            Time.timeScale = timeScaleAccelerator;
        }
        if (this.transform.childCount == 1 && nbGen <= 0)
            Debug.Log("Recupere le dernier guerrier !");
    }

    //Generate the first generation of the program
    void generateGen1()
    {
        float angleSpawn = 360 / nbWarriorInit;
        float dist = 100; 
        for (int i = 0; i < nbWarriorInit; i++)
        {
            GameObject warrior = Instantiate(warriorPrefab);
            warrior.transform.parent = transform;
            warrior.name = warrior.name + i;
            warrior.transform.position = new Vector3(dist * Mathf.Cos(angleSpawn * i / (180f / Mathf.PI)), 1, dist * Mathf.Sin(angleSpawn * i / (180f / Mathf.PI)));
            warrior.GetComponent<WarriorBehaviour>().instantiateFirstGen();
        }
    }

    //Generate the n generation of the program
    void generateNGen()
    {
        float angleSpawn = 360 / listWarriorNextFight.Count; 
        float dist = 100;
        for (int i = 0; i < listWarriorNextFight.Count; i++)
        {
            GameObject warrior = Instantiate(warriorPrefab);
            warrior.transform.parent = transform;
            warrior.name = warrior.name + i;
            warrior.transform.position = new Vector3(dist * Mathf.Cos(angleSpawn * i / (180f / Mathf.PI)), 1, dist * Mathf.Sin(angleSpawn * i / (180f / Mathf.PI)));
            warrior.GetComponent<CapsuleCollider>().radius = 20;
           /* Debug.Log(listWarriorNextFight[i].Item1[0] + " " + listWarriorNextFight[i].Item1[1] + " " + listWarriorNextFight[i].Item1[2] + " " + listWarriorNextFight[i].Item1[3] + " " + listWarriorNextFight[i].Item1[4] + " " + listWarriorNextFight[i].Item1[5]);
            Debug.Log(listWarriorNextFight[i].Item2);
            Debug.Log(listWarriorNextFight[i].Item3);*/
            warrior.GetComponent<WarriorBehaviour>().instantiateNextGen(listWarriorNextFight[i].Item1, listWarriorNextFight[i].Item2, listWarriorNextFight[i].Item3);
        }
        EmptyList();
    }

    //Accouple la génération n de guerrier pour donner la génération n+1
    void croisement()
    {
        int nbChild = listWarriorNextFight.Count;
        for(int i = 0; i < nbChild; i+=2)
        {
            //Croisement des stats
            int randomStatBreak = Random.Range(1, 6);
            int[] statWarriorChild = new int[6];
            for(int s = 0; s < listWarriorNextFight[i].Item1.Length; s++)
            {
                if (s >= randomStatBreak)
                    statWarriorChild[s] = listWarriorNextFight[i + 1].Item1[s];
                else
                    statWarriorChild[s] = listWarriorNextFight[i].Item1[s];
            }

            //Choix de l'arme
            int randomWeapon = Random.Range(0, 2);
            Weapon weaponChild = listWarriorNextFight[i + randomWeapon].Item2;

            //Choix de bouclier ou non
            bool bouclierChild = false;
            if (weaponChild.name != "Arc")
            {
                int randomBouclier = Random.Range(0, 2);
                bouclierChild = listWarriorNextFight[i + randomBouclier].Item3;
            }

            //add le nouveau guerrier à la liste de la future gen
            listWarriorNextFight.Add((statWarriorChild, weaponChild, bouclierChild));
        }
    }

    //Récupere les guerriers de la génération qui vient de combattre et met leurs stats dans une liste
    void getSurvivingWarrior()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int[] statsWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().warrioStats;
            Weapon weaponWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().weapon;
            bool bouclierWarrior = transform.GetChild(i).GetComponent<WarriorBehaviour>().bouclier;
            listWarriorNextFight.Add((statsWarrior, weaponWarrior, bouclierWarrior));
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void EmptyList()
    {
        int listSize = listWarriorNextFight.Count;
        for (int i = 0; i < listSize; i++)
        {
            listWarriorNextFight.RemoveAt(0);
        }
    }

    void AfficheLastSurvivor()
    {
        int width = Screen.width;
        int height = Screen.height;

    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }

}
