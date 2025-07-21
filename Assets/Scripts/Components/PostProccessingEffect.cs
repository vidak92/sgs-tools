using UnityEngine;

namespace SGSTools.Components
{
    public class PostProccessingEffect : MonoBehaviour
    {
        public Material EffectsMaterial;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, EffectsMaterial);
        }
    }
}