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
        if (Spell.ProgresToCast >= 0)
        {
            Splash.fillAmount = Spell.ProgresToCast;
            Splash.gameObject.SetActive(!(Splash.fillAmount >= 1));
        }
    }

    private void Start()
    {
        Splash.fillAmount = 0;
        Icon.sprite = Spell.Icon;
    }
}
