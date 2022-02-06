using System.Collections.Generic;

namespace TextEffects
{
    public static class TextEffectsUtility
    {
        public static int GetTotalCharacterCount(List<LinkTextData> linkTextDatas)
        {
            int totalCount = 0;
            for (int i = 0; i < linkTextDatas.Count; i++)
            {
                totalCount += linkTextDatas[i].count;
            }
            return totalCount;
        }
    }
}