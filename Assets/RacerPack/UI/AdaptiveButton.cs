using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdaptiveButton : MonoBehaviour
{
    public Button button; 
    public Sprite roundedSprite; 
    void Start()
    { 
        Image image = button.GetComponent<Image>(); 
        image.sprite = roundedSprite; 
        image.type = Image.Type.Sliced; 
        
        HorizontalLayoutGroup layoutGroup = button.gameObject.AddComponent<HorizontalLayoutGroup>(); 
        layoutGroup.padding = new RectOffset(10, 10, 10, 10); 
        layoutGroup.childAlignment = TextAnchor.MiddleCenter; 
        layoutGroup.childControlWidth = true; 
        layoutGroup.childControlHeight = true; 
        layoutGroup.childForceExpandWidth = false; 
        layoutGroup.childForceExpandHeight = false; 
        Text buttonText = button.GetComponentInChildren<Text>(); 
        buttonText.alignment = TextAnchor.MiddleCenter; 
    }
}
