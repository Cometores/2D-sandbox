using UnityEngine;

namespace Graph.Contracts
{
    /// <summary>
    /// Interface definition for showing visual for a data point
    /// </summary>
    public interface IGraphVisual
    {
        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void CleanUp();
    }
}
