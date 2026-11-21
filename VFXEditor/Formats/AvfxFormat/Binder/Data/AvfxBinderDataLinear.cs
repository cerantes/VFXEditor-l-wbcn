using VfxEditor.Formats.AvfxFormat.Base;
using VfxEditor.Formats.AvfxFormat.Curve;

namespace VfxEditor.Formats.AvfxFormat.Binder.Data {
    public class AvfxBinderDataLinear : AvfxData {
        public readonly AvfxCurve1Axis CarryOverFactor = new( "传递因子", "COF" );
        public readonly AvfxCurve1Axis CarryOverFactorRandom = new( "随机传递因子", "COFR" );

        public AvfxBinderDataLinear() : base() {
            Parsed = [
                CarryOverFactor,
                CarryOverFactorRandom
            ];

            Tabs.Add( CarryOverFactor );
            Tabs.Add( CarryOverFactorRandom );
        }
    }
}