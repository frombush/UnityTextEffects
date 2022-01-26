using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PostPacu.TextEffects
{
    [AddComponentMenu("TMPro Effects/Text Effects Manager")]
    [RequireComponent(typeof(TMP_Text))]
    public class TextEffectsManager : MonoBehaviour
    {
        private TextEffectsParser parser;
        private TMP_Text textComponent;

        [SerializeField] private List<TextEffect> effects = null;

        private void Awake()
        {
            textComponent = GetComponent<TMP_Text>();
            parser = new TextEffectsParser(this, textComponent);
        }

        private void OnEnable() => parser.OnEnable();
        private void OnDisable() => parser.OnDisable();
        
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