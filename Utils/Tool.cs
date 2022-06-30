﻿using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AdvancedModDLC.Utils
{
    public class Tool
    {

        public static int[] DifferentArray(int n, int max)
        {
            int[] l = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            for (int i = 0;i < n; i++)
            {
            a: l[i] = Main.rand.Next(max);
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (l[j] == l[i])
                        {
                            j = 0;
                            goto a;
                        }
                    }
                }
            }
            return l;
        }

        public static Vector2 TurnVector(Vector2 PreVector,float angle)
        {
            Vector2 TurnedVector = new Vector2((float)(PreVector.X * Math.Cos(angle) - PreVector.Y * Math.Sin(angle)), (float)(PreVector.Y * Math.Cos(angle) + PreVector.X * Math.Sin(angle)));

            return TurnedVector;
        }

        public static void AddItem(ref Chest shop, ref int nextSlot, bool Check, int type, int value)
        {
            if (!Check) return;
            shop.item[nextSlot].SetDefaults(type);
            shop.item[nextSlot].value = value;
            nextSlot++;
        }

        /*
        public static bool CheckBossAlive()
        {
            foreach (var npc in Main.npc)
            {
                foreach (var boss in AdvancedMod.Bosses)
                {
                    if (npc.type == boss) return true;
                }
            }

            return false;
        }
        */

        public static NPC GetClosestNPC(Vector2 position,bool boss)
        {
            int NPCIndex = 0;
            if (!boss)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Vector2.Distance(position,Main.npc[i].Center) < Vector2.Distance(position, Main.npc[NPCIndex].Center) && !Main.npc[i].friendly)
                    {
                        NPCIndex = i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Vector2.Distance(position, Main.npc[i].Center) < Vector2.Distance(position, Main.npc[NPCIndex].Center) && !Main.npc[i].friendly && Main.npc[i].boss)
                    {
                        NPCIndex = i;
                    }
                }
            }

            return Main.npc[NPCIndex];
        }

        public static bool AccessoryEquiped(int type,Player player)
        {
            foreach (var Item in player.armor)
            {
                if (type == Item.type) return true;
            }

            return false;
        }

        public static IItemDropRule BossBagDropCustom(int itemType, int amount = 1)
        {
            return new DropLocalPerClientAndResetsNPCMoneyTo0(itemType, 1, amount, amount, null);
        }

        public static void NewModItem(Vector2 spawnPos,string ModName,string ItemName,int amount = 1)
        {
            if (ModContent.TryFind(ModName,ItemName,out ModItem item))
            {
                Item.NewItem(null, spawnPos, item.Type, amount);
            }
        }

        public static int GetModItem(string modName,string itemName)
        {
            if (ModContent.TryFind(modName,itemName,out ModItem item))
            {
                return item.Type;
            }

            return 0;
        }

        public static int GetModNPC(string modName,string npcName)
        {
            if (ModContent.TryFind(modName,npcName,out ModNPC npc))
            {
                return npc.Type;
            }

            return 0;
        }

        public static int GetModBuff(string modName,string buffName)
        {
            if (ModContent.TryFind(modName,buffName,out ModBuff buff))
            {
                return buff.Type;
            }
            return 0;
        }

        public static bool HaveItem(Player player,int type)
        {
            foreach(Item item in player.inventory)
            {
                if (item.type == type) return true;
            }

            return false;
        }
    }
}
