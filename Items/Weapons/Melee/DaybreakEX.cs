using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace AdvancedModDLC.Items.Weapons.Melee
{
    public class DaybreakEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("破晓之光 EX");
            Tooltip.SetDefault("使用光明之矛将敌人的时间停止……并四分五裂！");
        }

        public override string Texture => "Terraria/Images/Item_3543";

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Rapier;  //近战武器
            Item.damage = 1000;   //伤害
            Item.useAnimation = 20; //使用动画时长
            Item.useTime = 20;   //攻速
            Item.knockBack = 9;  //击退
            Item.width = 30;     //大小
            Item.height = 30;    //大小
            Item.scale = 1.25f;  //碰撞箱
            Item.rare = ItemRarityID.Red;       //稀有度
            Item.value = Item.sellPrice(gold: 44);
            Item.crit = 30;       //暴击率
            Item.autoReuse = true;   //自动挥舞
            Item.useTurn = true;      //使用中可转身
            Item.DamageType = DamageClass.Melee;     //近战武器
            Item.shootSpeed = 10f;    //射速
            Item.channel = true;      //有特殊行为
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.DaybreakEXProj>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);

            return true;
        }

        public override void AddRecipes() => CreateRecipe()
            .AddIngredient(ItemID.DayBreak)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.LunarBar, 15)
            .AddIngredient<AdvancedMod.Items.Mateiral.AdvancedBar>(10)
            .AddTile<AdvancedMod.Tiles.ElectromagneticWorkStation>()
            .Register();
    }
}
