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
    GrassBlock
}

namespace Classes
{
    public class Block
    {
        public BlockType type;

        public Block(BlockType type = default)
        {
            this.type = type;
        }
    }
}
