using UnityEngine;

namespace BuildingSystem
{
    public class ConstructionLayer : TilemapLayer
    {
        public void Build(Vector3 worldCoords, BuildableItem item)
        {
            var coords = _tilemap.WorldToCell(worldCoords);
            if (item.Tile != null)
            {
                _tilemap.SetTile(coords, item.Tile);
            }
        }
    }
}