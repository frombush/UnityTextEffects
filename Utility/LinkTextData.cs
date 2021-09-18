namespace Pacu.TextEffects
{
    /// <summary>
    /// Stores the index at which the characters inside a link starts and ends. 
    /// And how many characters there are in that link. 
    /// </summary>
    [System.Serializable]
    public struct LinkTextData
    {
        public int startIndex;
        public int endIndex;
        public int count;

        public LinkTextData(int startIndex, int endIndex, int count)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.count = count;
        }
    }
}