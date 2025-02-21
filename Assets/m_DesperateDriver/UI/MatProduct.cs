using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatProduct: MonoBehaviour
{
    [SerializeField] private CarMaterialSetup matSetup;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockIcon;



    public void SetupConcreteProduct()
    {
        title.text = matSetup.title;
        icon.color = matSetup.iconColor;

        if (matSetup.isUnlock)
        {
            Destroy(lockIcon);
            price.text = matSetup.price;
        }
    }
}
