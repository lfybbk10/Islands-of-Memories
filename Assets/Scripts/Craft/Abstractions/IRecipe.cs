using System.Collections.Generic;

namespace Craft
{
    public interface IRecipe
    {
        Component Received { get; }
        IEnumerable<Component> Requirements { get; }
    }
}