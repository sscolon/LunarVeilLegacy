﻿using Terraria.Localization;
using Terraria.ModLoader;

namespace LunarVeilLegacy.UI.Dialogue
{
    internal abstract class Dialogue
    {
        public DialogueSystem DialogueSystem => ModContent.GetInstance<DialogueSystem>();
        public string GetLocalizedText(string title)
        {
            return Language.GetText($"Mods.LunarVeilLegacy.Dialogue.{title}").Value;
        }

        public virtual int Length { get; }
        public virtual void Next(int index) { }
        public virtual void Update(int index) { }
        public virtual void Complete() { }
    }
}
