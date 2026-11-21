using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C175 : TmbEntry {
        public const string MAGIC = "C175";
        public const string DISPLAY_NAME = "物体缩放";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x28;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedEnum<AtchState> InitialScale = new( "初始缩放" );
        private readonly ParsedEnum<ObjectControl> ObjectControl = new( "物体控制" );
        private readonly ParsedEnum<ObjectControlFinal> FinalScale = new( "最终缩放" );
        private readonly ParsedBool ScaleDelay = new( "缩放延迟" );
        private readonly ParsedInt Unk7 = new( "未知 7" );

        public C175( TmbFile file ) : base( file ) { }

        public C175( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk2,
            InitialScale,
            ObjectControl,
            FinalScale,
            ScaleDelay,
            Unk7
        ];
    }
}