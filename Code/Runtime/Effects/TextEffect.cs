using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TextEffects
{
    public abstract class TextEffect : ScriptableObject
    {
        [Tooltip("List of link IDs that this effect works on")]
        [SerializeField] protected List<string> IDs = new List<string>(); 

        public virtual void ApplyEffect(TMP_Text textComponent, TextEffectsParser parser)
        {
            
        }
    }
}