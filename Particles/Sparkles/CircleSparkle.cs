﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LunarVeilLegacy.Helpers;
using Terraria;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Particles.Sparkles
{
    public class CircleSparkle : RaritySparkle
    {
        public CircleSparkle(int lifetime, float scale, float initialRotation, float rotationSpeed, Vector2 position, Vector2 velocity)
        {
            Lifetime = lifetime;
            Scale = 0;
            MaxScale = scale;
            Rotation = initialRotation;
            RotationSpeed = rotationSpeed;
            Position = position;
            Velocity = velocity;
            DrawColor = Color.Lerp(Color.White, Color.White, Main.rand.NextFloat(1f));
            Texture = ModContent.Request<Texture2D>("LunarVeilLegacy/Particles/MagicCircle2").Value;
            BaseFrame = null;
        }
    }
}
