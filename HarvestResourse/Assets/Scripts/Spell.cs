using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType {Heale, Damage};

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public string Name;

    public float Range;
    public float Damage;
    public float ManaCost;

    public float TimeToShoot;
    public float TimeToCast;
    public float TimeBetweenShoot;

    public SpellType Type;

    public Sprite Icon;
}
