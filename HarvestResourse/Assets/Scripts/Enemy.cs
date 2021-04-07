using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Healths")]
    [SerializeField] private float _health = 50f;
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TMP_Text _healthBarText;
    [SerializeField] private GameObject _canvasGameobject;
    [SerializeField] private float _updateSpeedSeconds;


    public void TakeDamage(float ammount)
    {
        ModifyHealth(ammount);
    }



    private void ModifyHealth(float amount)
    {
        _health -= amount;
        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        UpdateHealthBar();
        if(_health <=0)
        {
            Destroy(gameObject);
        }
        else if(_health < _maxHealth)
        {
            _canvasGameobject.SetActive(true);
        }else if(_health >= _maxHealth)
        {
            _canvasGameobject.SetActive(false);
        }
    }

    private void UpdateHealthBar()
    {
        _healthBarText.text = $"{_health.ToString("F1")} / {_maxHealth.ToString("F1")} HP";
        var currentHealthPtc = (float)_health / (float)_maxHealth;
        StartCoroutine(ChangeHealthToPrc(currentHealthPtc));
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void LateUpdate()
    {
        _canvasGameobject.transform.LookAt(Camera.main.transform);
        _canvasGameobject.transform.Rotate(0, 180, 0);
    }


    private IEnumerator ChangeHealthToPrc(float prc)
    {
        var preChangePct = _healthBarImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < _updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            _healthBarImage.fillAmount = Mathf.Lerp(preChangePct, prc, elapsed / _updateSpeedSeconds);
            yield return null;      
        }

        _healthBarImage.fillAmount = prc;
    }
}
