//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System;
using Classes.World;
using MonoBehaviours.World;
using UnityEngine;

namespace Classes.Misc
{
    public static class TextureAtlas
    {
        public const float TileFraction = 1f / 4; // The fraction of the side length each texture takes up

        /// <summary>
        /// Gets the Texture UV coordinates of a block
        /// </summary>
        /// <param name="type">Block type</param>
        /// <param name="orientation">Orientation of the block</param>
        /// <returns>TextureUV struct containing information about the UVs. </returns>
        /// <exception cref="ArgumentOutOfRangeException">Not actually going to be called unless one block type or orientation is not handled in this method. </exception>
        public static TextureUV GetTextureUV(BlockType type, BlockFaceOrientation orientation)
        {
            // Create variables for tile x and tile y
            int tileX, tileY;

            // Switch the type for textures
            switch (type)
            {
                case BlockType.Bedrock:
                    tileX = 1;
                    tileY = 1;
                    break;
            
                case BlockType.Stone:
                    tileX = 2;
                    tileY = 1;
                    break;
            
                case BlockType.Dirt:
                    tileX = 1;
                    tileY = 2;
                    break;
            
                case BlockType.GrassBlock:
                    // Switch the orientation for textures
                    switch (orientation)
                    {
                        case BlockFaceOrientation.Top:
                            tileX = 1;
                            tileY = 3;
                            break;
                        case BlockFaceOrientation.Left:
                        case BlockFaceOrientation.Right:
                        case BlockFaceOrientation.Front:
                        case BlockFaceOrientation.Back:
                            tileX = 2;
                            tileY = 2;
                            break;
                        case BlockFaceOrientation.Bottom:
                            tileX = 1;
                            tileY = 2;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                    }
                    break;

                case BlockType.Lava:
                    tileX = 2;
                    tileY = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            // Get the uv values
            var topLeft = new Vector2(tileX - 1, tileY) * TileFraction;
            var topRight = new Vector2(tileX, tileY) * TileFraction;
            var bottomLeft = new Vector2(tileX - 1, tileY - 1) * TileFraction;
            var bottomRight = new Vector2(tileX, tileY - 1) * TileFraction;

            // Return the uv values as TextureUV struct
            return new TextureUV(topLeft, topRight, bottomLeft, bottomRight);
        }
    }

    public struct TextureUV
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topLeft">The top left UV value of the block face</param>
        /// <param name="topRight">The top right UV value of the block face</param>
        /// <param name="bottomLeft">The bottom left UV value of the block face</param>
        /// <param name="bottomRight">The bottom right UV value of the block face</param>
        public TextureUV(Vector2 topLeft = default, Vector2 topRight = default, Vector2 bottomLeft = default, Vector2 bottomRight = default)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }
}