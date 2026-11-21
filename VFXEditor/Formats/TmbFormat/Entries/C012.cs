using Dalamud.Interface;
using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Components;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Flag;
using VfxEditor.Parsing.Int;
using VfxEditor.Spawn;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C012 : TmbEntry {
        public const string MAGIC = "C012";
        public const string DISPLAY_NAME = "视效";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x48;
        public override int ExtraSize => 4 * ( 3 + 3 + 3 + 4 );

        private readonly ParsedInt Duration = new( "持续时间", value: 30 );
        private readonly ParsedInt Unk1 = new( "未知 1" );
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
        private readonly ParsedEnum<BindUser> BindOrigin2 = new( "绑定点 2 原点", size: 1 );
        private readonly ParsedEnum<BindType> BindType2 = new( "绑定点 2 类型", size: 1 );
        private readonly ParsedShort BindId2 = new( "绑定点 2", value: -1 );
        private readonly TmbOffsetFloat3 Scale = new( "缩放", defaultValue: new( 1 ) );
        private readonly TmbOffsetAngle3 Rotation = new( "旋转" );
        private readonly TmbOffsetFloat3 Position = new( "位置" );
        private readonly TmbOffsetFloat4 RGBA = new( "RGBA", defaultValue: new( 1 ) );
        private readonly ParsedFlag<VfxC012Visibility> Visibility = new( "可见性" );
        private readonly ParsedInt Unk3 = new( "未知 3" );

        public C012( TmbFile file ) : base( file ) { }

        public C012( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk1,
            Path,
            BindOrigin1,
            BindType1,
            BindId1,
            BindOrigin2,
            BindType2,
            BindId2,
            Scale,
            Rotation,
            Position,
            RGBA,
            Visibility,
            Unk3
        ];
    }
}