using Terraria.ModLoader;

namespace AdvancedModDLC
{
	public class AdvancedModDLC : Mod
	{
        public override object Call(params object[] args)
        {
            if ((string)args[0] == "EnableWitherMode")
            {
                AdvancedModDLCWorld.WitherMode = true;
                return true;
            }

            return false;
        }
    }
}