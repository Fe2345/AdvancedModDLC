using Terraria;
using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace AdvancedModDLC
{
    public class AdvancedDLCConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("NPC配置")]
        [Label("允许 化学家 生成")]
        [Tooltip("此NPC售卖药剂，要求平衡或认为和炼金术师Mod冲突的可以关掉")]
        [DefaultValue(true)]
        public bool ChemistSpawn;

        [Label("允许 中间体 生成")]
        [Tooltip("此NPC售卖Boss召唤物和事件召唤物，已安装Fargo之魂的可以关掉")]
        [DefaultValue(true)]
        public bool MidbodySpawn;

        [Header("辅助性功能配置")]
        [Label("可捕捉NPC")]
        [Tooltip("启用后，NPC将可以被虫网捕捉")]
        [DefaultValue(false)]
        public bool CanCatchNPC;
    }
}
