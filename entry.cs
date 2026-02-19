using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;

namespace HDPortraitsUltra
{
    public class ModEntry : Mod
    {
        private PortraitManager portraitManager;
        private UIMenuHandler menuHandler;
        private ModConfig config;

        public override void Entry(IModHelper helper)
        {
            this.config = helper.ReadConfig<ModConfig>();
            
            this.portraitManager = new PortraitManager(this.Monitor, helper, this.config);
            this.menuHandler = new UIMenuHandler(this.Monitor, helper, portraitManager);

            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
            
            this.Monitor.Log("HDPortraits-Ultra loaded successfully!", LogLevel.Info);
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (e.Button.ToString() == this.config.Hotkey)
            {
                this.menuHandler.OpenPortraitMenu();
            }
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            this.portraitManager.RefreshPortraits();
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            this.portraitManager.LoadAvailablePortraitPacks();
        }
    }
}
