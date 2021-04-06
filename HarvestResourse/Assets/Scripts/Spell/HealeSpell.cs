using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealeSpell : Spell
{
    public float Value;

    public override void SpellCast()
    {
        Debug.Log($"You heale by {Value}♥");
        
        TimeToCast = TimeBetweenCast;
    }
}
