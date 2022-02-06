using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TextEffects
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextEffectsParser
    {
        private List<LinkContainer> linkContainers = new List<LinkContainer>();

        public TMP_Text TextComponent { get; set; }
        public bool HasTextChanged { get; set; }

        public TextEffectsParser(MonoBehaviour caller, TMP_Text textComponent)
        {
            TextComponent = textComponent;
            caller.StartCoroutine(ParseCoroutine());
        }

        // Subscribe to event fired when text object has been regenerated.
        public void OnEnable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);

        public void OnDisable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);

        private void ON_TEXT_CHANGED(Object obj)
        {
            if (obj == TextComponent) 
                HasTextChanged = true;  
        }

        private IEnumerator ParseCoroutine()
        {
            while (true)
            {
                if (HasTextChanged)
                {
                    ParseText();
                }
                yield return new WaitForSeconds(0.25f);
            }
        }

        /// <summary>
        /// This method gets all the links in our textComponent. Then loops through the links and adds them to a LinkContainer that has the same ID as them. 
        /// Before adding a link to a LinkContainer the link gets converted into a LinkTextData. If a LinkContainer with the same ID as the link doesn't exist 
        /// then we create a new LinkContainer with the same ID as the link and add the link to that LinkContainer. 
        /// </summary>
        private void ParseText()
        {
            TextComponent.ForceMeshUpdate();
            linkContainers.Clear();

            // Get all the links in textComponent
            TMP_TextInfo textInfo = TextComponent.textInfo;
            List<TMP_LinkInfo> linkInfos = new List<TMP_LinkInfo>();
            for (int i = 0; i < textInfo.linkCount; i++)
            {
                linkInfos.Add(textInfo.linkInfo[i]);
            }

            // Loop through all the links
            for (int i = 0; i < linkInfos.Count; i++)
            {
                TMP_LinkInfo currentLink = linkInfos[i];
                // If we don't yet have any LinkContainers then create a new LinkContainer that contains the current link and its ID
                if (linkContainers.Count == 0)
                {
                    LinkContainer newLinkContainer = CreateNewLinkContainer(currentLink); 
                    linkContainers.Add(newLinkContainer);
                    continue;
                }
                
                bool idExist = false;
                for (int j = 0; j < linkContainers.Count; j++)
                {
                    // If we find a linkContainer that has the same ID as the current links ID then we add this
                    // link to that linkContainer.
                    if (currentLink.GetLinkID() == linkContainers[j].ID)
                    {
                        idExist = true;
                        LinkTextData linkTextData = CreateLinkTextData(currentLink);
                        linkContainers[j].AddLinkTextData(linkTextData);
                        
                        // Prevent this link from being added multiple times
                        break;
                    }
                }

                // If we can't find a LinkContainer that has the same ID as the current links ID then we create a new 
                // LinkContainer that contains the current link and its ID.
                if (!idExist)
                {            
                    LinkContainer newLinkContainer = CreateNewLinkContainer(currentLink);
                    linkContainers.Add(newLinkContainer);
                }
            }
        }

        /// <summary>
        /// Creates a new LinkContainer with the id of linkInfo.GetLinkID()
        /// </summary>
        private LinkContainer CreateNewLinkContainer(TMP_LinkInfo linkInfo)
        {
            LinkTextData linkTextData = CreateLinkTextData(linkInfo);
            LinkContainer linkContainer = new LinkContainer(linkInfo.GetLinkID());
            linkContainer.AddLinkTextData(linkTextData);
            return linkContainer;
        }

        /// <summary>
        /// Creates a new LinkTextData based on the provided linkInfo
        /// </summary>
        private LinkTextData CreateLinkTextData(TMP_LinkInfo linkInfo)
        {
            int count = linkInfo.GetLinkText().Length;
            int startIndex = linkInfo.linkTextfirstCharacterIndex;
            int endIndex = startIndex + count; 
            LinkTextData textData = new LinkTextData(startIndex, endIndex, count);
            return textData;
        }

        /// <summary>
        /// Returns a list of LinkTextDatas that all contain one of the IDs provided
        /// </summary>
        public List<LinkTextData> GetLinkTextDatasWithIDs(List<string> ID)
        {
            List<LinkTextData> results = new List<LinkTextData>();
            for (int i = 0; i < linkContainers.Count; i++)
            {
                for (int j = 0; j < ID.Count; j++)
                {
                    if (linkContainers[i].ID == ID[j])
                    {
                        results.AddRange(linkContainers[i].linkTextDatas);
                    }
                }
            }
            return results;
        }
    }
}