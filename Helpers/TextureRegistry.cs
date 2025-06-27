using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace LunarVeilLegacy.Helpers
{
    internal static class TextureRegistry
    {
        public static string EmptyTexture => "LunarVeilLegacy/Assets/Textures/Empty";
        public static string EmptyBigTexture => "LunarVeilLegacy/Assets/Textures/EmptyBig";
        public static string FlowerTexture => "LunarVeilLegacy/Effects/Masks/Flower";
        public static string CircleOutline => "LunarVeilLegacy/Effects/Masks/Extra_67";
        public static string NormalNoise1 => "LunarVeilLegacy/Textures/NormalNoise1";
        public static string ZuiEffect => "LunarVeilLegacy/Effects/Masks/ZuiEffect";
        public static string VoxTexture3 => "LunarVeilLegacy/Assets/Effects/VoxTexture3";

        public static string VoxTexture4 => "LunarVeilLegacy/Assets/Effects/VoxTexture5";

        public static string BoreParticleWhite => "LunarVeilLegacy/Particles/BoreParticleWhite";
        public static Asset<Texture2D> CloudTexture => ModContent.Request<Texture2D>("LunarVeilLegacy/Assets/Effects/CloudTexture");
        public static Asset<Texture2D> IrraTexture => ModContent.Request<Texture2D>("LunarVeilLegacy/Assets/Effects/IrraTexture2");
    }
}
