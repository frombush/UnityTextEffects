using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PostPacu.TextEffects
{
    [RequireComponent(typeof(TextEffectsParser), typeof(TMP_Text))]
    public class TextEffectsManager : MonoBehaviour
    {
        private TextEffectsParser parser;
        private TMP_Text textComponent;

        [SerializeField] private List<TextEffect> effects = null;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
            parser = GetComponent<TextEffectsParser>();
        }

        private void LateUpdate()
        {
            textComponent.ForceMeshUpdate();
            ApplyEffects();
            UpdateTextComponent();
        }

        private void ApplyEffects()
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].ApplyEffect(textComponent, parser);
            }
        }

        private void UpdateTextComponent()
        {
           for (int i = 0; i < textComponent.textInfo.meshInfo.Length; ++i)
            {
                TMP_MeshInfo meshInfo = textComponent.textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }
}