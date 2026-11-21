using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C118 : TmbEntry {

        public const string MAGIC = "C118";
        public const string DISPLAY_NAME = "禁用物理";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x18;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedEnum<PhysicsType> Type = new( "类型", size: 2 );
        private readonly ParsedBool Unk3 = new( "未知 3", size: 2 );

        public C118( TmbFile file ) : base( file ) { }

        public C118( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk2,
            Type,
            Unk3
        ];
    }
}