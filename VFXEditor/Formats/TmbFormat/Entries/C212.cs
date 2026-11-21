using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Float;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C212 : TmbEntry {
        public const string MAGIC = "C212";
        public const string DISPLAY_NAME = "挂载武器";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x20;
        public override int ExtraSize => 0;

        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedEnum<C212WeaponSlot> Slot = new( "武器槽位" );
        private readonly ParsedBool UseCustomScale = new( "使用自定义缩放", size: 1 );
        private readonly ParsedByte Unk4 = new( "未知 4" );
        private readonly ParsedShort ScaleId = new( "缩放 Id" ); // used when Use Custom Scale is off
        private readonly ParsedFloat CustomScale = new( "自定义缩放" );

        public C212( TmbFile file ) : base( file ) { }

        public C212( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Unk1,
            Unk2,
            Slot,
            UseCustomScale,
            Unk4,
            ScaleId,
            CustomScale
        ];
    }
}