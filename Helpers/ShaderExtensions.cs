﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;

namespace LunarVeilLegacy.Helpers
{
    internal static class ShaderExtensions
    {
        public static void UseIntensity(this MiscShaderData miscShaderData, float intensity)
        {
            miscShaderData.Shader.Parameters["uIntensity"].SetValue(intensity);
        }
    }
}
