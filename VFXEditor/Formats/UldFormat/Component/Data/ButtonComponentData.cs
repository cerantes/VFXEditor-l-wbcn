using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Data {
    public class ButtonComponentData : UldGenericData {
        public ButtonComponentData() {
            Parsed.AddRange( [
                new ParsedUInt( "文本节点 ID" ),
                new ParsedUInt( "背景节点 ID" ),
            ] );
        }
    }
}