using JsonStructures;

namespace Actions
{
    public interface IActionsArray
    {
        ActionDefinition this[int index] { get; }
    }
}