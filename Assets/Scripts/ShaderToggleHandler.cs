using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShaderToggleHandler : MonoBehaviour
    {
        List<Material> materials = new List<Material>();

        bool shadersOff;
        Shader unlitShader;
        Shader defaultShader;

        private void Awake()
        {
            MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
            foreach(MeshRenderer renderer in renderers)
            {
                materials.AddRange(renderer.materials);
            }
            unlitShader = Shader.Find("Unlit/Color");
            defaultShader = Shader.Find("Standard");
        }

        public void ToggleShaders()
        {
            if (shadersOff)
            {
                shadersOff = false;
                ApplyShaderToAll(defaultShader);
            }
            else
            {
                shadersOff = true;
                ApplyShaderToAll(unlitShader);
            }
        }

        private void ApplyShaderToAll(Shader shader)
        {
            foreach(Material material in materials)
            {
                material.shader = shader;
            }
        }
    }
}