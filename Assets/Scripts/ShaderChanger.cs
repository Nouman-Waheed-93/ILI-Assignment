using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShaderChanger : MonoBehaviour
    {
        MeshRenderer renderer;

        private void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            ShaderToggleHandler shaderHandler = ShaderToggleHandler.singleton;
            shaderHandler.onShaderChanged.AddListener(ChangeShader);
            ChangeShader(shaderHandler.GetCurrentShader());
        }

        private void ChangeShader(Shader shader)
        {
            foreach(Material material in renderer.materials)
            {
                material.shader = shader;
            }
        }
    }
}