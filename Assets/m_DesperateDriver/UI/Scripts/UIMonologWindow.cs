using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonologWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI buttonWriting;
    [SerializeField] private Button doButton;

    private Action onClickAction;

    public void SetupWindow(string title, string buttonWriting, Action onClick)
    {
        gameObject.SetActive(true);

        this.title.text = title;
        this.buttonWriting.text = buttonWriting;
        onClickAction = onClick;


        doButton.onClick.AddListener(Do);
    }

    private void Do()
    {
        onClickAction?.Invoke(); // Invoke the confirm action
        gameObject.SetActive(false);
    }
}
