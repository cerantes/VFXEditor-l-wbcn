using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using VfxEditor.Ui;
using VfxEditor.Utils;

namespace VfxEditor.Interop.Penumbra {
    public class PenumbraRequiredWindow : DalamudWindow {
        public enum PenumbraStatus {
            OK,
            NOT_LOADED,
            MODS_DISABLED
        }

        public PenumbraStatus Status { get; private set; } = PenumbraStatus.OK;

        public PenumbraRequiredWindow() : base( "需要 Penumbra", false, new( 500, 230 ), Plugin.WindowSystem ) {
            SizeCondition = ImGuiCond.Always;
            Flags |= ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize;
        }

        private static PenumbraStatus GetStatus() {
            var ipc = Plugin.PenumbraIpc;
            if( ipc == null || !ipc.PenumbraEnabled ) return PenumbraStatus.NOT_LOADED;
            if( !ipc.ModsEnabled ) return PenumbraStatus.MODS_DISABLED;
            return PenumbraStatus.OK;
        }

        public void Tick() {
            var lastStatus = Status;
            Status = GetStatus();

            if( Status != lastStatus && Status != PenumbraStatus.OK ) {
                Show();
            }
        }

        public override void DrawBody() {
            using var style = ImRaii.PushStyle( ImGuiStyleVar.WindowPadding, new Vector2( 30f, 15f ) );
            using var child = ImRaii.Child( "PenumbraRequired", new( -1, -1 ), false, ImGuiWindowFlags.AlwaysUseWindowPadding );

            using( ImRaii.PushFont( UiBuilder.IconFont ) ) {
                var iconSize = UiUtils.GetIconSize( FontAwesomeIcon.ExclamationTriangle );
                ImGui.SetCursorPosX( ImGui.GetCursorPosX() + ( ImGui.GetContentRegionAvail().X - iconSize.X ) / 2f );
                ImGui.TextColored( UiUtils.YELLOW_ACCENT, FontAwesomeIcon.ExclamationTriangle.ToIconString() );
            }

            ImGui.SetCursorPosY( ImGui.GetCursorPosY() + 5f );

            ImGui.TextWrapped( "VFXEditor 现在使用 Penumbra 在游戏内预览更改。在安装 Penumbra 并启用模组之前，本插件无法正常工作" );

            DrawCondition( Status != PenumbraStatus.NOT_LOADED, "Penumbra 已加载", "Penumbra 未安装或未加载" );
            DrawCondition( Status == PenumbraStatus.OK, "Penumbra 模组已禁用", "Penumbra 模组已禁用" );

            ImGui.SetCursorPosY( ImGui.GetCursorPosY() + 10f );

            var size = new Vector2( ( ImGui.GetContentRegionAvail().X - ( ImGui.GetStyle().ItemSpacing.X * 2 ) ) / 3f, 30f );

            if( ImGui.Button( "插件安装器", size ) ) Dalamud.PluginInterface.OpenPluginInstallerTo( PluginInstallerOpenKind.AllPlugins, "Penumbra" );

            ImGui.SameLine();
            if( ImGui.Button( "Penumbra Github", size ) ) UiUtils.OpenUrl( "https://github.com/xivdev/Penumbra" );

            ImGui.SameLine();
            using var disabled = ImRaii.Disabled( Status == PenumbraStatus.NOT_LOADED );
            if( ImGui.Button( "打开 Penumbra", size ) ) Plugin.PenumbraIpc.OpenSettings();
        }

        private static void DrawCondition( bool condition, string successText, string failText ) {
            using var color = ImRaii.PushColor( ImGuiCol.Text, condition ? UiUtils.GREEN_COLOR : UiUtils.RED_COLOR );
            using( ImRaii.PushFont( UiBuilder.IconFont ) ) {
                ImGui.Text( condition ? FontAwesomeIcon.Check.ToIconString() : FontAwesomeIcon.Times.ToIconString() );
            }
            ImGui.SameLine();
            ImGui.Text( condition ? successText : failText );
        }

        public override bool DrawConditions() {
            return Status != PenumbraStatus.OK;
        }
    }
}