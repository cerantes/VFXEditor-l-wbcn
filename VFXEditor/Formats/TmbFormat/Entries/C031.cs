using System.Collections.Generic;
using VfxEditor.Data;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;
using VfxEditor.Parsing.Sheets;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C031 : TmbEntry {
        public const string MAGIC = "C031";
        public const string DISPLAY_NAME = "召唤动画";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x18;
        public override int ExtraSize => 0;

        private readonly ParsedBool Enabled = new( "启用" );
        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedSheetShort Animation = new( "动画", SheetData.WeaponTimelines, SheetData.InitWeaponTimelines );
        private readonly ParsedEnum<ObjectControl> TargetType = new( "目标类型", size: 2 );

        public C031( TmbFile file ) : base( file ) { }

        public C031( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Enabled,
            Unk1,
            Animation,
            TargetType
        ];
    }
}