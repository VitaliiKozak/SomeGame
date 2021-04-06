using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItem : MonoBehaviour
{
    public Image Icon;
    public Image Splash;
    public Spell Spell;

    public void Update()
    {
        if (Spell.TimeToShoot >= 0)
        {
            Spell.TimeToShoot -= Time.deltaTime;
            Splash.fillAmount = Mathf.Clamp(1 - Spell.TimeToShoot / Spell.TimeBetweenShoot, 0, 1);
            Splash.gameObject.SetActive(!(Splash.fillAmount >= 1));
        }
    }

    private void Start()
    {
        Splash.fillAmount = 0;
        Icon.sprite = Spell.Icon;
        Spell.TimeToShoot = 0;
    }
}
