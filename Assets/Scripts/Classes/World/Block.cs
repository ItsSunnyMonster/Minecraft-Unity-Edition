//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

public enum BlockType
{
    Bedrock,
    Stone,
    Dirt,
    GrassBlock,
    Lava
}

public class Block
{
    public BlockType type;

    /// <summary>
    /// Creates a new block of type <paramref name="type"/>
    /// </summary>
    /// <param name="type">Type of the block</param>
    public Block(BlockType type)
    {
        this.type = type;
    }
}