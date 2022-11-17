using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBehaviour : MonoBehaviour
{
    //Liste possible des armes du guerrier
    public List<Weapon> possibleWeapon = new List<Weapon>();

    //Stats non visible du guerrier
    int pv = 20;
    public int peur;
    float timeLeftForAttack = 0;

    //Liste des ennemis aux alentours
    List<GameObject> listNearEnnemy = new List<GameObject>();

    //Ensemble des stats dans l'ordre : Vitesse - Dexterité - Agilité - Force - Endurance - Courage
    int[] warrioStats = new int[6];
    //Arme du guerrier
    Weapon weapon;
    //Bouclier ou non
    bool bouclier = false;
    int bouclierDurability = 3;
    bool lastHitBlocked = false;

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
        //Debug.Log("Stat du guerrier : " + warrioStats[0] + " " + warrioStats[1] + " " + warrioStats[2] + " " + warrioStats[3] + " " + warrioStats[4] + " " + warrioStats[5]);

        //Choix de l'arme du guerrier
        weapon = possibleWeapon[Random.Range(0, 4)];
        //Debug.Log("Arme du guerrier : " + weapon.name);

        //Choix du bouclier ou non
        int bouclierRng = Random.Range(0, 2);
        if (weapon.name != "Arc" && bouclierRng == 0)
            bouclier = true;
        //Debug.Log("Le guerrier a un bouclier : " + bouclier);

        //Debug.Log("Guerrier " + this.name + " de courage : " + warrioStats[5] + " posséde comme arme : " + weapon.name + " et a un bouclier : " + bouclier);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftForAttack -= Time.deltaTime;
        peur = FearModifier();
        if (peur > warrioStats[5])
            Fuite();
        else
            Attaque();
    }

    //Methode qui modifie la variable peur
    int FearModifier()
    {
        int peurCalcul = 0;
        //nombre d'ennemi autour du guerrier
        int nbEnnemyNearby = listNearEnnemy.Count;
        for(int i = 0; i < nbEnnemyNearby; i++)
        {
            WarriorBehaviour ennemyBehaviour = listNearEnnemy[i].GetComponent<WarriorBehaviour>();
            //L'ennemi a une arme efficace contre l'arme du guerrier
            if (ennemyBehaviour.weapon.name == weapon.weaponCounter)
                peurCalcul += 2;
            //L'ennemi a un bouclier
            if (ennemyBehaviour.bouclier == true && weapon.name != "Hache")
                peurCalcul += 2;
            if (bouclier && ennemyBehaviour.weapon.name == "Hache")
                peurCalcul += 3;
        }
        //Les pv du guerrier sont low
        if (pv < 5)
            peurCalcul += 4;
        else if (pv < 10)
            peurCalcul += 2;
        return nbEnnemyNearby + peurCalcul;
    }

    //methode qui simule le comportement du guerrier quand il fuit
    void Fuite()
    {
        //Penser a bloquer le guerrier dans la zone circulaire
    }

    //Methode qui simule le comportement du guerrier quand il attaque
    void Attaque()
    {    
        if (listNearEnnemy.Count != 0)
        {
            //Get nearbiest ennemy
            targetWarrior = NearbiestEnnemy();

            //Si l'ennemie est hors de portée, le guerrier se déplace vers lui
            if(Vector3.Distance(targetWarrior.transform.position, transform.position) > weapon.portee)
            {
                //Move to him
                var step = warrioStats[0] * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetWarrior.transform.position, step);
            }
            //Si il est à portée, il l'attaque
            else
            {
                //Si l'attaque du guerrier n'est pas en cooldown
                if (timeLeftForAttack <= 0)
                {
                    targetWarrior.GetComponent<WarriorBehaviour>().ReceptionDégat(weapon.degat);
                    timeLeftForAttack = weapon.coolDown - ((float)warrioStats[4] * 2 / 100) * weapon.coolDown;
                }
            }
        }
    }

    //Methode qui gére les dégat recus par le guerrier
    public void ReceptionDégat(int degatWeapon)
    {
        pv -= degatWeapon;
        if (pv <= 0)
        {
            for(int i = 0; i < listNearEnnemy.Count; i++)
            {
                listNearEnnemy[i].GetComponent<WarriorBehaviour>().ImDead(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void ImDead(GameObject ennemyDead)
    {
        listNearEnnemy.Remove(ennemyDead);
    }

    GameObject NearbiestEnnemy()
    {
        GameObject nearbiestEnnemy = listNearEnnemy[0];
        float lessDistance = Vector3.Distance(listNearEnnemy[0].transform.position, transform.position);
        for (int i = 1; i < listNearEnnemy.Count; i++)
        {
            float distWithEnnemy = Vector3.Distance(listNearEnnemy[i].transform.position, transform.position);
            if (distWithEnnemy < lessDistance)
            {
                lessDistance = distWithEnnemy;
                nearbiestEnnemy = listNearEnnemy[i];
            }
        }
        return nearbiestEnnemy;
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
    void AfficheListNearEnnemy()
    {
        for(int i = 0; i < listNearEnnemy.Count; i++)
            Debug.Log(listNearEnnemy[i].name);
    }

 
}
