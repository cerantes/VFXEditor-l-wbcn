using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Curve;

namespace VfxEditor.Formats.AvfxFormat.Particle.Data {
    public class AvfxParticleDataPolygon : AvfxData {
        public readonly AvfxCurve1Axis Count = new( "数量", "Cnt" );
        public readonly AvfxCurve1Axis CountRandom = new( "随机数量", "CntR" );

        public AvfxParticleDataPolygon() : base() {
            Parsed = [
                Count,
                CountRandom
            ];

            Tabs.Add( Count );
            Tabs.Add( CountRandom );
        }
    }
}