using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Gamagora/Weapon")]
public class Weapon : ScriptableObject
{
    public string name;
    public float coolDown;
    public float degat;
    public int portee;
    public string weaponCounter;
}
