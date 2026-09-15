using Terraria ;
using Terraria.Audio ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Inventory
{
    public static class ItemInteractionHandler
    {
        public static void HandleLeftClick(ref Item slot, ref Item mouse)
        {
            if(slot.IsAir&&mouse.IsAir)return;
            SoundEngine.PlaySound(SoundID.Grab) ;
            if(!slot.IsAir&&mouse.IsAir){mouse=slot;slot=new Item();return;}
            if(!slot.IsAir&&!mouse.IsAir&&slot.type==mouse.type&&slot.prefix==mouse.prefix){int total=slot.stack+mouse.stack;int move=System.Math.Min(mouse.stack,slot.maxStack-slot.stack);slot.stack+=move;mouse.stack-=move;if(mouse.stack<=0)mouse.TurnToAir();return;}
            (slot,mouse)=(mouse,slot) ;
        }
        public static void HandleRightClick(ref Item slot, ref Item mouse)
        {
            if(slot.IsAir&&mouse.IsAir)return;
            if(!slot.IsAir&&mouse.IsAir){SoundEngine.PlaySound(SoundID.Grab);mouse=slot.Clone();mouse.stack=1;slot.stack--;if(slot.stack<=0)slot.TurnToAir();return;}
            if(!mouse.IsAir&&(slot.IsAir||(slot.type==mouse.type&&slot.prefix==mouse.prefix&&slot.stack<slot.maxStack))){SoundEngine.PlaySound(SoundID.Grab);if(slot.IsAir){slot=mouse.Clone();slot.stack=1;}else slot.stack++;mouse.stack--;if(mouse.stack<=0)mouse.TurnToAir();return;}
            if(!slot.IsAir&&!mouse.IsAir){SoundEngine.PlaySound(SoundID.Grab);(slot,mouse)=(mouse,slot);}
        }
        public static void HandleDragPlace(ref Item slot, ref Item mouse)
        {
            if(slot.IsAir&&!mouse.IsAir&&mouse.stack>0){slot=mouse.Clone();slot.stack=1;mouse.stack--;if(mouse.stack<=0)mouse.TurnToAir();}
        }
        public static void GatherSameItems(Item[] slots,int target)
        {
            if(slots[target].IsAir)return; int type=slots[target].type;
            for(int i=0;i<slots.Length;i++){if(i==target||slots[i].type!=type)continue;int move=System.Math.Min(slots[i].stack,slots[target].maxStack-slots[target].stack);slots[target].stack+=move;slots[i].stack-=move;if(slots[i].stack<=0)slots[i].TurnToAir();if(slots[target].stack>=slots[target].maxStack)break;}
        }
        public static void QuickTransferToInventory(Item slot, Player player)
        {
            if(slot.IsAir)return; SoundEngine.PlaySound(SoundID.Grab); player.QuickSpawnItem(player.GetSource_Misc("LegacySupertableQuickTake"),slot.Clone(),slot.stack);slot.TurnToAir();
        }
        public static bool TryQuickPlaceRecipe(Item[] slots, Item[] preview, ref Item mouse, Player player)
        {
            bool any=false;
            for(int i=0;i<preview.Length;i++){
                int type=preview[i]?.type??0;if(type==0)continue;
                if(!mouse.IsAir&&mouse.type==type&&PlaceOne(ref slots[i],mouse)){mouse.stack--;if(mouse.stack<=0)mouse.TurnToAir();any=true;continue;}
                for(int p=0;p<player.inventory.Length;p++){Item inv=player.inventory[p];if(inv.type==type&&PlaceOne(ref slots[i],inv)){inv.stack--;if(inv.stack<=0)inv.TurnToAir();any=true;break;}}
            }
            if(any)SoundEngine.PlaySound(SoundID.Grab); return any;
        }
        private static bool PlaceOne(ref Item slot, Item source)
        {
            if(slot.IsAir){slot=source.Clone();slot.stack=1;return true;}
            if(slot.type==source.type&&slot.prefix==source.prefix&&slot.stack<slot.maxStack){slot.stack++;return true;}return false;
        }
    }
}
