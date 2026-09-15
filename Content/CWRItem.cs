using Terraria ;
using Terraria.ModLoader ;
using Terraria.ModLoader.IO ;

namespace CalamityOverhaulLegacy.Content
{
    public class CWRItem : GlobalItem
    {
        public override bool InstancePerEntity => true ;

        public const int MaxAISlot = 3 ;

        public float[] ai = new float[MaxAISlot] ;

        public string[] OmigaSnyContent ;

        public override GlobalItem Clone(Item from, Item to)
        {
            CWRItem clone = (CWRItem)base.Clone(from, to) ;
            clone.ai = (float[])ai.Clone() ;
            clone.OmigaSnyContent = OmigaSnyContent ;
            return clone ;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            if (ai != null) {
                tag["LegacyCWR_ai"] = ai ;
            }
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            if (tag.TryGet("LegacyCWR_ai", out float[] saved)
                && saved != null
                && saved.Length == MaxAISlot) {
                ai = saved ;
            }
        }
    }
}
