using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellVizualizator : MonoBehaviour
{
    [SerializeField] private GameObject _spellItemPrefabs;
    [SerializeField] private Transform _spellHolder;

    public void AddSpellOnBar(Spell spell)
    {
        GameObject GO = Instantiate(_spellItemPrefabs);
        GO.transform.SetParent(_spellHolder, false);
        SpellItem spellItem = GO.GetComponent<SpellItem>();
        spellItem.Spell = spell;
    }

}
