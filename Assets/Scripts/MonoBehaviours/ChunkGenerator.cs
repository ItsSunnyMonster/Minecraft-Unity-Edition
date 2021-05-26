//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

namespace MonoBehaviours
{
    public class ChunkGenerator : MonoBehaviour
    {
        [Header("References")]
        public GameObject chunk;
        [Space]
        [Header("Noise Options")]
        public float noiseAmplifier2D = 1f;
        public float noiseScaleMultiplier2D = 0.025f;
        public float noiseScaleMultiplier3D = 0.025f;
        [Range(0f, 1f)] public float noise3DThreshold = 0.5f;

        public static ChunkGenerator Instance { get; private set; }
    
        private Dictionary<Vector3, Chunk> _chunks = new Dictionary<Vector3, Chunk>();
        private List<Block> _allBlocksInScene = new List<Block>();

        private void Awake()
        {
            // Set singleton
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    GenerateChunk(new Vector3(x, 0, y));
                }
            }
        }

        /// <summary>
        /// Generates the chunk
        /// </summary>
        private void GenerateChunk(Vector3 position)
        {
            // Add chunk
            _chunks.Add(
                position,
                Instantiate(chunk, position * 16, Quaternion.identity, transform).GetComponent<Chunk>());
        }
    }
}