using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarriorLoad : MonoBehaviour
{
    public float nbWarriorInit;
    public GameObject warriorPrefab;

    public Canvas canvas;

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
            Time.timeScale = 0f;
            getSurvivingWarrior();
            croisement();
            generateNGen();
            nbGen--;
            Time.timeScale = timeScaleAccelerator;
        }
        if (this.transform.childCount == 1 && nbGen <= 0)
            AfficheLastSurvivor();
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
        GameObject survivor = transform.GetChild(0).gameObject;
        int[] statsWarrior = survivor.GetComponent<WarriorBehaviour>().warrioStats;
        Weapon weaponWarrior = survivor.GetComponent<WarriorBehaviour>().weapon;
        bool bouclierWarrior = survivor.GetComponent<WarriorBehaviour>().bouclier;

        GameObject image = canvas.transform.GetChild(0).gameObject;

        GameObject nameText = image.transform.GetChild(1).gameObject;
        nameText.GetComponent<TextMeshProUGUI>().text = "Le guerrier s'appelle " + survivor.name;

        GameObject weaponText = image.transform.GetChild(2).gameObject;
        if(weaponWarrior.name != "Arc")
            weaponText.GetComponent<TextMeshProUGUI>().text = "Il a comme arme une " + weaponWarrior.name;
        else
            weaponText.GetComponent<TextMeshProUGUI>().text = "Il a comme arme un " + weaponWarrior.name;


        GameObject statsText = image.transform.GetChild(3).gameObject;
        statsText.GetComponent<TextMeshProUGUI>().text = "Il a " + statsWarrior[0] + " de vitesse, " + statsWarrior[1] + " de dextérité, " + statsWarrior[2] + " d'agilité, " + statsWarrior[3] + " de force, " + statsWarrior[4] + " d'endurance et " + statsWarrior[5] + " de courage";

        GameObject bouclierText = image.transform.GetChild(4).gameObject;
        if(bouclierWarrior)
            bouclierText.GetComponent<TextMeshProUGUI>().text = "Il a un bouclier";
        else
            bouclierText.GetComponent<TextMeshProUGUI>().text = "Il n'a pas de bouclier";

        canvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }

}
