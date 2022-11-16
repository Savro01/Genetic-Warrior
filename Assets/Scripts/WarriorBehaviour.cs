using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBehaviour : MonoBehaviour
{
    //Liste possible des armes du guerrier
    public List<Weapon> possibleWeapon = new List<Weapon>();

    //Stats non visible du guerrier
    int pv = 20;
    int peur = 0;
    float timeLeftForAttack = 0;

    //Liste des ennemis aux alentours
    List<GameObject> listNearEnnemy = new List<GameObject>();

    //Ensemble des stats dans l'ordre : Vitesse - Dexterité - Agilité - Force - Endurance - Courage
    int[] warrioStats = new int[6];
    //Arme du guerrier
    Weapon weapon;
    //Bouclier ou non
    bool bouclier = false;

    //Cible courante du guerrier
    GameObject targetWarrior;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation des stats du guerrier
        for (int i = 0; i < 5; i++)
        {
            warrioStats[i] += 10;
            int rng = Random.Range(0, 10);
            warrioStats[i] -= rng;
            int rngGiveStat = Random.Range(0, 5);
            warrioStats[rngGiveStat] += rng;
        }

        int courageRng = Random.Range(1, 21);
        warrioStats[5] = courageRng;
        Debug.Log("Stat du guerrier : " + warrioStats[0] + " " + warrioStats[1] + " " + warrioStats[2] + " " + warrioStats[3] + " " + warrioStats[4] + " " + warrioStats[5]);

        //Choix de l'arme du guerrier
        weapon = possibleWeapon[Random.Range(0, 4)];
        Debug.Log("Arme du guerrier : " + weapon.name);

        //Choix du bouclier ou non
        int bouclierRng = Random.Range(0, 2);
        if (weapon.name != "Arc" && bouclierRng == 0)
            bouclier = true;
        //Debug.Log("Le guerrier a un bouclier : " + bouclier);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftForAttack -= Time.deltaTime;
        peur = FearModifier();
        if(peur > warrioStats[5])
            Fuite();
        else
            Attaque();
    }

    //Methode qui modifie la variable peur
    int FearModifier()
    {
        int peurBouclier = 0;
        int peurWeaponWeak = 0;
        int pvLow = 0;
        //nombre d'ennemi autour du guerrier
        int nbEnnemyNearby = listNearEnnemy.Count;
        for(int i = 0; i < nbEnnemyNearby; i++)
        {
            //L'ennemi a une arme efficace contre l'arme du guerrier
            WarriorBehaviour ennemyBehaviour = listNearEnnemy[i].GetComponent<WarriorBehaviour>();
           /* switch(weapon){
                case "Epee":
                    break;
                case "Hache":
                    break;
                case "Arc":
                    break;
                case "Lance":
                    break;
            }*/
            //L'ennemi a un bouclier
            if (ennemyBehaviour.bouclier == true)
                peurBouclier = 2;

        }
        return 1;
    }

    //methode qui simule le comportement du guerrier quand il fuit
    void Fuite()
    {
        //Penser a bloquer le guerrier dans la zone circulaire
    }

    //Methode qui simule le comportement du guerrier quand il attaque
    void Attaque()
    {
        //Si ennemie à portée
        if(timeLeftForAttack <= 0)
        {
            //Attaque
            timeLeftForAttack = weapon.coolDown - ((float)warrioStats[4]*2 / 100) * weapon.coolDown;
            Debug.Log(timeLeftForAttack);
        }
    }

    //Methode qui gére les dégat recus par le guerrier
    void ReceptionDégat(int degatWeapon)
    {
        if (pv <= 0)
            Destroy(this);
    }

    //Si collision avec un autre GameObject, l'ajoute à la liste des ennemis proches
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "warrior")
            listNearEnnemy.Add(other.gameObject);
    }

    //Si fin de collision avec un autre GameObject, le retire de la liste des ennemis proches
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "warrior")
            listNearEnnemy.Remove(other.gameObject);
    }

    //Affiche la liste des ennemis proche (par leur nom)
    void afficheListNearEnnemy()
    {
        for(int i = 0; i < listNearEnnemy.Count; i++)
            Debug.Log(listNearEnnemy[i].name);
    }

 
}
