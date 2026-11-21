using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Utility.Raii;
using System.IO;
using System.Linq;
using System.Numerics;
using VfxEditor.Data;
using VfxEditor.FileManager;
using VfxEditor.Formats.AvfxFormat.Texture;
using VfxEditor.Select;
using VfxEditor.Spawn;
using VfxEditor.Utils;

namespace VfxEditor.Formats.AvfxFormat {
    public class AvfxDocument : FileManagerRenamedDocument<AvfxFile> {
        public override string Id => "Vfx";
        public override string Extension => "avfx";

        private string SpawnPath => ReplacePath;
        private bool SpawnDisabled => string.IsNullOrEmpty( SpawnPath );

        public AvfxDocument( AvfxManager manager, string writeLocation ) : base( manager, writeLocation ) { }

        public AvfxDocument( AvfxManager manager, string writeLocation, string localPath, WorkspaceMetaRenamed data ) : base( manager, writeLocation, localPath, data ) { }

        public override void CheckKeybinds() {
            base.CheckKeybinds();

            if( Plugin.Configuration.SpawnOnSelfKeybind.KeyPressed() ) {
                VfxSpawn.Clear();
                if( !SpawnDisabled ) VfxSpawn.OnSelf( SpawnPath, true );
            }

            if( Plugin.Configuration.SpawnOnGroundKeybind.KeyPressed() ) {
                VfxSpawn.Clear();
                if( !SpawnDisabled ) VfxSpawn.OnGround( SpawnPath, true );

            }

            if( Plugin.Configuration.SpawnOnTargetKeybind.KeyPressed() ) {
                VfxSpawn.Clear();
                if( !SpawnDisabled ) VfxSpawn.OnTarget( SpawnPath, true );
            }
        }

        protected override AvfxFile FileFromReader( BinaryReader reader, string sourcePath, bool verify ) => new( reader, verify );

        public void Import( string path ) {
            if( File != null && System.IO.File.Exists( path ) ) File.Import( path );
        }

        public void OpenTemplate( string path ) =>
            SetSource( new SelectResult( SelectResultType.Local, "", "[模板]", Path.Combine( Plugin.RootLocation, "Files", path ) ) );

        // ========= DRAWING =============

        protected override void DrawExtraColumn() {
            var space = ImGui.GetContentRegionAvail();
            ImGui.SetCursorPosX( ImGui.GetCursorPosX() + ( space.X - 30 ) );
            Plugin.TrackerManager.Vfx.DrawEye( new Vector2( 30, ImGui.GetFrameHeight() * 2 + ImGui.GetStyle().ItemSpacing.Y ) );
        }

        protected override string GetWarningText() {
            var invalidTimeline = File.TimelineView.Group.Items.FirstOrDefault( timeline => timeline.Items.Any( item => !item.HasValue ) );
            if( invalidTimeline == null ) return "";
            return $"时间线 [{invalidTimeline.GetText()}] 缺少数值";
        }

        protected override void DrawControls() {
            base.DrawControls();

            if( File.TextureView.Group.Items.Where( x => !x.FileExists() ).Any() ) {
                ImGui.SameLine();
                using( var font = ImRaii.PushFont( UiBuilder.IconFont ) ) {
                    if( UiUtils.RemoveButton( FontAwesomeIcon.Images.ToIconString() ) ) {
                        var paths = File.TextureView.Group.Items
                            .Where( x => !x.FileExists() )
                            .Select( x => x.Path.Value.Trim( '\0' ) )
                            .Where( x => !string.IsNullOrEmpty( x ) ).ToList();

                        Plugin.AddModal( new ImportMissingTexturesModal( paths ) );
                    }
                }
                UiUtils.Tooltip( "导入缺失材质" );
            }
        }

        protected override float GetExtraControlsWidth() => 60 + 5;

        protected override void DrawExtraControls() {
            using( ImRaii.PushFont( UiBuilder.IconFont ) ) {
                using( ImRaii.Disabled( SpawnDisabled ) ) {
                    if( ImGui.Button( FontAwesomeIcon.PaintBrush.ToIconString(), new Vector2( 30, ImGui.GetFrameHeight() ) ) && !SpawnDisabled ) ImGui.OpenPopup( "SpawnPopup" );
                }
                UiUtils.Tooltip( "将选中的特效生成到游戏世界中（需要同时选择源特效和替换特效）" );

                using( var spacing = ImRaii.PushStyle( ImGuiStyleVar.ItemSpacing, new Vector2( 5f, 0f ) ) ) {
                    ImGui.SameLine();
                }
                if( ImGui.Button( FontAwesomeIcon.Book.ToIconString(), new Vector2( 30, ImGui.GetFrameHeight() ) ) ) Plugin.LibraryManager.Show();
                UiUtils.Tooltip( "打开特效组件与材质库" );
            }

            if( !SpawnDisabled ) VfxSpawn.DrawPopup( SpawnPath, true );
        }
    }
}