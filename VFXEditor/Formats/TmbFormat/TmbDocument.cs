using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Utility.Raii;
using System.IO;
using System.Numerics;
using VfxEditor.FileManager;
using VfxEditor.Spawn;
using VfxEditor.Utils;

namespace VfxEditor.Formats.TmbFormat {
    public class TmbDocument : FileManagerBasicDocument<TmbFile> {
        public override string Id => "Tmb";
        public override string Extension => "tmb";

        public uint AnimationId = 0;
        private bool AnimationDisabled => string.IsNullOrEmpty( ReplacePath ) || AnimationId == 0;

        public TmbDocument( TmbManager manager, string writeLocation ) : base( manager, writeLocation ) { }

        public TmbDocument( TmbManager manager, string writeLocation, string localPath, WorkspaceMetaBasic data ) : base( manager, writeLocation, localPath, data ) {
            AnimationId = TmbSpawn.GetIdFromTmbPath( ReplacePath );
        }

        protected override TmbFile FileFromReader( BinaryReader reader, string sourcePath, bool verify ) => new( reader, verify );

        protected override void DrawExtraColumn() {
            using var framePadding = ImRaii.PushStyle( ImGuiStyleVar.FramePadding, new Vector2( 4, 3 ) );
            using( var group = ImRaii.Group() ) {
                using( var style = ImRaii.PushStyle( ImGuiStyleVar.Alpha, ImGui.GetStyle().Alpha * 0.5f, AnimationDisabled ) ) {
                    if( ImGui.Button( "播放", new Vector2( 90, ImGui.GetFrameHeight() ) ) && !AnimationDisabled ) TmbSpawn.Apply( AnimationId );
                }

                if( ImGui.Button( "重置", new Vector2( 90, ImGui.GetFrameHeight() ) ) ) TmbSpawn.Reset();
            }

            var height = ImGui.GetItemRectSize().Y;

            using( var _ = ImRaii.PushStyle( ImGuiStyleVar.ItemSpacing, new Vector2( ImGui.GetStyle().ItemSpacing.Y, 4 ) ) ) {
                ImGui.SameLine();
            }
            Plugin.TrackerManager.Tmb.DrawEye( new Vector2( 40, height ) );
        }

        protected override float GetExtraControlsWidth() => 60 + 5;

        protected override void DrawExtraControls() {
            using var font = ImRaii.PushFont( UiBuilder.IconFont );

            using( ImRaii.Disabled( AnimationDisabled ) ) {
                if( ImGui.Button( FontAwesomeIcon.Play.ToIconString(), new Vector2( 30, ImGui.GetFrameHeight() ) ) && !AnimationDisabled ) TmbSpawn.Apply( AnimationId );
            }
            UiUtils.Tooltip( "强制你的角色播放当前选中的动画（需要同时选择源 TMB 和替换 TMB）" );

            using( var spacing = ImRaii.PushStyle( ImGuiStyleVar.ItemSpacing, new Vector2( 5f, 0f ) ) ) {
                ImGui.SameLine();
            }
            if( ImGui.Button( FontAwesomeIcon.Stop.ToIconString(), new Vector2( 30, ImGui.GetFrameHeight() ) ) ) TmbSpawn.Reset();
            UiUtils.Tooltip( "重置正在播放的动画" );
        }

        protected override void DrawBody() {
            ImGui.SetCursorPosY( ImGui.GetCursorPosY() + 5 );
            DrawAnimationWarning();
            base.DrawBody();
        }
    }
}