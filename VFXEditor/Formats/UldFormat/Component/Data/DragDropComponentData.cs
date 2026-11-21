using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Data {
    public class DragDropComponentData : UldGenericData {
        public DragDropComponentData() {
            Parsed.AddRange( [
                new ParsedUInt( "未知节点 ID 1" ),
            ] );
        }
    }
}