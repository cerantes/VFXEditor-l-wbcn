using Dalamud.Interface.Utility.Raii;
using System.Collections.Generic;
using VfxEditor.Formats.TmbFormat.Base;
using VfxEditor.Formats.TmbFormat.Utils;
using VfxEditor.Parsing;
using VfxEditor.Parsing.Enums;
using VfxEditor.Parsing.Int;

namespace VfxEditor.Formats.TmbFormat.Entries {
    public class C043 : TmbEntry {
        public const string MAGIC = "C043";
        public const string DISPLAY_NAME = "召唤武器";
        public override string DisplayName => DISPLAY_NAME;
        public override string Magic => MAGIC;

        public override int Size => 0x20;
        public override int ExtraSize => 0;

        private readonly ParsedInt Duration = new( "持续时间" );
        private readonly ParsedInt Unk1 = new( "未知 1" );
        private readonly ParsedEnum<C043Type> Type = new( "类型" );
        private readonly ParsedShort WeaponId = new( "武器 ID" );
        private readonly ParsedShort BodyId = new( "身体 ID" );
        private readonly ParsedInt VariantId = new( "分支 ID" );

        public C043( TmbFile file ) : base( file ) { }

        public C043( TmbFile file, TmbReader reader ) : base( file, reader ) { }

        protected override List<ParsedBase> GetParsed() => [
            Duration,
            Unk1,
            Type,
            WeaponId,
            BodyId,
            VariantId
        ];

        public override void DrawBody() {
            DrawHeader();
            Unk1.Draw();
            Type.Draw();

            using( var disabled = ImRaii.Disabled( Type.Value != C043Type.Use_Weapon_Id ) ) {
                WeaponId.Draw();
                BodyId.Draw();
                VariantId.Draw();
            }
        }
    }
}