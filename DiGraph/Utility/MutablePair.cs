namespace DiGraph.Utility
{
    class MutablePair<TFirst, TSecond>
    {
        public MutablePair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }
        public TFirst First { get; set; }
        public TSecond Second { get; set; }

    }
}
