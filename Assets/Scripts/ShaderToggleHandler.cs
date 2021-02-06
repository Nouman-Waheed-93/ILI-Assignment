using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    public class ShaderToggleHandler : MonoBehaviour
    {
        public class ShaderEvent : UnityEvent<Shader>
        {

        }

        public static ShaderToggleHandler singleton;

        public ShaderEvent onShaderChanged = new ShaderEvent();

        bool shadersOff;
        Shader unlitShader;
        Shader defaultShader;

        private void Awake()
        {
            unlitShader = Shader.Find("Unlit/Color");
            defaultShader = Shader.Find("Standard");
            singleton = this;
        }

        public void ToggleShaders()
        {
            shadersOff = !shadersOff;
            Shader newShader = GetCurrentShader();
            onShaderChanged.Invoke(newShader);
        }

        public Shader GetCurrentShader()
        {
            if (shadersOff)
                return unlitShader;
            else
                return defaultShader;
        }
    }
}