using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Components.Enums;
using static VfxEditor.Formats.AvfxFormat.Base.Enums;

namespace VfxEditor.Formats.AvfxFormat.Particle.Data {
    public class AvfxParticleDataWindmill : AvfxDataWithParameters {
        public readonly AvfxEnum<WindmillUVType> WindmillUVType = new( "风车 UV 映射类型", "WUvT" );

        public AvfxParticleDataWindmill() : base() {
            Parsed = [
                WindmillUVType
            ];

            ParameterTab.Add( WindmillUVType );
        }
    }
}