using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace HDPortraitsUltra
{
    public class UIMenuHandler
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly PortraitManager portraitManager;
        private PortraitSelectionMenu portraitMenu;

        public UIMenuHandler(IMonitor monitor, IModHelper helper, PortraitManager portraitManager)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.portraitManager = portraitManager;
        }

        public void OpenPortraitMenu()
        {
            this.portraitMenu = new PortraitSelectionMenu(this.portraitManager);
            Game1.activeClickableMenu = this.portraitMenu;
        }
    }

    public class PortraitSelectionMenu : IClickableMenu
    {
        private readonly PortraitManager portraitManager;
        private List<string> npcList = new();
        private int selectedNPCIndex = 0;

        public PortraitSelectionMenu(PortraitManager portraitManager)
        {
            this.portraitManager = portraitManager;
            this.PopulateNPCList();
        }

        private void PopulateNPCList()
        {
            foreach (var npc in Game1.getAllCharacters())
            {
                this.npcList.Add(npc.Name);
            }
        }

        public override void draw(StardewValley.BinaryData.SpriteBatch b)
        {
            // Draw menu UI
            base.draw(b);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            base.receiveLeftClick(x, y, playSound);
        }

        public override void receiveKeyPress(StardewValley.BinaryData.Keys key)
        {
            if (key == StardewValley.BinaryData.Keys.Escape)
            {
                this.exitThisMenu();
            }
            
            base.receiveKeyPress(key);
        }
    }
}
