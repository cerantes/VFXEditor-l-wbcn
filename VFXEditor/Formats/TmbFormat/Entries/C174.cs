using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C174 : TmbEntry {
        public const string MAGIC = "C174";
        public const string DISPLAY_NAME = "物体控制";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x28;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly ParsedEnum<AtchState> InitialPosition = new( "初始位置" );
        private readonly ParsedEnum<ObjectControl> ObjectControl = new( "物体控制" );
        private readonly ParsedEnum<ObjectControlFinal> FinalPosition = new( "最终位置" );
        private readonly ParsedBool PositionDelay = new( "位置延迟" );
        private readonly ParsedInt Unk6 = new( "未知 6" );

        public C174( TmbFile file ) : base( file ) { }

        public C174( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk2,
            InitialPosition,
            ObjectControl,
            FinalPosition,
            PositionDelay,
            Unk6
        ];
    }
}