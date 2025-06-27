using Microsoft.Xna.Framework;

namespace LunarVeilLegacy.Helpers
{
    internal interface IComboSystem
    {
        int[] ComboProjectiles { get; }
        string[] ComboProjectilesIcons { get; }
        float[] ComboProjectilesDamageMultiplers { get; }
        string FullCharge { get; }
        string EmptyCharge { get; }
        Vector3 ColorStart { get; }
        Vector3 ColorEnd { get; }
    }
}