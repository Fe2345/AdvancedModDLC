﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using System.Collections.Generic;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.Bestiary;
using AdvancedModDLC.Utils;

namespace AdvancedModDLC.NPCs
{
    [AutoloadHead]
    public class Chemist : ModNPC
    {
        public static int shopNum = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("化学家");
            //该NPC的游戏内显示名
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Guide];
            //NPC总共帧图数，一般为16+下面两种帧的帧数
            NPCID.Sets.ExtraFramesCount[NPC.type] = NPCID.Sets.ExtraFramesCount[NPCID.Guide];
            //额外活动帧，一般为5
            NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackFrameCount[NPCID.Guide];
            //攻击帧，这个帧数取决于你的NPC攻击类型，射手填5，战士和投掷填4，法师填2，当然，也可以多填，就是不知效果如何（这里直接引用商人的）
            NPCID.Sets.DangerDetectRange[NPC.type] = 1000;
            //巡敌范围，以像素为单位，这个似乎是半径
            NPCID.Sets.AttackType[NPC.type] = NPCID.Sets.AttackType[NPCID.Guide];
            //攻击类型，一般为0，想要模仿其他NPC就填他们的ID
            NPCID.Sets.AttackTime[NPC.type] = 20;
            //单次攻击持续时间，越短，则该NPC攻击越快（可以用来模拟长时间施法的NPC）
            NPCID.Sets.AttackAverageChance[NPC.type] = 2;
            //NPC遇敌的攻击优先度，该数值越大则NPC遇到敌怪时越会优先选择逃跑，反之则该NPC越好斗。
            //最小一般为1，你可以试试0或负数LOL~

            NPC.Happiness.SetNPCAffection(NPCID.Nurse, AffectionLevel.Love);
            NPC.Happiness.SetNPCAffection(NPCID.Princess, AffectionLevel.Love);
            NPC.Happiness.SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Like);
            NPC.Happiness.SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike);
            NPC.Happiness.SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate);

            NPC.Happiness.SetBiomeAffection<JungleBiome>(AffectionLevel.Love);
            NPC.Happiness.SetBiomeAffection<ForestBiome>(AffectionLevel.Like);
            NPC.Happiness.SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike);
            NPC.Happiness.SetBiomeAffection<CrimsonBiome>(AffectionLevel.Hate);
            NPC.Happiness.SetBiomeAffection<CorruptionBiome>(AffectionLevel.Hate);
            NPC.Happiness.SetBiomeAffection<DungeonBiome>(AffectionLevel.Hate);

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = -1f,
                Direction = -1
            };
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            //如果你想写敌对NPC也行
            NPC.width = 22;
            //碰撞箱宽
            NPC.height = 32;
            //碰撞箱高            
            NPC.aiStyle = 7;
            //必带项，如果你能自己写出城镇NPC的AI可以不带
            NPC.damage = 10;
            //碰撞伤害，由于城镇NPC没有碰撞伤害所以可以忽略
            NPC.defense = 150;
            //防御力
            NPC.lifeMax = Main.expertMode ? 500 : 150;
            //生命值
            NPC.HitSound = SoundID.NPCHit1;
            //受伤音效
            NPC.DeathSound = SoundID.NPCDeath1;
            //死亡音效
            NPC.knockBackResist = 0.3f;
            //抗击退性，数字越大抗性越低
            AnimationType = NPCID.Guide;

            if (NPC.downedMechBossAny)
            {
                NPC.lifeMax = Main.expertMode ? 1000 : 300;
            }

            if (NPC.downedMoonlord)
            {
                NPC.lifeMax = Main.expertMode ? 1500 : 450;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.AdvancedModDLC.Bestiary.Chemist")
            });
        }

        public override List<string> SetNPCNameList()
        {
            string[] names = { "Lavoisier", "CH3COOH", "Oxygen","CH3COOCH2CH3","Side" };
            return new List<string>(names);
        }

        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            //该入住条件为：护士存在且有至少10个NPC
            return NPC.AnyNPCs(NPCID.Nurse) && numTownNPCs >= 10 && ModContent.GetInstance<AdvancedDLCConfig>().ChemistSpawn;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            {
                if (!Main.bloodMoon && !Main.eclipse)
                {
                    //无家可归时
                    if (NPC.homeless)
                    {
                        chat.Add("我已经设计好了一个绝妙的实验，但是我的实验室离我太远了。");
                    }
                    else
                    {
                        chat.Add($"{Main.LocalPlayer.name}，你知道吗？我有几乎所有的药剂！");
                    }
                }
                //日食时
                if (Main.eclipse)
                {
                    chat.Add("太阳去哪里了？？？这样我的反应就无法进行了！！！");
                }
                //血月时
                if (Main.bloodMoon)
                {
                    chat.Add("这红色的月亮让水中掺杂了杂质，我的实验又失败了。");
                }
                if (Main.raining)
                {
                    chat.Add("雨天的雨水，正是最好的溶剂！");
                }
                if (NPC.FindFirstNPC(NPCID.Nurse) >= 0 )
                {
                    chat.Add($"{Main.npc[NPCID.Nurse].GivenName}和我都一直为泰拉世界的医药学而努力。");
                }
                return chat;
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            //翻译“商店文本”
            switch (shopNum)
            {
                case 1:
                    button = "商店（原版药水1）";
                    break;
                case 2:
                    button = "商店（原版药水2）";
                    break;
                case 3:
                    button = "商店（模组药水）";
                    break;
            }
            button2 = "切换商店";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            int[] debuffs = {30,20,24,70,22,80,35,23,31,32,197,33,36,195,196,37,38,39,69,46,47,103,149,156,164,163,144,148,145,94,21,88,68,67,25,119,120,86,194,199 };
            //如果按下第一个按钮，则开启商店
            if (firstButton)
            {
                shop = true;
            }
            else if (!firstButton)
            {
                if (shopNum == 3)
                {
                    shopNum = 1;
                }
                else
                {
                    shopNum++;
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (shopNum == 1)
            {
                shop.item[nextSlot].SetDefaults(ItemID.AmmoReservationPotion);
                //设置价格
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ArcheryPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BattlePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BuilderPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CalmingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.CratePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.EndurancePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FeatherfallPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FishingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(2329);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FlipperPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GillsPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GravitationPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HunterPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.InfernoPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HeartreachPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.InvisibilityPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.IronskinPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LifeforcePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LovePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MagicPowerPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ManaRegenerationPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MiningPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NightOwlPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ObsidianSkinPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RagePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RegenerationPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ShinePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SonarPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SpelunkerPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Diamond);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Topaz);
                shop.item[nextSlot].value = 500;
                nextSlot++;
            }
            else if (shopNum == 2)
            {
                shop.item[nextSlot].SetDefaults(ItemID.StinkPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SummoningPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SwiftnessPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ThornsPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.TitanPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WarmthPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WaterWalkingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WrathPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RecallPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.TeleportationPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Mushroom);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.BottledHoney);
                shop.item[nextSlot].value = 500;
                shop.item[nextSlot].SetDefaults(ItemID.BottledWater);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HealingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LesserManaPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ManaPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GreaterManaPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Honeyfin);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RedPotion);
                shop.item[nextSlot].value = 500;
                nextSlot++;
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee && Main.hardMode, ItemID.FlaskofCursedFlames, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofFire, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofGold, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee && Main.hardMode, ItemID.FlaskofIchor, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofNanites, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofParty, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofPoison, 500);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.FlaskofVenom, 500);
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
                    shop.item[nextSlot].value = 500;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                    shop.item[nextSlot].value = 500;
                    nextSlot++;
                }
                if (NPC.AnyNPCs(NPCID.SantaClaus))
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Eggnog);
                    shop.item[nextSlot].value = 500;
                    nextSlot++;
                }
                if (NPC.AnyNPCs(NPCID.SkeletonMerchant))
                {
                    shop.item[nextSlot].SetDefaults(ItemID.StrangeBrew);
                    shop.item[nextSlot].value = 500;
                    nextSlot++;
                }

            }
            else if (shopNum == 3)
            {
                Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("AdvancedMod", "GodBlood").Type, 500);
                Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("AdvancedMod", "WonderPotion").Type, 500);

                if (ModLoader.TryGetMod("CalamityMod",out Mod calamity))
                {
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "AnechoicCoating").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "AstralInjection").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "BoundingPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "AureusCell").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "CalciumPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "CeaselessHungerPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "CrumblingPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "DraconicElixir").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "GravityNormalizerPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "HolyWrathPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "PenumbraPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "PhotosynthesisPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "ProfanedRagePotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "ShatteringPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "ShadowPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "SoaringPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "SulphurskinPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "TeslaPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "TitanScalePotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "TriumphPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "ZenPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "ZergPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "CadancePotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "PotionofOmniscience").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "YharimsStimulants").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "CalamitasBrew").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "Bloodfin").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "SupremeHealingPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "OmegaHealingPotion").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "HadalStew").Type, 500);
                    Tool.AddItem(ref shop, ref nextSlot, true, ModContent.Find<ModItem>("CalamityMod", "SupremeManaPotion").Type, 500);
                }
            }
            
            //设置商品
            
        }
        /*
        //设置该NPC的近战/抛射物伤害和击退（取决于NPC攻击类型）
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 55;
            knockback = 3f;
        }
        */
        //NPC攻击一次后的间隔
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 1;
            randExtraCooldown = 1;
            //间隔的算法：实际间隔会大于或等于cooldown的值且总是小于cooldown+randExtraCooldown的总和（TR总整这些莫名其妙的玩意）
        }

        //弹幕设置
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!Main.hardMode)
            {
                projType = ProjectileID.FlamingArrow;
                //使用烈焰箭的弹幕
                attackDelay = 120;
                //NPC在出招后多长时间才会发射弹幕
            }
            else
            {
                projType = ProjectileID.IchorArrow;
                //使用灵液箭的弹幕
                attackDelay = 60;
                //NPC在出招后多长时间才会发射弹幕
            }
        }

        //射手NPC专属：手持武器（选带项）
        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
        {
            if (!Main.hardMode)
            {
                scale = 1f;
                //大小
                item = ItemID.PlatinumBow;
                //手持武器类型，铂金弓
                closeness = 18;
                //武器更接近NPC(以像素为单位, 数字越大离NPC越近, 当武器位置不对时调整)
            }
            else
            {
                scale = 1f;
                //大小
                item = ItemID.Tsunami;
                //手持武器类型，海啸
                closeness = 18;
                //武器更接近NPC(以像素为单位, 数字越大离NPC越近, 当武器位置不对时调整)
            }
        }
    }
}
