using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATA : MonoBehaviour
{
    public static Shader SHADER_DEFAULT;
    public static Shader SHADER_DOUBLE_SIDE;
    public static Dimensional DimensionData { get; private set; }
    public static float BREAKING_OUT_TIME_TAKEN = 35f;

    public class Dimensional
    {
        public readonly int npr = 10;
        public float matrixDiameter { get; private set; }
        public readonly float gridSize = 50;
        public float cubeDia { get; private set; }
        public float cubeR { get; private set; }

        public Dimensional()
        {
            cubeDia = 0.8f * gridSize;
            matrixDiameter = gridSize * npr;
            cubeR = cubeDia / 2;
        }

        public void resetSize(float newS)
        {
            cubeDia = newS * gridSize;
            cubeR = cubeDia / 2;
        }
    }

    private void Awake()
    {
        DimensionData = new Dimensional();
        SHADER_DEFAULT = Shader.Find("Custom/CUCUMBER");
        SHADER_DOUBLE_SIDE = Shader.Find("Ciconia Studio/Double Sided/Standard/Diffuse Bump");
    }

}
