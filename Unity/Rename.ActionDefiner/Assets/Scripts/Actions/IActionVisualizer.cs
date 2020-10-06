using JsonStructures;

namespace Actions
{
    public interface IActionVisualizer
    {
        void AddVisualFor(ActionDefinition definition);
    }
}