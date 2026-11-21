using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility.Raii;
using System.Collections.Generic;
using System.Linq;
using VfxEditor.Select.Base;

namespace VfxEditor.Select.Tabs.Character {
    public class SelectedPap {
        // Idle, MoveA, MoveB
        public Dictionary<string, string> General;

        // Pose # -> Start, Loop
        public Dictionary<string, Dictionary<string, string>> Poses;

        public List<(string, uint, string)> FacePaths;

        public string GroundStart, Jmn, ChairStart, Sit, Umbrella, Pack, Torch, GatlingGun, GatlingIdle, Shovel;
        public Dictionary<string, Dictionary<string, string>> GroundSitPoses;
        public Dictionary<string, Dictionary<string, string>> ChairSitPoses;
        public Dictionary<string, Dictionary<string, string>> OrnamentPoses;
    }

    public class CharacterTabPap : SelectTab<CharacterRow, SelectedPap> {
        public CharacterTabPap( SelectDialog dialog, string name ) : base( dialog, name, "Character" ) { }

        // ===== LOADING =====

        public override void LoadData() => CharacterTab.Load( Items );

        public override void LoadSelection( CharacterRow item, out SelectedPap loaded ) {
            var general = new Dictionary<string, string>();
            var idlePath = item.GetPap( "resident/idle" );
            var movePathA = item.GetPap( "resident/move_a" );
            var movePathB = item.GetPap( "resident/move_b" );
            var deadPath = item.GetPap( "resident/original" );

            var swimOverIdlePath = item.GetPap( "resident/swo_idle" );
            var swimOverPathA = item.GetPap( "resident/swo_move_a" );
            var swimOverPathB = item.GetPap( "resident/swo_move_b" );
            var swimUnderIdlePath = item.GetPap( "resident/swu_idle" );
            var swimUnderPathA = item.GetPap( "resident/swu_move_a" );
            var swimUnderPathB = item.GetPap( "resident/swu_move_b" );
            var swimDeadPath = item.GetPap( "resident/swim_original" );

            if( Dalamud.DataManager.FileExists( idlePath ) ) general.Add( "Idle", idlePath );
            if( Dalamud.DataManager.FileExists( movePathA ) ) general.Add( "Move A", movePathA );
            if( Dalamud.DataManager.FileExists( movePathB ) ) general.Add( "Move B", movePathB );
            if( Dalamud.DataManager.FileExists( deadPath ) ) general.Add( "死亡", deadPath );

            if( Dalamud.DataManager.FileExists( swimOverIdlePath ) ) general.Add( "待机（游泳）", swimOverIdlePath );
            if( Dalamud.DataManager.FileExists( swimOverPathA ) ) general.Add( "游泳 A", swimOverPathA );
            if( Dalamud.DataManager.FileExists( swimOverPathB ) ) general.Add( "游泳 B", swimOverPathB );
            if( Dalamud.DataManager.FileExists( swimUnderIdlePath ) ) general.Add( "待机（水下）", swimUnderIdlePath );
            if( Dalamud.DataManager.FileExists( swimUnderPathA ) ) general.Add( "水下 A", swimUnderPathA );
            if( Dalamud.DataManager.FileExists( swimUnderPathA ) ) general.Add( "水下 B", swimUnderPathA );
            if( Dalamud.DataManager.FileExists( swimDeadPath ) ) general.Add( "死亡（水下）", swimDeadPath );

            var poses = new Dictionary<string, Dictionary<string, string>>();
            for( var i = 1; i <= SelectDataUtils.MaxChangePoses; i++ ) {
                var start = item.GetStartPap( i, "" );
                var loop = item.GetLoopPap( i, "" );
                if( Dalamud.DataManager.FileExists( start ) && Dalamud.DataManager.FileExists( loop ) ) {
                    poses.Add( $"姿势 {i}", new Dictionary<string, string>() {
                        { "开始", start },
                        { "Loop", loop }
                    } );
                }
            }

            var groundSitPoses = new Dictionary<string, Dictionary<string, string>>();
            for( var i = 1; i <= SelectDataUtils.MaxChangePoses; i++ ) {
                var start = item.GetStartPap( i, "j_" );
                var loop = item.GetLoopPap( i, "j_" );
                if( Dalamud.DataManager.FileExists( start ) && Dalamud.DataManager.FileExists( loop ) ) {
                    groundSitPoses.Add( $"地面坐姿 {i}", new Dictionary<string, string>() {
                        { "开始", start },
                        { "循环", loop }
                    } );
                }
            }

            var chairSitPoses = new Dictionary<string, Dictionary<string, string>>();
            for( var i = 1; i <= SelectDataUtils.MaxChangePoses; i++ ) {
                var start = item.GetStartPap( i, "s_" );
                var loop = item.GetLoopPap( i, "s_" );
                if( Dalamud.DataManager.FileExists( start ) && Dalamud.DataManager.FileExists( loop ) ) {
                    chairSitPoses.Add( $"座椅坐姿 {i}", new Dictionary<string, string>() {
                        { "开始", start },
                        { "循环", loop }
                    } );
                }
            }

            var ornamentPoses = new Dictionary<string, Dictionary<string, string>>();
            for( var i = 1; i <= SelectDataUtils.MaxChangePoses; i++ ) {
                var start = item.GetOrnamentStartPap( i, "onm_" );
                var loop = item.GetOrnamentLoopPap( i, "onm_" );
                if( Dalamud.DataManager.FileExists( start ) && Dalamud.DataManager.FileExists( loop ) ) {
                    ornamentPoses.Add( $"持伞姿势 {i}", new Dictionary<string, string>() {
                        { "开始", start },
                        { "循环", loop }
                    } );
                }
            }

            var jmn = item.GetPap( "emote/jmn" );
            var groundStart = item.GetPap( "event_base/event_base_ground_start" );
            var sit = item.GetPap( "emote/sit" );
            var chairStart = item.GetPap( "event_base/event_base_chair_start" );
            //ornaments
            var umbrella = item.GetOrnamentResidentPap( "ot_m6001/resident/ornament" );
            var pack = item.GetOrnamentResidentPap( "ot_m6008/resident/ornament" );
            var torch = item.GetOrnamentResidentPap( "ot_m6011/resident/ornament" ); //shared by lantern
            var gatling = item.GetOrnamentResidentPap( "ot_m6016/resident/ornament" );
            var gatlingIdle = item.GetOrnamentResidentPap( "bt_common/ornament_sp/m6016/onm_pose01_loop" );
            var shovel = item.GetOrnamentResidentPap( "ot_m6017/resident/ornament" );

            var facePaths = item.Data.FaceOptions
                .Select( id => (id, $"chara/human/{item.SkeletonId}/animation/f{id:D4}/resident/face.pap") )
                .Where( x => Dalamud.DataManager.FileExists( x.Item2 ) )
                .Select( x => ($"面部 {x.id}", item.Data.FaceToIcon.TryGetValue( x.id, out var icon ) ? icon : 0, x.Item2) )
                .ToList();

            loaded = new SelectedPap {
                General = general,
                Poses = poses,
                GroundSitPoses = groundSitPoses,
                ChairSitPoses = chairSitPoses,
                OrnamentPoses = ornamentPoses,
                FacePaths = facePaths,
                Jmn = Dalamud.DataManager.FileExists( jmn ) ? jmn : null,
                GroundStart = Dalamud.DataManager.FileExists( groundStart ) ? groundStart : null,
                Sit = Dalamud.DataManager.FileExists( sit ) ? sit : null,
                ChairStart = Dalamud.DataManager.FileExists( chairStart ) ? chairStart : null,
                //ornaments
                Umbrella = Dalamud.DataManager.FileExists( umbrella ) ? umbrella : null,
                Pack = Dalamud.DataManager.FileExists( pack ) ? pack : null,
                Torch = Dalamud.DataManager.FileExists( torch ) ? torch : null,
                GatlingGun = Dalamud.DataManager.FileExists( gatling ) ? gatling : null,
                GatlingIdle = Dalamud.DataManager.FileExists( gatlingIdle ) ? gatlingIdle : null,
                Shovel = Dalamud.DataManager.FileExists( shovel ) ? shovel : null,
            };
        }

        // ===== DRAWING ======

        protected override void DrawSelected() {
            using var tabBar = ImRaii.TabBar( "栏" );
            if( !tabBar ) return;

            if( ImGui.BeginTabItem( "一般" ) ) {
                Dialog.DrawPaths( Loaded.General, Selected.Name, SelectResultType.GameCharacter );
                ImGui.EndTabItem();
            }
            if( ImGui.BeginTabItem( "姿势" ) ) {
                Dialog.DrawPaths( Loaded.Poses, Selected.Name, SelectResultType.GameCharacter );
                ImGui.EndTabItem();
            }
            if( ImGui.BeginTabItem( "坐姿" ) ) {
                Dialog.DrawPaths( new Dictionary<string, string>() {
                    { "地面坐姿开始动作", Loaded.GroundStart },
                    { "地面默认坐姿", Loaded.Jmn },
                }, Selected.Name, SelectResultType.GameCharacter );

                ImGui.Separator();
                Dialog.DrawPaths( Loaded.GroundSitPoses, Selected.Name, SelectResultType.GameCharacter );

                ImGui.EndTabItem();
            }
            if( ImGui.BeginTabItem( "椅子坐姿" ) ) {
                Dialog.DrawPaths( new Dictionary<string, string>() {
                    { "Chair Start", Loaded.ChairStart },
                    { "Chair Sit Default", Loaded.Sit },
                }, Selected.Name, SelectResultType.GameCharacter );

                ImGui.Separator();
                Dialog.DrawPaths( Loaded.ChairSitPoses, Selected.Name, SelectResultType.GameCharacter );

                ImGui.EndTabItem();
            }
            if( ImGui.BeginTabItem( "时尚配饰" ) ) {
                Dialog.DrawPaths( new Dictionary<string, string>() {
                    { "Move (Umbrella)", Loaded.Umbrella },
                    { "Move (Backpack)", Loaded.Pack },
                    { "移动（火把/提灯）", Loaded.Torch },
                    { "移动（加特林机枪）", Loaded.GatlingGun },
                    { "待机（加特林机枪）", Loaded.GatlingIdle },
                    { "移动（铁锹）", Loaded.Shovel },
                }, Selected.Name, SelectResultType.GameCharacter );

                ImGui.Separator();
                Dialog.DrawPaths( Loaded.OrnamentPoses, Selected.Name, SelectResultType.GameCharacter );

                ImGui.EndTabItem();
            }
            if( ImGui.BeginTabItem( "面部" ) ) {
                Dialog.DrawPaths( Loaded.FacePaths, Selected.Name, SelectResultType.GameCharacter );
                ImGui.EndTabItem();
            }
        }
    }
}