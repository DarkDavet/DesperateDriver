using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIUpgradeWidget : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgradeVisualObjects;
    [SerializeField] private TextMeshProUGUI currentValue;
    [SerializeField] private TextMeshProUGUI nextValue;
    [SerializeField] private TextMeshProUGUI costValue;
    [SerializeField] private Button button;

    public void SetupWidget(UpgradeSetup upgradeSetup)
    {
        if (upgradeSetup.capacityNext == -1 && upgradeSetup.cost == -1)
        {
            SetVisulaObjects(upgradeSetup);
            foreach (GameObject upgradeObject in upgradeVisualObjects)
            {
                var image = upgradeObject.GetComponent<Image>();
                image.color = Color.green;  
            }

            currentValue.text = upgradeSetup.capacity.ToString();
            nextValue.text = "";
            costValue.text = "";
            button.interactable = false;
        }
        else
        {
            currentValue.text = upgradeSetup.capacity.ToString();
            nextValue.text = upgradeSetup.capacityNext.ToString();
            costValue.text = upgradeSetup.cost.ToString();
            SetVisulaObjects(upgradeSetup);
        }    
    }


    private void SetVisulaObjects(UpgradeSetup upgradeSetup)
    {
            switch (upgradeSetup.level)
            {
            case 0:
                upgradeVisualObjects[0].SetActive(false);
                upgradeVisualObjects[1].SetActive(false);
                upgradeVisualObjects[2].SetActive(false);
                break;
            case 1:
                upgradeVisualObjects[0].SetActive(true);
                upgradeVisualObjects[1].SetActive(false);
                upgradeVisualObjects[2].SetActive(false);
                break;
            case 2:
                upgradeVisualObjects[0].SetActive(true);
                upgradeVisualObjects[1].SetActive(true);
                upgradeVisualObjects[2].SetActive(false);
                break;
            case 3:
                upgradeVisualObjects[0].SetActive(true);
                upgradeVisualObjects[1].SetActive(true);
                upgradeVisualObjects[2].SetActive(true);
                break;
        }
    }
}
