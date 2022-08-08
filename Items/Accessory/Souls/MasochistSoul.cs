using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AdvancedModDLC.Items.Accessory.Souls
{
    public class MasochistSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("受虐狂之魂");
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
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "GodEater").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "Lovestruck").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "MutantNibble").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "MutantFang").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "AbomFang").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "MutantPresence").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "AbomPresence").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>("FargowiltasSouls", "DeviPresence").Type] = true;
        }
    }
}
