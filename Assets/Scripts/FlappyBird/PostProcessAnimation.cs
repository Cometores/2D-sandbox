using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FlappyBird
{
    public class PostProcessAnimation : MonoBehaviour
    {
        private LensDistortion _lensDistortion;
    
        private void Awake()
        {
            var volume = GetComponent<Volume>();
            volume.profile.TryGet(out _lensDistortion);
        }
    
        private void OnEnable()
        {
            _lensDistortion.intensity.value = 0;
            
            DOTween.To(
                () => _lensDistortion.intensity.value,
                x => _lensDistortion.intensity.value = (float)x,
                -0.85,
                0.5f
            ).SetEase(Ease.OutElastic);
        }
    }
}
