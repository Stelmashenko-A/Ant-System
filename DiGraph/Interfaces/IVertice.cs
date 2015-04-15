
namespace DiGraph
{
    public interface IVertice<TMark>
    {
        TMark Mark { get; set; }
        int Number { get; set; }
    }
}
