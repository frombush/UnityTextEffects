using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PostPacu.TextEffects
{
    [CreateAssetMenu(menuName="Text Effects/Wave/Vertex Wave Effect", fileName="Vertex Wave Effect")]
    public class TextVertexWaveEffect : TextEffect
    {   
        [SerializeField] private float amplitude = 0f;
        [SerializeField] private float speedMultiplier = 0f;
        [SerializeField] private float curveMultiplier = 0f;

        public override void ApplyEffect(TMP_Text textComponent, TextEffectsParser parser)
        {
            TMP_TextInfo textInfo = textComponent.textInfo;
            List<LinkTextData> textDatas = parser.GetLinkTextDatasWithIDs(IDs);
            int totalCharacterCount = TextEffectsUtility.GetTotalCharacterCount(textDatas);
            int textDataIndex = 0;
            int charIndex = 0;

            for (int i = 0; i < totalCharacterCount; ++i)
            {
                // If the charIndex is equal to the length of the current textData character count then we know that we have fully manipulated
                // this textData (all characters in it) and can now move on to manipulating the other textDatas in our textDatas list
                if (charIndex == textDatas[textDataIndex].count)
                {
                    textDataIndex++;
                    charIndex = 0;
                }

                // Get the index of the current character that we want to animate
                int index = textDatas[textDataIndex].startIndex + charIndex;
                TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
                TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];        

                // Increase charIndex so that we will manipulate the next character in the next loop
                charIndex++;
                        
                // Skip characters that are not visible and thus have no geometry to manipulate.
                if (!charInfo.isVisible)
                    continue;
                
                // Apply wave effect
                for (int j = 0; j < 4; ++j)
                {
                    int k = charInfo.vertexIndex + j;
                    Vector3 origin = meshInfo.vertices[k];
                    meshInfo.vertices[k] = origin + new Vector3(0f, Mathf.Sin(Time.time * speedMultiplier + origin.x * curveMultiplier) * amplitude, 0f);
                }
            }
        }
    }
}