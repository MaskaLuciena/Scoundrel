using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackChoiceView : MonoBehaviour
{
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button useWeaponButton;
    [SerializeField] private Button takeDamageButton;

    public void Show(Action onUseWeapon, Action onTakeDamage)
    {
        choicePanel.SetActive(true);

        useWeaponButton.onClick.RemoveAllListeners();
        useWeaponButton.onClick.AddListener(() =>
        {
            choicePanel.SetActive(false);
            onUseWeapon?.Invoke();
        });

        takeDamageButton.onClick.RemoveAllListeners();
        takeDamageButton.onClick.AddListener(() =>
        {
            choicePanel.SetActive(false);
            onTakeDamage?.Invoke();
        });
    }

}