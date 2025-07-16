using FlappyBird.Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IPointerDownHandler
{
    private Image _FillImage;

    [SerializeField] private RectTransform _ScalingRect;

    private float _ParentScale;

    void Awake()
    {
        AudioManager.Instance.OnVolumeChanged += (sender, args) =>
        {
            _FillImage.fillAmount = args.NewVolume;
        };
    }

    void Start()
    {
        _FillImage = GetComponent<Image>();
        _ParentScale = _ScalingRect.localScale.x;
        _FillImage.fillAmount = AudioManager.Instance.Volume;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _ParentScale = _ScalingRect.localScale.x;
        var imageWidth = _FillImage.rectTransform.rect.width * _FillImage.canvas.scaleFactor * _ParentScale;
        var fixedClick = eventData.position.x - _FillImage.transform.position.x;
        var percentage = fixedClick / imageWidth;
        var final = MathF.Round(percentage * 20) / 20;
        _FillImage.fillAmount = final;
        AudioManager.Instance.SetVolume(final);
    }
}
