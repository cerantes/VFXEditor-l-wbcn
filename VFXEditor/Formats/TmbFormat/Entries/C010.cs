using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility.Raii;
using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Components;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Flag;
using VfxEditor.Parsing.Float;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C010 : TmbEntry {
        public const string MAGIC = "C010";
        public const string DISPLAY_NAME = "动画";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x28;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedBool TimeControl = new( "使用时间控制", size: 1 );
        private readonly ParsedFlag<C010Unk2Flags> Unk3 = new( "未知 3", size: 2 );
        private readonly ParsedFlag<C010Unk3Flags> Unk4 = new( "未知 4", size: 1 ); //cfxf
        private readonly ParsedFloat AnimationStart = new( "动画起始帧" );
        private readonly ParsedFloat AnimationEnd = new( "动画结束帧" );
        private readonly TmbOffsetString Path = new( "路径" );
        private readonly ParsedInt Unk2 = new( "未知 2" );

        public C010( TmbFile file ) : base( file ) { }

        public C010( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk1,
            TimeControl,
            Unk3,
            Unk4,
            AnimationStart,
            AnimationEnd,
            Path,
            Unk2
        ];

        public override void DrawBody() {
            DrawHeader();
            Unk1.Draw();
            Path.Draw();
            Unk2.Draw();
            TimeControl.Draw();

            using( var disabled = ImRaii.Disabled( TimeControl.Value == false ) ) {
                Duration.Draw();
                AnimationStart.Draw();
                AnimationEnd.Draw();
            }

            //jank, but works for now
            ImGui.Columns( 2 );
            Unk3.Draw();
            ImGui.NextColumn();
            Unk4.Draw();
            ImGui.Columns( 1 );
        }
    }
}