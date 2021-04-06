using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    public Sprite Icon;
    public string Name;
    public float TimeBetweenCast;
    public float TimeToCast=0;

    public bool ReadyToCast => TimeToCast <= 0;
    public float ProgresToCast => Mathf.Clamp(1 - TimeToCast / TimeBetweenCast, 0, 1);

    public virtual void SpellCast()
    {
        throw new Exception("Dont have overide spell cast");
    }

    public void UpdateTime(float value)
    {
        TimeToCast -= value;
       // Debug.Log($"{Name} left time to shoot ={TimeToCast}");
    }
}
