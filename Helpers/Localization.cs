using Terraria.Localization;
using Terraria.ModLoader;
namespace LunarVeilLegacy.Helpers
{
    public static class LangText
    {
        public static string Chat(ModNPC npc, string Path)
        {
            return Language.GetTextValue($"Mods.LunarVeilLegacy.NPCs.{npc.Name}." + Path);
        }
        public static string Chat(ModNPC npc, string Path, object arg0)
        {
            return Language.GetTextValue($"Mods.LunarVeilLegacy.NPCs.{npc.Name}." + Path, arg0);
        }
        
        public static LocalizedText CreateBestiary(ModNPC npc, string Text, string key = null)
        {
            return Language.GetOrRegister($"Mods.LunarVeilLegacy.NPCs.{npc.Name}.Bestiary" + key, () => Text);
        }
        /// <summary>
        /// OrginText doesn't influence anything.
        /// You should edit Mods.LunarVeilLegacy.NPCs.hjson instead of OrginText.
        /// </summary>
        /// <param name="OrginText"></param>
        /// <returns></returns>
        public static string Bestiary(ModNPC npc, string OrginText, string key = null)
        {
            //return (string)Language.GetOrRegister($"Mods.LunarVeilLegacy.NPCs.{npc.Name}.Bestiary" + key, () => OrginText);
            return Language.GetTextValue($"Mods.LunarVeilLegacy.NPCs.{npc.Name}.Bestiary" + key, OrginText);
        }

        public static string Common(string Path, object arg0 = null)
        {
            return Language.GetTextValue("Mods.LunarVeilLegacy.Items.Common." + Path, arg0);
        }
        public static string Special(ModItem item, string key = null, object arg0 = null)
        {
            return Language.GetTextValue($"Mods.LunarVeilLegacy.Items.{item.Name}.Special" + key, arg0);
        }
        public static string SetBonus(ModItem item)
        {
            return Language.GetTextValue($"Mods.LunarVeilLegacy.Items.SetBonus.{item.Name}");
        }

        public static string Misc(string key)
        {
            return Language.GetTextValue("Mods.LunarVeilLegacy.Misc." + key);
        }
    }
}