using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private ParticleSystem _magicEffect;

    [SerializeField] private List<Spell> _allSpells;
    [SerializeField] private SpellVizualizator _spellVizualizator;

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
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _curentSpell.TimeToShoot <= 0)
        {
            Shoot();
            
            _curentSpell.TimeToShoot = _curentSpell.TimeBetweenShoot;
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


    private void NextSpell()
    {
        if(_allSpells.Count - 1 == _curentSpellPos)
        {
            _curentSpellPos = 0;
        }else
        {
            _curentSpellPos++;
        }
        _curentSpell = _allSpells[_curentSpellPos];
        //SelectSpell(_curentSpellPos);
    }

    private void PreviouseSpell()
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
        //SelectSpell(_curentSpellPos);
    }

    private void Shoot()
    {
        _magicEffect.Play();

        switch (_curentSpell.Type)
        {
            case SpellType.Heale:
                {
                    Debug.Log($"You heale by {_curentSpell.Damage}♥");
                }
                break;
            case SpellType.Damage:
                {
                    RaycastHit hit;
                    if (Physics.Raycast(_fpsCamera.transform.position, _fpsCamera.transform.forward, out hit, _curentSpell.Range))
                    {
                        Enemy target = hit.transform.GetComponent<Enemy>();
                        Debug.Log(hit.transform.name);
                        if (target != null)
                        {
                            target.TakeDamage(_curentSpell.Damage);
                        }
                    }
                }
                break;
        }

        
    }



    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
