namespace Twitler.Utils.HashTools
{
    public class HashConverter : IHashConverter
    {
        public int GetHashValue(string hashTag)
        {
            return hashTag.GetHashCode();
        }
    }
}