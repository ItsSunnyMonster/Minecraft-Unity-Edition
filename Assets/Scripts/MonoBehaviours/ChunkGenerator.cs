//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections.Generic;
using Classes;
using UnityEngine;

namespace MonoBehaviours
{
    public class ChunkGenerator : MonoBehaviour
    {
        [Header("References")]
        public GameObject block;
        public GameObject chunk;
        [Space]
        [Header("Noise Options")]
        public float noiseAmplifier2D = 1f;
        public float noiseScaleMultiplier2D = 0.025f;
        public float noiseScaleMultiplier3D = 0.025f;
        [Range(0f, 1f)] public float noise3DThreshold = 0.5f;
        [Space]
        [Header("World Options")]
        public int worldHeight = 10;
        public Vector2Int chunkSize = new Vector2Int(16, 16);
    
        private Dictionary<Vector3, Chunk> Chunks = new Dictionary<Vector3, Chunk>();

        private void Start()
        {
            GenerateChunk();
        }

        /// <summary>
        /// Generates the chunk
        /// </summary>
        public void GenerateChunk()
        {
            // Add chunk
            Chunks.Add(
                new Vector3(0f, 0f, 0f),
                Instantiate(chunk, new Vector3(0f, 0f, 0f), Quaternion.identity).GetComponent<Chunk>());

            // Spawn blocks
            for (var x = 0; x < chunkSize.x; x++)
            {
                for (var z = 0; z < chunkSize.y; z++)
                {
                    for (var y = 0; y < worldHeight + Mathf.RoundToInt(Noise.Get2DNoiseValue(new Vector2(x, z), noiseScaleMultiplier2D, noiseAmplifier2D)); y++)
                    {
                        if (Noise.Get3DNoiseValue(new Vector3(x, y, z), noiseScaleMultiplier3D) < noise3DThreshold) continue;
                        if (y == 0)
                        {
                            SpawnBlock(new Vector3(x, y, z), Chunks[new Vector3(0f, 0f, 0f)].gameObject, BlockType.Bedrock);
                        }
                        else if (y > 0 && y < worldHeight)
                        {
                            SpawnBlock(new Vector3(x, y, z), Chunks[new Vector3(0f, 0f, 0f)].gameObject, BlockType.Stone);
                        }
                        else if (y >= worldHeight && y < worldHeight + Mathf.RoundToInt(Noise.Get2DNoiseValue(new Vector2(x, z), noiseScaleMultiplier2D, noiseAmplifier2D)) - 1)
                        {
                            SpawnBlock(new Vector3(x, y, z), Chunks[new Vector3(0f, 0f, 0f)].gameObject, BlockType.Dirt);
                        }
                        else
                        {
                            SpawnBlock(new Vector3(x, y, z), Chunks[new Vector3(0f, 0f, 0f)].gameObject, BlockType.GrassBlock);
                        }
                    }
                }
            }

            // Generate mesh
            Chunks[new Vector3(0f, 0f, 0f)].GenerateMesh();
        }

        /// <summary>
        /// Spawns a block of type <paramref name="type"/> in <paramref name="positionInGame"/> to <paramref name="parentChunk"/>.
        /// </summary>
        /// <param name="positionInGame">Position of block to spawn</param>
        /// <param name="parentChunk">Parent chunk of the block</param>
        /// <param name="type">Type of the block</param>
        /// <returns>The block spawned. </returns>
        public GameObject SpawnBlock(Vector3 positionInGame, GameObject parentChunk, BlockType type)
        {
            // Spawn block
            var spawnedBlock = Instantiate(block, positionInGame, Quaternion.identity, parentChunk.transform);
            spawnedBlock.GetComponent<Block>().type = type;
            parentChunk.GetComponent<Chunk>().BlocksInChunk.Add(positionInGame, spawnedBlock.GetComponent<Block>());

            // Return spawned block
            return spawnedBlock;
        }
    }
}