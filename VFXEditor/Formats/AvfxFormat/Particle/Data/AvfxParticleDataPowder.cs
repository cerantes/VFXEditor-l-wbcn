using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Components.Bool;
using VfxEditor.Formats.AvfxFormat.Components.Enums;
using VfxEditor.Formats.AvfxFormat.Components.Float;
using static VfxEditor.Formats.AvfxFormat.Base.Enums;

namespace VfxEditor.Formats.AvfxFormat.Particle.Data {
    public class AvfxParticleDataPowder : AvfxDataWithParameters {
        public readonly AvfxBool MV = new( "跟随角色移动", "bMV" );
        public readonly AvfxBool Loc = new( "使用角色位置", "bLoc" );
        public readonly AvfxBool IsLightning = new( "是否点亮", "bLgt" );
        public readonly AvfxEnum<DirectionalLightType> DirectionalLightType = new( "定向光源类型", "LgtT" );
        public readonly AvfxFloat CenterOffset = new( "中心偏移", "CnOf" );

        public AvfxParticleDataPowder() : base() {
            Parsed = [
                MV,
                Loc,
                IsLightning,
                DirectionalLightType,
                CenterOffset
            ];

            ParameterTab.Add( MV );
            ParameterTab.Add( Loc );
            ParameterTab.Add( DirectionalLightType );
            ParameterTab.Add( IsLightning );
            ParameterTab.Add( CenterOffset );
        }
    }
}