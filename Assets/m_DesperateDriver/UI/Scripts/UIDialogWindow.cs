using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action onConfirmAction;
    private Action onCancelAction;

    public void SetupWindow(string title, Action onConfirm, Action onCancel)
    {
        gameObject.SetActive(true);

        this.title.text = title;
        onConfirmAction = onConfirm;
        onCancelAction = onCancel;


        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        onConfirmAction?.Invoke(); // Invoke the confirm action
        gameObject.SetActive(false);
    }

    private void Cancel()
    {
        onCancelAction?.Invoke(); // Invoke the cancel action
        gameObject.SetActive(false); // Hide the window
    }
}
