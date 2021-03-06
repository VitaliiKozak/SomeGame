using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealeTargetSpell : Spell
{
    public float Range;
    public float HealeValue;

    public override void SpellCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(-1 * HealeValue);
            }
        }
        TimeToCast = TimeBetweenCast;
    }
}
