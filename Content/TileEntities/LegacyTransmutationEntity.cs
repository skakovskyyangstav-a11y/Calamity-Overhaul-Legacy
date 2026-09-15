using Microsoft.Xna.Framework ;
using CalamityOverhaulLegacy.Content.Tiles ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs ;
using System.IO ;
using System.Collections.Generic ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;
using Terraria.ModLoader.IO ;

namespace CalamityOverhaulLegacy.Content.TileEntities
{
    internal class LegacyTransmutationEntity : ModTileEntity
    {
        internal Item[] Items = CreateEmpty() ;
        private static Item[] CreateEmpty(){Item[] r=new Item[81];for(int i=0;i<r.Length;i++)r[i]=new Item();return r;}
        public override bool IsTileValidForEntity(int x,int y){Tile tile=Framing.GetTileSafely(x,y);return tile.HasTile&&tile.TileType==ModContent.TileType<TransmutationOfMatter>();}
        public override int Hook_AfterPlacement(int i,int j,int type,int style,int direction,int alternate)
        {
            if(Main.netMode==NetmodeID.MultiplayerClient){NetMessage.SendTileSquare(Main.myPlayer,i,j,5,3);NetMessage.SendData(MessageID.TileEntityPlacement,-1,-1,null,i,j,Type);return -1;}
            return Place(i,j);
        }
        public override void SaveData(TagCompound tag){List<TagCompound> list=new();for(int i=0;i<Items.Length;i++)list.Add(ItemIO.Save(Items[i]??new Item()));tag["Items"]=list;}
        public override void LoadData(TagCompound tag){Items=CreateEmpty();if(tag.TryGet("Items",out List<TagCompound> list)){for(int i=0;i<Items.Length&&i<list.Count;i++)Items[i]=ItemIO.Load(list[i]);}}
        public override void NetSend(BinaryWriter writer){for(int i=0;i<81;i++)ItemIO.Send(Items[i]??new Item(),writer,true);}
        public override void NetReceive(BinaryReader reader){Items=CreateEmpty();for(int i=0;i<81;i++)Items[i]=ItemIO.Receive(reader,true);}
        internal void SetItems(Item[] source){for(int i=0;i<81;i++)Items[i]=source!=null&&i<source.Length?(source[i]?.Clone()??new Item()):new Item();}
        internal void DropAll(){if(Main.netMode==NetmodeID.MultiplayerClient)return;foreach(Item item in Items){if(item==null||item.IsAir)continue;Item.NewItem(new EntitySource_TileBreak(Position.X, Position.Y), new Rectangle(Position.X * 16, Position.Y * 16, 80, 48), item.Clone());}Items=CreateEmpty();}
        internal static bool TryGet(Point16 position,out LegacyTransmutationEntity entity){entity=null;if(TileEntity.ByPosition.TryGetValue(position,out TileEntity te)&&te is LegacyTransmutationEntity e){entity=e;return true;}return false;}
        internal void SendClientSync(){if(Main.netMode!=NetmodeID.MultiplayerClient)return;ModPacket packet=Mod.GetPacket();packet.Write((byte)LegacyPacketType.SupertableItems);packet.Write(ID);for(int i=0;i<81;i++)ItemIO.Send(Items[i]??new Item(),packet,true);packet.Send();}
        internal void Broadcast(int ignore=-1){if(Main.netMode!=NetmodeID.Server)return;ModPacket packet=Mod.GetPacket();packet.Write((byte)LegacyPacketType.SupertableItems);packet.Write(ID);for(int i=0;i<81;i++)ItemIO.Send(Items[i]??new Item(),packet,true);packet.Send(-1,ignore);}
        internal static void ReceiveSync(BinaryReader reader,int sender)
        {
            int id=reader.ReadInt32();Item[] items=CreateEmpty();for(int i=0;i<81;i++)items[i]=ItemIO.Receive(reader,true);
            if(!TileEntity.ByID.TryGetValue(id,out TileEntity te)||te is not LegacyTransmutationEntity entity)return;
            entity.SetItems(items);
            if(Main.netMode==NetmodeID.Server)entity.Broadcast(sender);
            if(Main.netMode==NetmodeID.MultiplayerClient&&SupertableUI.Instance!=null&&SupertableUI.Instance.BoundEntityId==id)SupertableUI.Instance.ReceiveEntityItems(items);
        }
    }
}
