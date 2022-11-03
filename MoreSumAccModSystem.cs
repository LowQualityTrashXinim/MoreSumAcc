using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace MoreSumAcc
{
    internal class MoreSumAccModSystem : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup AnyIronBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} { Lang.GetItemNameValue(ItemID.IronBar) }", new int[]
                {
                ItemID.IronBar,
                ItemID.LeadBar
                });
            RecipeGroup.RegisterGroup("3rd tier Bar", AnyIronBar);

            RecipeGroup AnyCrown = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} { Lang.GetItemNameValue(ItemID.GoldCrown) }", new int[]
            {
                ItemID.GoldCrown,
                ItemID.PlatinumCrown
            });
            RecipeGroup.RegisterGroup("Crowns", AnyCrown);
        }
    }
}
