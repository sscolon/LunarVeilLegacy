using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace LunarVeilLegacy.DropRules
{
    public class BloodmoonDropRule : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => Main.bloodMoon;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => "During a Bloodmoon";
    }
}
