using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

// This is in the base namespace for convience with how often textures are used with newer visuals.
// Please maintain Type -> Alphabetical order when adding new textures to the registry.
namespace LunarVeilLegacy.Helpers.Separate
{
    public static class StellasTextureRegistry
    {
       

        public static Asset<Texture2D> BloomLine => ModContent.Request<Texture2D>("LunarVeilLegacy/Textures/BloomLine");

        public static Asset<Texture2D> BloomLineSmall => ModContent.Request<Texture2D>("LunarVeilLegacy/Textures/BloomLineSmall");

        public static Asset<Texture2D> Invisible => ModContent.Request<Texture2D>("LunarVeilLegacy/Textures/Invisible");

     
        public static Asset<Texture2D> LaserCircle => ModContent.Request<Texture2D>("LunarVeilLegacy/Textures/LaserCircle");

        public static Asset<Texture2D> Line => ModContent.Request<Texture2D>("ILunarVeilLegacy/Textures/Line");

        public static string InvisPath => "ILunarVeilLegacy/Textures/Invisible";
    }
}
