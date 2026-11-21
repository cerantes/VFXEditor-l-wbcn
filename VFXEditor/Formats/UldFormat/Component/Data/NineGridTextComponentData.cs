using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Data {
    public class NineGridTextComponentData : UldGenericData {
        public NineGridTextComponentData() {
            Parsed.AddRange( [
                new ParsedUInt( "文本节点 ID" ),
                new ParsedUInt( "未知节点 ID 2" ),
            ] );
        }
    }
}