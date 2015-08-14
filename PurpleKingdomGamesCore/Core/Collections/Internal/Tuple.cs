namespace PurpleKingdomGames.Core.Collections.Internal
{
    /// <summary>
    /// Provides a Tuple implementaion. This is available in .Net 4.5, but
    /// we're compiling to 3.5 so we need this replacement
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    internal class Tuple<T1, T2>
    {
        /// <summary>
        /// First item
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// Second item
        /// </summary>
        public T2 Item2;

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}