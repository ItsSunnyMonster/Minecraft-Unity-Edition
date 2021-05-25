//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

namespace MonoBehaviours
{
    public enum BlockType
    {
        Bedrock,
        Stone,
        Dirt,
        GrassBlock
    }

    public class Block : MonoBehaviour
    {
        public BlockType type;
    }
}