using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Data {
    public class ScrollBarComponentData : UldGenericData {
        public ScrollBarComponentData() {
            Parsed.AddRange( [
                new ParsedUInt( "未知节点 ID 1" ),
                new ParsedUInt( "未知节点 ID 2" ),
                new ParsedUInt( "未知节点 ID 3" ),
                new ParsedUInt( "未知节点 ID 4" ),
                new ParsedUInt( "边距", size: 2 ),
                new ParsedByteBool( "是否垂直" ),
                new ParsedInt( "内边距", size: 1 ),
            ] );
        }
    }
}