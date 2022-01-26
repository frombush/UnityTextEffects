using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pacu.TextEffects
{
    [CreateAssetMenu(menuName = "TextEffects/ShakeEffect")]
    public class TextShakeEffect : TextEffect
    {
        [SerializeField] private float shakeMultiplier = 0f;

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
                
                // Apply shake effect
                Vector3 offset = new Vector3(Random.Range(-shakeMultiplier, shakeMultiplier), Random.Range(-shakeMultiplier, shakeMultiplier), 0f);
                for (int j = 0; j < 4; ++j)
                {
                    int k = charInfo.vertexIndex + j;
                    Vector3 origin = meshInfo.vertices[k];
                    meshInfo.vertices[k] = origin + offset;
                }
            }
        }
    }
}