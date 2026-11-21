using Dalamud.Interface;
using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Components;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Flag;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C063 : TmbEntry {
        public const string MAGIC = "C063";
        public const string DISPLAY_NAME = "音效";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x20;
        public override int ExtraSize => 0;

        private readonly ParsedInt Loop = new( "循环/持续时间", value: 1 );
        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly TmbOffsetString Path = new( "路径", null, true, "sound/replace_me.scd" );
        private readonly ParsedInt SoundIndex = new( "音频索引" );
        private readonly ParsedFlag<SoundC063Flags> SoundPosition = new( "音频位置", size: 1 );
        private readonly ParsedByte BindId = new( "绑定 Id" );
        private readonly ParsedShort Unk2 = new( "未知 2" );

        public C063( TmbFile file ) : base( file ) {
            SetupIcon();
        }

        public C063( TmbFile file, TmbReader reader ) : base( file, reader ) {
            SetupIcon();
        }

        private void SetupIcon() {
            Path.Icons.Insert( 0, new() {
                Icon = () => FontAwesomeIcon.VolumeUp,
                Remove = false,
                Action = value => Plugin.ResourceLoader.PlaySound( value, SoundIndex.Value )
            } );
        }

        protected override List<ParsedBase> GetParsed() => [
            Loop,
            Unk1,
            Path,
            SoundIndex,
            SoundPosition,
            BindId,
            Unk2
        ];
    }
}