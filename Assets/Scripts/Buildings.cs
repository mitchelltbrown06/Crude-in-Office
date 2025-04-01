using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{

    [CreateAssetMenu(menuName = "Building/New Buildable Item" , fileName = "New Buildable Item")]
    public class Buildings : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public TileBase Tile { get; private set; }
    }
}