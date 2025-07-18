using FlappyBird.UI;
using UnityEngine;

public class FireballVFX : MonoBehaviour
{
    private MenuButton _menuButton;

    private void Awake()
    {
        _menuButton = GetComponentInParent<MenuButton>();
        if (_menuButton == null)
            Debug.LogError("MenuButton not found in parent hierarchy.", this);
            
    }
    
    /// <summary>
    /// Used as Event Trigger on fireball animation
    /// </summary>
    public void TurnOffVFX() => _menuButton.TurnOffVFX();
}