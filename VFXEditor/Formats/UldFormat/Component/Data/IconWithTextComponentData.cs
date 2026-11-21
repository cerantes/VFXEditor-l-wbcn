using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Data {
    public class IconWithTextComponentData : UldGenericData {
        public IconWithTextComponentData() {
            Parsed.AddRange( [
                new ParsedUInt( "未知节点 ID 1" ),
                new ParsedUInt( "未知节点 ID 2" ),
            ] );
        }
    }
}