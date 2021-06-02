//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections.Generic;
using Classes.World;
using UnityEngine;

namespace MonoBehaviours.World
{
    public class WorldGenerator : MonoBehaviour
    {
        [Header("References")]
        public GameObject chunk;
        public Transform player;
        [Space]
        [Header("Noise Options")]
        public float noiseAmplifier2D = 10f;
        public float noiseScaleMultiplier2D = 0.05f;
        public float noiseScaleMultiplier3D = 0.025f;
        [Range(0f, 1f)] public float noise3DThreshold = 0.35f;
        [Space]
        [Header("World Options")]
        public int renderDistance = 2;

        public static WorldGenerator Instance { get; private set; }
    
        private Dictionary<Vector3, Chunk> _chunks = new Dictionary<Vector3, Chunk>();

        private Vector2 _playerChunkCoordLastFrame;
        public Vector2 playerChunkCoord;

        private List<Chunk> _chunksVisibleLastUpdate = new List<Chunk>();

        private bool _firstFrame = true;

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

        private void Update()
        {
            // Set position and coordinates
            var position = player.position;
            playerChunkCoord = Vector2Int.RoundToInt(new Vector2(position.x, position.z) / 16);
            
            // Update chunks if player moves out of chunk
            if (playerChunkCoord != _playerChunkCoordLastFrame || _firstFrame)
            {
                UpdateChunks();
            }
            _playerChunkCoordLastFrame = playerChunkCoord;

            _firstFrame = false;
        }
        
        private void UpdateChunks()
        {
            // Delete all chunks visible last update
            foreach (var currentChunk in _chunksVisibleLastUpdate)
            {
                currentChunk.SetVisible(false);
            }
            _chunksVisibleLastUpdate.Clear();
            
            // Update chunks that should be visible this frame
            var playerPosition = player.position;
            var playerChunkCoordX = Mathf.RoundToInt(playerPosition.x / 16);
            var playerChunkCoordY = Mathf.RoundToInt(playerPosition.z / 16);
            
            for (var x = -renderDistance; x < renderDistance; x++)
            {
                for (var y = -renderDistance; y < renderDistance; y++)
                {
                    var currentChunkCoord = new Vector2(playerChunkCoordX + x, playerChunkCoordY + y);
                    var currentChunkCoordV3 = new Vector3(currentChunkCoord.x, 0, currentChunkCoord.y);
                    
                    if (_chunks.ContainsKey(currentChunkCoordV3))
                    {
                        _chunks[currentChunkCoordV3].UpdateVisibility();
                    }
                    else
                    {
                        GenerateChunk(currentChunkCoordV3);
                    }
                    
                    _chunksVisibleLastUpdate.Add(_chunks[currentChunkCoordV3]);
                }
            }
        }

        /// <summary>
        /// Generates the chunk
        /// </summary>
        /// <param name="coordinate">The coordinate of the chunk (not position in game) </param>
        private void GenerateChunk(Vector3 coordinate)
        {
            // Add chunk
            _chunks.Add(coordinate, new Chunk(new Vector2(coordinate.x, coordinate.z)));
        }
    }
}