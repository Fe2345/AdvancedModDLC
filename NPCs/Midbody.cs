using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using System.Collections.Generic;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.Bestiary;
using AdvancedModDLC.Utils;

namespace AdvancedModDLC.NPCs
{
    public class Midbody : ModNPC
    {
        public static int shopNum = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("中间体");
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

            NPC.Happiness.SetNPCAffection<Chemist>(AffectionLevel.Love);
            NPC.Happiness.SetNPCAffection(NPCID.Princess, AffectionLevel.Love);
            NPC.Happiness.SetNPCAffection(NPCID.Steampunker, AffectionLevel.Like);
            NPC.Happiness.SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Dislike);
            NPC.Happiness.SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate);

            NPC.Happiness.SetBiomeAffection<ForestBiome>(AffectionLevel.Love);
            NPC.Happiness.SetBiomeAffection<SnowBiome>(AffectionLevel.Like);
            NPC.Happiness.SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike);
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
            NPC.defense = 191;
            //防御力
            NPC.lifeMax = 114514;
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
                new FlavorTextBestiaryInfoElement("Mods.AdvancedModDLC.Bestiary.Midbody")
            });
        }

        public override List<string> SetNPCNameList()
        {
            string[] names = { "Fergo", "Ben", "Boki", "Initalize", "Nine" ,"Calamitas"};
            return new List<string>(names);
        }

        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            //该入住条件为：护士存在且有至少10个NPC
            return (NPC.downedBoss1 || NPC.downedGoblins) && ModContent.GetInstance<AdvancedDLCConfig>().MidbodySpawn;
        }

        public override string GetChat()
        {
            List<string> chat = new List<string> { 
                "买点东西！丛林孢子，999个！",
                "你绝不知道为了收集这些商品我花了多长时间",
                "想要挑战我？或许你该问问SwordOfWar",
                "那个天上的大眼球去哪里了？或许你把它打败了？或许没有？哦，我的伙计，这一个眼珠子就值30金！",
                "1个血腥脊椎相当于50金！"
                };
            return Main.rand.Next(chat);
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            //翻译“商店文本”
            switch (shopNum)
            {
                case 1:
                    button = "Boss召唤物商店";
                    break;
                case 2:
                    button = "宝藏袋商店";
                    break;
                case 3:
                    button = "事件召唤物商店";
                    break;
                case 4:
                    button = "基础材料商店";
                    break;
            }
            button2 = "切换商店";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            int[] debuffs = { 30, 20, 24, 70, 22, 80, 35, 23, 31, 32, 197, 33, 36, 195, 196, 37, 38, 39, 69, 46, 47, 103, 149, 156, 164, 163, 144, 148, 145, 94, 21, 88, 68, 67, 25, 119, 120, 86, 194, 199 };
            //如果按下第一个按钮，则开启商店
            if (firstButton)
            {
                shop = true;
            }
            else if (!firstButton)
            {
                if (shopNum == 4)
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
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedSlimeKing, ItemID.SlimeCrown, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss1, ItemID.SuspiciousLookingEye, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss2, ItemID.WormFood, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedSlimeKing, ItemID.BloodySpine, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.Abeemination, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss3, ItemID.ClothierVoodooDoll, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedDeerclops, ItemID.DeerThing, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.GuideVoodooDoll, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenSlime, ItemID.QueenSlimeCrystal, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss2, ItemID.MechanicalEye, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss1, ItemID.MechanicalWorm, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss3, ItemID.MechanicalSkull, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedEmpressOfLight, ItemID.EmpressButterfly, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedGolemBoss, ItemID.LihzahrdAltar, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedGolemBoss, ItemID.LihzahrdPowerCell, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedFishron, ItemID.TruffleWorm, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMoonlord, ItemID.CelestialSigil, 2000);

                Tool.AddItem(ref shop, ref nextSlot, (bool)ModLoader.GetMod("AdvancedMod").Call("downedTreeDiagrammer"), ModContent.ItemType<AdvancedMod.Items.Summon.DiagrammerWreckage>(), 2000);
                if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
                {
                    Tool.AddItem(ref shop, ref nextSlot, (bool)fargo.Call("DownedDevi"), ModContent.Find<ModItem>("FargowiltasSouls", "DevisCurse").Type, 2000);
                    Tool.AddItem(ref shop, ref nextSlot, NPC.downedMoonlord, ModContent.Find<ModItem>("FargowiltasSouls", "SigilOfChampions").Type, 2000);
                    Tool.AddItem(ref shop, ref nextSlot, (bool)fargo.Call("DownedAbom"), ModContent.Find<ModItem>("FargowiltasSouls", "AbomsCurse").Type, 2000);
                    Tool.AddItem(ref shop, ref nextSlot, (bool)fargo.Call("DownedMutant"), ModContent.Find<ModItem>("FargowiltasSouls", "MutantsCurse").Type, 2000);
                }
            }
            else if (shopNum == 2)
            {
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedSlimeKing, ItemID.KingSlimeBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss1, ItemID.EyeOfCthulhuBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss2, ItemID.EaterOfWorldsBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss2, ItemID.BrainOfCthulhuBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenBee, ItemID.QueenBeeBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss3, ItemID.SkeletronBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedDeerclops, ItemID.DeerclopsBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.WallOfFleshBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedQueenSlime, ItemID.QueenSlimeBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss2, ItemID.TwinsBossBag,2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss1, ItemID.DestroyerBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBoss3, ItemID.SkeletronPrimeBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedPlantBoss, ItemID.PlanteraBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedEmpressOfLight, ItemID.FairyQueenBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedGolemBoss, ItemID.GolemBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedFishron, ItemID.FishronBossBag, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMoonlord, ItemID.MoonLordBossBag, 2000);
            }
            else if (shopNum == 3)
            {
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.BloodMoonStarter, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedGoblins, ItemID.GoblinBattleStandard, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss2, ItemID.DD2ElderCrystalStand, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss2, ItemID.DD2ElderCrystal, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedPirates, ItemID.PirateMap, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.SnowGlobe, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedHalloweenTree || NPC.downedHalloweenKing, ItemID.PumpkinMoonMedallion, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedChristmasTree || NPC.downedChristmasSantank || NPC.downedChristmasIceQueen, ItemID.NaughtyPresent, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedMechBossAny, ItemID.SolarTablet, 2000);
            }
            else if (shopNum == 4)
            {
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.JungleSpores, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.StoneBlock, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.Lens, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.Stinger, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.Vine, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedBoss3, ItemID.Bone, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.SharkFin, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.TatteredCloth, 2000);
                Tool.AddItem(ref shop, ref nextSlot, true, ItemID.Obsidian, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.TurtleShell, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.FrozenTurtleShell, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.SoulofLight, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.SoulofNight, 2000);
                Tool.AddItem(ref shop, ref nextSlot, Main.hardMode, ItemID.SoulofFlight, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedTowerSolar, ItemID.FragmentSolar, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedTowerVortex, ItemID.FragmentVortex, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedTowerNebula, ItemID.FragmentNebula, 2000);
                Tool.AddItem(ref shop, ref nextSlot, NPC.downedTowerStardust, ItemID.FragmentStardust, 2000);

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
