using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellVizualizator : MonoBehaviour
{
    [SerializeField] private GameObject _spellItemPrefabs;
    [SerializeField] private Transform _spellHolder;

    private SpellItem[] _spellItemOnHolder = new SpellItem[7];//Inventory limit TODO!!!
    private int _itemCounter = 0;

    public void AddSpellOnBar(Spell spell)
    {
        SpellItem SI;

        GameObject GO = Instantiate(_spellItemPrefabs);
        GO.transform.SetParent(_spellHolder, false);

        SI = GO.GetComponent<SpellItem>();
        SI.Spell = spell;
        SI.Spell.TimeToCast = 0;
        SI.SetBorderVisible(false);
        _spellItemOnHolder[_itemCounter++] = SI;
    }

    public void SelectSpell(Spell spell)
    {
        for (int i = 0; i < _spellItemOnHolder.Length; i++)
        {
            if (_spellItemOnHolder[i] == null) continue;
            if(_spellItemOnHolder[i].Spell == spell)
            {
                _spellItemOnHolder[i].SetBorderVisible(true);
            }
            else
            {
                _spellItemOnHolder[i].SetBorderVisible(false);
            }
        }
    }
}
