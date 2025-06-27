using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace LunarVeilLegacy.DropRules
{
    public class HardmodeDropRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.hardMode;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => "During Hardmode";
    }
}
