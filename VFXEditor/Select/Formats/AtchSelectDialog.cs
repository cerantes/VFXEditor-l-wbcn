using VfxEditor.Formats.AtchFormat;
using VfxEditor.Select.Tabs.Character;
using VfxEditor.Select.Tabs.Npc;
using VfxEditor.Select.Tabs.NpcID;

namespace VfxEditor.Select.Formats {
    public class AtchSelectDialog : SelectDialog {
        public AtchSelectDialog( string id, AtchManager manager, bool isSourceDialog ) : base( id, "atch", manager, isSourceDialog ) {
            GameTabs.AddRange( [
                new NpcTabAtch( this, "NPC" ),
                new CharacterTabAtch( this, "角色" ),
            ] );
        }
    }
}