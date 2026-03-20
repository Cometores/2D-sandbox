using UnityEngine;

namespace Graph.Contracts
{
    /// <summary>
    /// Represents a single Visual Object in the graph
    /// </summary>
    public interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void CleanUp();
    }
}