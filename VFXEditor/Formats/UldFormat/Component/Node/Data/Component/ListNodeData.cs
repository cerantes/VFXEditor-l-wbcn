using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.UldFormat.Component.Node.Data.Component {
    public class ListNodeData : UldNodeComponentData {
        public ListNodeData() : base() {
            Parsed.AddRange( [
                new ParsedUInt( "行", size: 2 ),
                new ParsedUInt( "列", size: 2 ),
            ] );
        }
    }
}