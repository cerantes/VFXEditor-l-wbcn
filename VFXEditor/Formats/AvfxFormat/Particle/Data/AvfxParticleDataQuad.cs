using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Components.Bool;
using VfxEditor.Formats.AvfxFormat.Components.Int;

namespace VfxEditor.Formats.AvfxFormat.Particle.Data {
    public class AvfxParticleDataQuad : AvfxDataWithParameters {
        public readonly AvfxInt SS = new( "Scaling Scale", "SS" );
        public readonly AvfxBool bMP = new( "为移动粒子", "bMP" );

        public AvfxParticleDataQuad() : base( true ) {
            Parsed = [
                SS,
                bMP
            ];

            ParameterTab.Add( SS );
            ParameterTab.Add( bMP );
        }
    }
}