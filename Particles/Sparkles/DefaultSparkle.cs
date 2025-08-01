﻿using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Helpers;

using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using SteelSeries.GameSense;

namespace LunarVeilLegacy.Particles.Sparkles
{
    public class DefaultSparkle : RaritySparkle
    {
        public DefaultSparkle(int lifetime, float scale, float initialRotation, float rotationSpeed, Vector2 position, Vector2 velocity)
        {
            Lifetime = lifetime;
            Scale = 0;
            MaxScale = scale;
            Rotation = initialRotation;
            RotationSpeed = rotationSpeed;
            Position = position;
            Velocity = velocity;
            DrawColor = Color.Lerp(Color.LightBlue, Color.LightCyan, Main.rand.NextFloat(1f));
            Texture = ModContent.Request<Texture2D>("LunarVeilLegacy/Particles/Sparkles/BaseRaritySparkleTexture").Value;
            BaseFrame = null;
        }
    }
}
