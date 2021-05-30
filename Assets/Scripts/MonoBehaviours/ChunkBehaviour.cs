//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BlockFaceOrientation
{
    Top,
    Left,
    Right,
    Bottom,
    Front,
    Back
}

namespace MonoBehaviours
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
    public class ChunkBehaviour : MonoBehaviour
    {
        public Dictionary<Vector3, Block> BlocksInChunk = new Dictionary<Vector3, Block>();

        private List<Vector3> _vertices = new List<Vector3>();
        private List<int> _triangles = new List<int>();
        private List<Vector2> _uvs = new List<Vector2>();

        private Vector3 _position;

        private Action _chunkGenerationCallback;

        private readonly System.Random _random = new System.Random();

        private void Start()
        {
            // Set position so it can be accessed from other threads
            _position = transform.position;

            // Setup and start thread
            var thread = new Thread(ChunkGenerationThread)
            {
                Name = "Chunk Generation"
            };
            
            thread.Start();
        }

        private void Update()
        {
            // If it is not null, then invoke
            _chunkGenerationCallback?.Invoke();
            
            // Set it back to null
            _chunkGenerationCallback = null;
        }

        /// <summary>
        /// The thread function to generate chunk data. It runs in the chunk generation thread. 
        /// </summary>
        private void ChunkGenerationThread()
        {
            GenerateBlockData();
            GenerateMeshData();
            _chunkGenerationCallback = UpdateMesh;
        }

        /// <summary>
        /// Generates block data
        /// </summary>
        private void GenerateBlockData()
        {
            // Spawn blocks
            for (var x = 0; x < 16; x++)
            for (var z = 0; z < 16; z++)
            for (var y = 0; y < 62 + Mathf.RoundToInt(Noise.Get2DNoiseValue(new Vector2(x + _position.x, z + _position.z), WorldGenerator.Instance.noiseScaleMultiplier2D, WorldGenerator.Instance.noiseAmplifier2D)); y++)
                SpawnBlockBasedOnContext(new Vector3Int(x, y, z) + Vector3Int.RoundToInt(_position));
        }

        /// <summary>
        /// Spawn a block based on all factors that affect world generation
        /// </summary>
        /// <param name="position">The block position relative to the world</param>
        private void SpawnBlockBasedOnContext(Vector3Int position)
        {
            // For convenient sake
            var wg = WorldGenerator.Instance;

            var columnHeight = 62 + Mathf.RoundToInt(Noise.Get2DNoiseValue(new Vector2(position.x, position.z),
                wg.noiseScaleMultiplier2D, wg.noiseAmplifier2D));

            if (position.y <= _random.Next(1, 4))
            {
                SpawnBlock(position - _position, BlockType.Bedrock);
            }
            else if (Noise.Get3DNoiseValue(position, wg.noiseScaleMultiplier3D) >= wg.noise3DThreshold)
            {
                if (position.y < 62 - _random.Next(1, 4))
                {
                    SpawnBlock(position - _position, BlockType.Stone);
                }
                else if (position.y < columnHeight - 1)
                {
                    SpawnBlock(position - _position, BlockType.Dirt);
                }
                else
                {
                    SpawnBlock(position - _position, BlockType.GrassBlock);
                }
            }
        }

        /// <summary>
        /// Spawns a block of type <paramref name="type"/> in <paramref name="positionInGame"/>.
        /// </summary>
        /// <param name="positionInGame">Position of block to spawn</param>
        /// <param name="type">Type of the block</param>
        /// <returns>The block spawned. </returns>
        private void SpawnBlock(Vector3 positionInGame, BlockType type)
        {
            BlocksInChunk.Add(positionInGame, new Block(type));
        }

        /// <summary>
        /// Generates chunk mesh data
        /// </summary>
        private void GenerateMeshData()
        {
            // Loop through the blocks
            foreach (var block in BlocksInChunk)
            {
                // If it is not next to blocks then add the face
                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(1f, 0f, 0f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Front);

                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(-1f, 0f, 0f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Back);

                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(0f, 1f, 0f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Top);

                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(0f, -1f, 0f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Bottom);

                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(0f, 0f, 1f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Left);

                if (!BlocksInChunk.ContainsKey(block.Key + new Vector3(0f, 0f, -1f))) AddBlockFace(block.Key, block.Value.type, BlockFaceOrientation.Right);
            }
        }

        /// <summary>
        /// Adds a quad to the mesh
        /// </summary>
        /// <param name="topLeft">Position of top left corner</param>
        /// <param name="topRight">Position of top right corner</param>
        /// <param name="bottomLeft">Position of bottom left corner</param>
        /// <param name="bottomRight">Position of bottom right corner</param>
        private void AddFace(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight)
        {
            // Add vertices
            _vertices.Add(topLeft); // -3
            _vertices.Add(topRight); // -2
            _vertices.Add(bottomLeft); // -1
            _vertices.Add(bottomRight); //

            // Add triangles
            var lastIndex = _vertices.Count - 1;

            _triangles.Add(lastIndex - 1); // Bottom Left
            _triangles.Add(lastIndex - 3); // Top Left
            _triangles.Add(lastIndex - 2); // Top Right

            _triangles.Add(lastIndex - 1); //Bottom Left
            _triangles.Add(lastIndex - 2); // Top Right
            _triangles.Add(lastIndex); // Bottom Right
        }

        /// <summary>
        /// Adds a block face to the chunk
        /// </summary>
        /// <param name="blockPosition">Position of the block</param>
        /// <param name="type">Type of the block</param>
        /// <param name="orientation">Orientation of the block face</param>
        /// <exception cref="ArgumentOutOfRangeException">Not gonna be called unless an orientation is not handled. </exception>
        private void AddBlockFace(Vector3 blockPosition, BlockType type, BlockFaceOrientation orientation)
        {
            // Switch the orientation to get the positions
            switch (orientation)
            {
                case BlockFaceOrientation.Top:
                    AddFace(
                        blockPosition + new Vector3(0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(0.5f, 0.5f, -0.5f),
                        blockPosition + new Vector3(-0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, 0.5f, -0.5f));
                    break;
                case BlockFaceOrientation.Bottom:
                    AddFace(
                        blockPosition + new Vector3(-0.5f, -.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, -0.5f, -0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, 0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, -0.5f));
                    break;
                case BlockFaceOrientation.Back:
                    AddFace(
                        blockPosition + new Vector3(-0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, 0.5f, -0.5f),
                        blockPosition + new Vector3(-0.5f, -.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, -0.5f, -0.5f));
                    break;
                case BlockFaceOrientation.Front:
                    AddFace(
                        blockPosition + new Vector3(0.5f, 0.5f, -0.5f),
                        blockPosition + new Vector3(0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, -0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, 0.5f));
                    break;
                case BlockFaceOrientation.Left:
                    AddFace(
                        blockPosition + new Vector3(0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, 0.5f, 0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, 0.5f),
                        blockPosition + new Vector3(-0.5f, -.5f, 0.5f));
                    break;
                case BlockFaceOrientation.Right:
                    AddFace(
                        blockPosition + new Vector3(-0.5f, 0.5f, -0.5f),
                        blockPosition + new Vector3(0.5f, 0.5f, -0.5f),
                        blockPosition + new Vector3(-0.5f, -0.5f, -0.5f),
                        blockPosition + new Vector3(0.5f, -0.5f, -0.5f));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }

            // Add uvs based on type and orientation
            AddUVs(TextureAtlas.GetTextureUV(type, orientation));
        }

        /// <summary>
        /// Adds uvs to the _uvs List
        /// </summary>
        /// <param name="uv">TextureUV struct that contains the uvs to add</param>
        private void AddUVs(TextureUV uv)
        {
            _uvs.Add(uv.TopLeft);
            _uvs.Add(uv.TopRight);
            _uvs.Add(uv.BottomLeft);
            _uvs.Add(uv.BottomRight);
        }

        /// <summary>
        /// Updates the mesh so that it shows up in game. 
        /// </summary>
        private void UpdateMesh()
        {
            // Create the mesh based on mesh data
            var mesh = new Mesh {vertices = _vertices.ToArray(), triangles = _triangles.ToArray(), uv = _uvs.ToArray()};
            // Recalculate normals
            mesh.RecalculateNormals();

            // Set the mesh
            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}