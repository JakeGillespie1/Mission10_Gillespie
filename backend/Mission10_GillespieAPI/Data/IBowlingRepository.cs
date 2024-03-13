namespace Mission10_GillespieAPI.Data
{
    /*
    This is a public interface that defines a contract that classes can implement.
    It must provide an implementation for the members defined in the interface.
    IEnumerable<>: this is a generic interface provided by C# that represents a sequence of elements
        (in this case, a sequence of Bowler objects).
    Interfaces are used to define a blueprint for data access operations. Classes that implement this interface
    will provide the actual implementation for accessing and managing bowler data. Abstraction and loose
    coupling are also benefits of using an interface.
    */
    public interface IBowlingRepository
    {
        IEnumerable<Bowler> Bowlers { get; }

        IEnumerable<Team> Teams { get; }

    }
}
