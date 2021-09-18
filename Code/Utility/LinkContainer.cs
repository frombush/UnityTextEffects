using System.Collections.Generic;

namespace Pacu.TextEffects
{
    /// <summary>
    /// Contains a list of LinkTextDatas that all have the same ID.
    /// </summary>
    [System.Serializable]
    public class LinkContainer
    {
        public string ID;
        public List<LinkTextData> linkTextDatas = new List<LinkTextData>();

        public LinkContainer(string ID)
        {
            this.ID = ID;
        }

        public bool AddLinkTextData(LinkTextData linkTextData)
        {
            for (int i = 0; i < linkTextDatas.Count; i++)
            {
                // If all the properties are the same then that means we already have this textData in our list and thus won't add it again
                if (linkTextDatas[i].count == linkTextData.count 
                &&  linkTextDatas[i].startIndex == linkTextData.startIndex 
                &&  linkTextDatas[i].endIndex == linkTextData.endIndex)
                {
                    return false;
                }
            }

            linkTextDatas.Add(linkTextData);
            return true;
        }
    }
}