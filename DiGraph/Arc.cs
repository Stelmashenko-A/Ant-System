using System;

namespace DiGraph
{
    public class Arc<TMark, TVerticeMark, TVertice> : IArc<TMark, TVerticeMark, TVertice>
        where TVertice : IVertice<TVerticeMark>
    {

        public TMark Mark { get; set; }
          
        public TVertice Start { get; set; }

        public TVertice End { get; set; }
    }
}
