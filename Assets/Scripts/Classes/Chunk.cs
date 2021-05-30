//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using MonoBehaviours;
using UnityEngine;

namespace Classes
{
    public class Chunk
    {
        public readonly GameObject ChunkGameObject;
        public readonly Vector2 Coordinate;
        
        public Chunk(Vector2 coordinate)
        {
            // Spawn chunk
            ChunkGameObject = Object.Instantiate(
                WorldGenerator.Instance.chunk, 
                new Vector3(coordinate.x * 16, 0, coordinate.y * 16), 
                Quaternion.identity, 
                WorldGenerator.Instance.transform);
            
            // Set coordinate
            Coordinate = coordinate;
            
            UpdateVisibility();
        }

        /// <summary>
        /// Sets the active state of _chunkGameObject to <paramref name="visible"/>
        /// </summary>
        /// <param name="visible">Determines if the chunk is visible or not</param>
        public void SetVisible(bool visible)
        {
            ChunkGameObject.SetActive(visible);
        }

        /// <summary>
        /// Update the visibility of the chunk
        /// </summary>
        public void UpdateVisibility()
        {
            var wg = WorldGenerator.Instance;

            var visible = Vector2.Distance(wg.playerChunkCoord, Coordinate) <= wg.renderDistance;
            
            SetVisible(visible);
        }
    }
}
