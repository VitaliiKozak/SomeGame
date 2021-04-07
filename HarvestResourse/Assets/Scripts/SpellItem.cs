using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItem : MonoBehaviour
{
    public Image Icon;
    public Image Splash;
    public Image Border;
    public Spell Spell;

    public void Update()
    {
        if (Spell.ProgresToCast >= 0)
        {
            Splash.fillAmount = Spell.ProgresToCast;
            Splash.gameObject.SetActive(!(Splash.fillAmount >= 1));
        }
    }

    public void SetBorderVisible(bool status)
    {
        Border.gameObject.SetActive(status);
    }

    private void Start()
    {
        Splash.fillAmount = 0;
        Icon.sprite = Spell.Icon;
    }
}
