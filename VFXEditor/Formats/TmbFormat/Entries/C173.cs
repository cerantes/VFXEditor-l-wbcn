using Dalamud.Interface;
using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Components;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Bool;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;
using VfxEditor.Spawn;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C173 : TmbEntry {
        public const string MAGIC = "C173";
        public const string DISPLAY_NAME = "Async VFX";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x44;
        public override int ExtraSize => 0;

        private readonly ParsedInt Loop = new( "循环 / 等待", value: 1 );
        private readonly ParsedInt Unk2 = new( "未知 2" );
        private readonly TmbOffsetString Path = new( "路径", [
            new() {
                Icon = () => VfxSpawn.IsActive ? FontAwesomeIcon.Times : FontAwesomeIcon.Eye,
                Remove = false,
                Action = path => {
                    if( VfxSpawn.IsActive ) VfxSpawn.Clear();
                    else VfxSpawn.OnSelf( path, false );
                }
            }
        ], false, "vfx/replace_me.avfx" );
        private readonly ParsedEnum<BindUser> BindOrigin1 = new( "绑定点 1 原点", size: 1 );
        private readonly ParsedEnum<BindType> BindType1 = new( "绑定点 1 类型", size: 1 );
        private readonly ParsedShort BindId1 = new( "绑定点 1", value: -1 );
        private readonly ParsedBool Visibility = new( "覆盖战斗特效设置" );
        private readonly ParsedInt Limit = new( "实例上限（1-8）" );
        private readonly ParsedInt Unk5 = new( "未知 5" );
        private readonly ParsedInt Unk6 = new( "未知 6" );
        private readonly ParsedInt Unk7 = new( "未知 7" );
        private readonly ParsedInt Unk8 = new( "未知 8" );
        private readonly ParsedInt Unk9 = new( "未知 9" );
        private readonly ParsedInt Unk10 = new( "未知 10" );
        private readonly ParsedInt Unk11 = new( "未知 11" );
        private readonly ParsedInt Unk12 = new( "未知 12" );

        public C173( TmbFile file ) : base( file ) { }

        public C173( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Loop,
            Unk2,
            Path,
            BindOrigin1,
            BindType1,
            BindId1,
            Visibility,
            Limit,
            Unk5,
            Unk6,
            Unk7,
            Unk8,
            Unk9,
            Unk10,
            Unk11,
            Unk12
        ];
    }
}