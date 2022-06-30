using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AdvancedModDLC.Items.Weapons.Melee
{
    public class GiftOfDungon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("地牢的馈赠");
            Tooltip.SetDefault("一击秒杀地牢守卫");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.damage = 114514;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 100;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.width = 60;
            Item.height = 60;
            Item.scale = 1.25f;
            Item.useAnimation = 10;
            Item.useTime = 10;

            Item.value = Item.sellPrice(platinum: 45);
        }

        public override void AddRecipes() => CreateRecipe()
            .AddIngredient(ItemID.Bone, 44)
            .AddIngredient(Utils.Tool.GetModItem("AdvancedMod","Disable_Bar"))
            .AddTile(TileID.Anvils)
            .Register();
    }
}
