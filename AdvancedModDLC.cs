using Terraria.ModLoader;

namespace AdvancedModDLC
{
	public class AdvancedModDLC : Mod
	{
        public override object Call(params object[] args)
        {
            switch ((string)args[0])
            {
                case "EnableWitherMode":
                    AdvancedModDLCWorld.WitherMode = true;
                    return true;
                case "WitherMode":
                    return AdvancedModDLCWorld.WitherMode;
            }

            return false;
        }
    }
}