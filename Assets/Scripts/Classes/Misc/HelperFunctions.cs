//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Classes.Misc
{
    public static class HelperFunctions
    {
        /// <summary>
        /// <para>Removes all children of <paramref name="parent"/>. </para>
        /// </summary>
        /// <param name="parent">The parent of the children to remove</param>
        public static void RemoveAllChildren(GameObject parent)
        {
            // Loop through all the children
            for (var i = 0; i < parent.transform.childCount; i++)
            {
                // Destroy child at index i
                var transform = parent.transform.GetChild(i);
                Object.Destroy(transform.gameObject);
            }
        }
    }
}