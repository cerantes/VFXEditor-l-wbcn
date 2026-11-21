using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C198 : TmbEntry {
        public const string MAGIC = "C198";
        public const string DISPLAY_NAME = "Lemure";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x28;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedInt Unk3 = new( "未知 3" );
        private readonly ParsedEnum<SummonId> SummonId = new( "召唤物 Id", size: 1 );
        private readonly ParsedEnum<C198AtchState> AtchState = new( "ATCH 状态", size: 1 );
        private readonly ParsedShort Unk4 = new( "未知 4" );
        private readonly ParsedShort ModelId = new( "模型 ID" );
        private readonly ParsedShort BodyId = new( "身体 ID" );
        private readonly ParsedInt Variant = new( "分支" );

        public C198( TmbFile file ) : base( file ) { }

        public C198( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk1,
            Unk2,
            Unk3,
            SummonId,
            AtchState,
            Unk4,
            ModelId,
            BodyId,
            Variant
        ];
    }
}