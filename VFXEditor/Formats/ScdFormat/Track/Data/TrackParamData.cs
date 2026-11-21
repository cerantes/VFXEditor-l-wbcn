using System.IO;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Float;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.ScdFormat.Track.Data {
    public class TrackParamData : ScdTrackData {
        public readonly ParsedFloat Value = new( "值" );
        public readonly ParsedInt Time = new( "时间" );

        public override void Read( BinaryReader reader ) {
            Value.Read( reader );
            Time.Read( reader );
        }

        public override void Write( BinaryWriter writer ) {
            Value.Write( writer );
            Time.Write( writer );
        }

        public override void Draw() {
            Value.Draw();
            Time.Draw();
        }
    }
}