using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShaderChanger : MonoBehaviour
    {
        MeshRenderer renderer;
        [SerializeField]
        List<int> ignoreMaterialIndexes;

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
            for(int materialIndex = 0; materialIndex < renderer.materials.Length; materialIndex++)
            {
                if (!ignoreMaterialIndexes.Contains(materialIndex))
                {
                    renderer.materials[materialIndex].shader = shader;
                }
            }
        }
    }
}