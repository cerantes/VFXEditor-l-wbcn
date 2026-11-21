using System;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Flag;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Node.Data {
    [Flags]
    public enum NodeComponentFlags {
        RepeatUp = 0x80,
        RepeatDown = 0x40,
        RepeatLeft = 0x20,
        RepeatRight = 0x10
    }

    public class UldNodeComponentData : UldGenericData {
        public UldNodeComponentData() {
            Parsed.AddRange( [
                new ParsedInt( "索引", size: 1 ),
                new ParsedInt( "上", size: 1 ),
                new ParsedInt( "下", size: 1 ),
                new ParsedInt( "左", size: 1 ),
                new ParsedInt( "右", size: 1 ),
                new ParsedInt( "指针", size: 1 ),
                new ParsedFlag<NodeComponentFlags>( "标识", size: 1 ),
                new ParsedInt( "未知", size: 1 ),
                new ParsedShort( "X 轴偏移" ),
                new ParsedShort( "Y 轴偏移" )
            ] );
        }
    }
}