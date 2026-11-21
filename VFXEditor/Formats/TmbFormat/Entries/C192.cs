using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Int;
using VfxEditor.Utils;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C192 : TmbEntry {
        public const string MAGIC = "C192";
        public const string DISPLAY_NAME = "播放共享组时间线";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;
        public override DangerLevel Danger => DangerLevel.Yellow;

        public override int Size => 0x38;
        public override int ExtraSize => 0;

        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedInt SharedGroupId = new( "共享组实例 Id" );
        private readonly ParsedInt Unk4 = new( "未知 4" );
        private readonly ParsedInt Unk5 = new( "未知 5" );
        private readonly ParsedInt TimelineIndex = new( "时间线索引" );
        private readonly ParsedInt Unk7 = new( "未知 7" );
        private readonly ParsedInt Unk8 = new( "未知 8" );
        private readonly ParsedInt Unk9 = new( "未知 9" );
        private readonly ParsedInt Unk10 = new( "未知 10" );
        private readonly ParsedInt Unk11 = new( "未知 11" );

        public C192( TmbFile file ) : base( file ) { }

        public C192( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Unk1,
            Unk2,
            SharedGroupId,
            Unk4,
            Unk5,
            TimelineIndex,
            Unk7,
            Unk8,
            Unk9,
            Unk10,
            Unk11
        ];
    }
}