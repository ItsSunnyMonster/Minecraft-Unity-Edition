//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

namespace Classes
{
    public static class Noise
    {
        #region PerlinNoise3D_And_Overloads

        /// <summary>
        /// <para>Generate 3D Perlin Noise. </para>
        /// </summary>
        /// <param name="point">The sample point</param>
        /// <returns><para>Value between 0.0 and 1.0</para></returns>
        public static float PerlinNoise3D(Vector3 point)
        {
            // Return another overload of the function
            return PerlinNoise3D(point.x, point.y, point.z);
        }

        /// <summary>
        /// <para>Generate 3D Perlin Noise. </para>
        /// </summary>
        /// <param name="x">The X-coordinate of the sample point. </param>
        /// <param name="y">The Y-coordinate of the sample point. </param>
        /// <param name="z">The Z-coordinate of the sample point. </param>
        /// <returns><para>Value between 0.0 and 1.0</para></returns>
        public static float PerlinNoise3D(float x, float y, float z)
        {
            // Get all three permutations of noise for x, y, z
            var ab = Mathf.PerlinNoise(x, y);
            var bc = Mathf.PerlinNoise(y, z);
            var ac = Mathf.PerlinNoise(x, z);

            // Get the reverse
            var ba = Mathf.PerlinNoise(y, x);
            var cb = Mathf.PerlinNoise(z, y);
            var ca = Mathf.PerlinNoise(z, x);

            // Return the average
            return (ab + bc + ac + ba + cb + ca) / 6f;
        }

        #endregion

        /// <summary>
        /// Generates 2D Noise Map
        /// </summary>
        /// <param name="noiseHeight">Y-axis of the size of noise map</param>
        /// <param name="noiseWidth">X-axis of the size of noise map</param>
        /// <param name="noiseSizeMultiplier">Controls the noise size. The smaller the number is, the bigger the noise map. </param>
        /// <param name="noiseAmplifier">Multiplies the result of noise values. </param>
        /// <returns>A 2D float array with noise values</returns>
        public static float[,] Get2DNoise(int noiseHeight, int noiseWidth, float noiseSizeMultiplier,
            float noiseAmplifier)
        {
            // Create new noisemap
            var noiseMap = new float[noiseWidth, noiseHeight];

            // Loop through all items
            for (var x = 0; x < noiseWidth; x++)
            for (var y = 0; y < noiseHeight; y++)
                // Set the value
                noiseMap[x, y] = Get2DNoiseValue(new Vector2(x, y), noiseSizeMultiplier, noiseAmplifier);
            
            // Return noisemap
            return noiseMap;
        }

        /// <summary>
        /// Generates 3D Noise Cube
        /// </summary>
        /// <param name="noiseHeight">Y-axis of the size of noise cube</param>
        /// <param name="noiseWidth">Z-axis of the size of noise cube</param>
        /// <param name="noiseLength">X-axis of the size of noise cube</param>
        /// <param name="noiseSizeMultiplier">Controls the noise size. The smaller the number is, the bigger the noise cube. </param>
        /// <param name="noiseAmplifier">Multiplies the result of noise values. </param>
        /// <returns>A 3D float array with noise values</returns>
        public static float[,,] Get3DNoise(int noiseHeight, int noiseWidth, int noiseLength, float noiseSizeMultiplier,
            float noiseAmplifier)
        {
            // Create new noise cube
            var noiseCube = new float[noiseWidth, noiseHeight, noiseLength];

            // Loop through all items
            for (var x = 0; x < noiseWidth; x++)
            for (var y = 0; y < noiseHeight; y++)
            for (var z = 0; z < noiseLength; z++)
                // Set value
                noiseCube[x, y, z] = Get3DNoiseValue(new Vector3(x, y, z), noiseSizeMultiplier, noiseAmplifier);

            // Return noise cube
            return noiseCube;
        }

        /// <summary>
        /// Gets 2D noise value at <paramref name="point"/>
        /// </summary>
        /// <param name="point">The sample point</param>
        /// <param name="noiseSizeMultiplier">Controls the noise size. The smaller the number is, the bigger the noise cube. </param>
        /// <param name="noiseAmplifier">Multiplies the result of noise values. </param>
        /// <returns>Noise value after resizing and amplifying. </returns>
        public static float Get2DNoiseValue(Vector2 point, float noiseSizeMultiplier, float noiseAmplifier)
        {
            // Get the noise value plus all the modification
            var value = Mathf.PerlinNoise(point.x * noiseSizeMultiplier, point.y * noiseSizeMultiplier) * noiseAmplifier;

            // Return value
            return value;
        }

        /// <summary>
        /// Gets 3D noise value at <paramref name="point"/>
        /// </summary>
        /// <param name="point">The sample point</param>
        /// <param name="noiseSizeMultiplier">Controls the noise size. The smaller the number is, the bigger the noise cube. </param>
        /// <param name="noiseAmplifier">Multiplies the result of noise values. </param>
        /// <returns>Noise value after resizing and amplifying. </returns>
        public static float Get3DNoiseValue(Vector3 point, float noiseSizeMultiplier, float noiseAmplifier = 1f)
        {
            // Get the noise value plus all the modification
            var value = PerlinNoise3D(point * noiseSizeMultiplier) * noiseAmplifier;

            // Return value
            return value;
        }
    }
}