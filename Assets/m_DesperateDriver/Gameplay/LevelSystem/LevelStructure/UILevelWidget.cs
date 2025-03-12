using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class UILevelWidget : MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private TextMeshProUGUI moneyLimit;
    [SerializeField] private TextMeshProUGUI moneyHas;
    [SerializeField] private List<GameObject> visualElements;
    public void OnEnable()
    {
        moneyLimit.text = levelInventory.lvlMoneyLimit.ToString();
        moneyHas.text = levelInventory.Glb_Money.ToString();
        SetVisulaObjects();
    }

    private void SetVisulaObjects()
    {
        switch (levelInventory.Glb_Stars)
        {
            case 0:
                visualElements[0].SetActive(false);
                visualElements[1].SetActive(false);
                visualElements[2].SetActive(false);
                break;
            case 1:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(false);
                visualElements[2].SetActive(false);
                break;
            case 2:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(true);
                visualElements[2].SetActive(false);
                break;
            case 3:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(true);
                visualElements[2].SetActive(true);
                break;
        }
    }
}
