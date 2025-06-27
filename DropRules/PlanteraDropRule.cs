using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace LunarVeilLegacy.DropRules
{
    public class PlanteraDropRule : 
        IItemDropRuleCondition, 
        IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => "After skeletron is defeated";
    }
}
