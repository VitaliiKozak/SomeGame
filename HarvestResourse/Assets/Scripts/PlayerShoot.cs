using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    

    [SerializeField] private List<Spell> _allSpells;
    [SerializeField] private SpellVizualizator _spellVizualizator;

    [SerializeField] private ParticleSystem _magicEffect;

    private Spell _curentSpell;
    private int _curentSpellPos;


    private void Start()
    {
        if(_allSpells.Count > 0)
        {
            _curentSpell = _allSpells[0];
            _curentSpellPos = 0;
        }
        else
        {
            throw new Exception("Don`t have any spell!!!");
        }

        for (int i = 0; i < _allSpells.Count; i++)
        {
            _spellVizualizator.AddSpellOnBar(_allSpells[i]);
        }

        SelectCurentSpell();
    }


    private void Update()
    {
        for (int i = 0; i < _allSpells.Count; i++)
        {
            _allSpells[i].UpdateTime(Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1") && _curentSpell.ReadyToCast)
        {
            _magicEffect.Play();
            _curentSpell.SpellCast();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            NextSpell();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)//back
        {
            PreviouseSpell();
        }
    }


    private void PreviouseSpell()
    {
        if(_allSpells.Count - 1 == _curentSpellPos)
        {
            _curentSpellPos = 0;
        }else
        {
            _curentSpellPos++;
        }
        _curentSpell = _allSpells[_curentSpellPos];
        SelectCurentSpell();
    }

    private void NextSpell()
    {
        if (_curentSpellPos == 0)
        {
            _curentSpellPos = _allSpells.Count - 1;
        }
        else
        {
            _curentSpellPos--;
        }
        _curentSpell = _allSpells[_curentSpellPos];
        SelectCurentSpell();
    }

    private void SelectCurentSpell()
    {
        _spellVizualizator.SelectSpell(_curentSpell);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
