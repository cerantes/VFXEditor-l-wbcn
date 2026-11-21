using Dalamud.Bindings.ImGui;
using System.IO;

namespace VfxEditor.Formats.MtrlFormat.Data.Table {
    // https://github.com/Ottermandias/Penumbra.GameData/blob/e93093f/Files/Materials/OpaqueColorTable.cs

    public class MtrlTableOpaque : MtrlTableBase {
        private readonly int DimensionLogs;
        private readonly byte[] Data;

        public MtrlTableOpaque( MtrlFile file, BinaryReader reader, long dataEnd, int dimensionLogs ) : base( file ) {
            DimensionLogs = dimensionLogs;
            Data = reader.ReadBytes( ( int )( dataEnd - reader.BaseStream.Position ) );
        }

        public override bool IsValidDimensionLogs( int logs ) => logs == DimensionLogs;

        public override int DefaultDimensionLogs => DimensionLogs;

        public override void Write( BinaryWriter writer ) {
            if( File.ColorTableEnabled ) writer.Write( Data );
        }

        public override void Draw() {
            ImGui.TextDisabled( $"不支持的颜色表尺寸（0x{DimensionLogs:X2}，{Data.Length} 字节），将原样保存" );
        }
    }
}