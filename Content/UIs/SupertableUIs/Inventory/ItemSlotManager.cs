using System.Collections.Generic ;
using System.Linq ;
using Terraria ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Inventory
{
    public class ItemSlotManager
    {
        private Item[] slots = new Item[SupertableConstants.TOTAL_SLOTS] ;
        private Item[] preview = new Item[SupertableConstants.TOTAL_SLOTS] ;
        public ItemSlotManager() { for (int i=0;i<slots.Length;i++) { slots[i]=new Item() ; preview[i]=new Item() ; } }
        public Item GetSlot(int i) => i>=0&&i<slots.Length ? slots[i] : null ;
        public void SetSlot(int i, Item item) { if(i>=0&&i<slots.Length) slots[i]=item??new Item() ; }
        public Item GetPreviewSlot(int i) => i>=0&&i<preview.Length ? preview[i] : null ;
        public void SetPreviewSlot(int i, Item item) { if(i>=0&&i<preview.Length) preview[i]=item??new Item() ; }
        public void SetPreviewFromTypes(int[] types) { if(types==null)return; for(int i=0;i<preview.Length;i++) preview[i]=new Item(i<types.Length?types[i]:0) ; }
        public IEnumerable<(int index, Item item)> GetNonEmptySlots() { for(int i=0;i<slots.Length;i++) if(!slots[i].IsAir) yield return(i,slots[i]) ; }
        public bool HasAnyItems() => slots.Any(i=>!i.IsAir) ;
        public void ClearSlot(int i) { if(i>=0&&i<slots.Length) slots[i].TurnToAir() ; }
        public int[] GetAllItemTypes() { int[] r=new int[slots.Length]; for(int i=0;i<r.Length;i++)r[i]=slots[i].type; return r; }
        public int GetMinimumStackSize() { int min=int.MaxValue; foreach(Item item in slots)if(!item.IsAir&&item.stack<min)min=item.stack; return min==int.MaxValue?1:min; }
        public void ConsumeItems(int amount) { foreach(Item item in slots) if(!item.IsAir){item.stack-=amount;if(item.stack<=0)item.TurnToAir();} }
        public ref Item[] Slots => ref slots ;
        public Item[] PreviewSlots => preview ;
    }
}
