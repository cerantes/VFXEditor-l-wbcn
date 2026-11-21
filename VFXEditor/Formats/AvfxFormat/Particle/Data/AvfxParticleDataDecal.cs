using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Components.Float;
using VfxEditor.Formats.AvfxFormat.Components.Int;

namespace VfxEditor.Formats.AvfxFormat.Particle.Data {
    public class AvfxParticleDataDecal : AvfxDataWithParameters {
        public readonly AvfxFloat ScalingScale = new( "缩放比例", "SS" );
        public readonly AvfxInt DDTT = new( "地形类型", "DDTT" );

        public AvfxParticleDataDecal() : base() {
            Parsed = [
                ScalingScale,
                DDTT,
            ];

            ParameterTab.Add( ScalingScale );
            ParameterTab.Add( DDTT );
        }
    }
}