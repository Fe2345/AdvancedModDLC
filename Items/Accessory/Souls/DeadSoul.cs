using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AdvancedModDLC.Items.Accessory.Souls
{
    public class DeadSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("死亡者之魂");
        }

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 42;
            Item.height = 42;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 11);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "VulnerabilityHex").Type] = true;
        }
    }
}
