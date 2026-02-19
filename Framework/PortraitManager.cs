using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HDPortraitsUltra
{
    public class PortraitManager
    {
        private readonly IMonitor monitor;
        private readonly IModHelper helper;
        private readonly ModConfig config;
        private Dictionary<string, PortraitPack> availablePacks = new();
        private Dictionary<string, string> npcCurrentPack = new();

        public PortraitManager(IMonitor monitor, IModHelper helper, ModConfig config)
        {
            this.monitor = monitor;
            this.helper = helper;
            this.config = config;
        }

        公共 void LoadAvailablePortraitPacks()
        {
            this.availablePacks.Clear();
            
            string modsPath = Path.Combine(this.helper.DirectoryPath, "..", "..");
            
            foreach (string modFolder in Directory.GetDirectories(modsPath))
            {
                string manifestPath = Path.Combine(modFolder, "manifest.json");
                if (!File.Exists(manifestPath))
                    continue;

                try
                {
                    dynamic manifest = this.helper.Data.ReadJsonFile<dynamic>(manifestPath);
                    
                    if (manifest["ContentPackFor"]?["UniqueID"] == "kiwimichelle.HDPortraits.Ultra")
                    {
                        this.LoadPortraitPackFromFolder(modFolder);
                    }
                }
                catch (Exception ex)
                {
                    this.monitor.Log($"Error loading portrait pack from {modFolder}: {ex.Message}", LogLevel.Warn);
                }
            }

            this.monitor.Log($"Loaded {this.availablePacks.Count} portrait packs", LogLevel.Info);
        }

        private void LoadPortraitPackFromFolder(string folderPath)
        {
            string portraitsJsonPath = Path.Combine(folderPath, "portraits.json");
            
            if (!File.Exists(portraitsJsonPath))
                return;

            try
            {
                string json = File.ReadAllText(portraitsJsonPath);
                // Parse and load portrait data
                this.monitor.Log($"Loaded portrait pack from {folderPath}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                this.monitor.Log($"Error parsing portraits.json: {ex.Message}", LogLevel.Error);
            }
        }

        public void RefreshPortraits()
        {
            ConditionEvaluator evaluator = new ConditionEvaluator();
            
            foreach (var npc in Game1.getAllCharacters())
            {
                if (this.npcCurrentPack.ContainsKey(npc.Name))
                {
                    string packName = this.npcCurrentPack[npc.Name];
                    // Apply portrait logic based on current conditions
                }
            }
        }

        public Dictionary<string, PortraitPack> GetAvailablePacks() => this.availablePacks;

        public void SetNPCPortraitPack(string npcName, string packName)
        {
            if (this.npcCurrentPack.ContainsKey(npcName))
                this.npcCurrentPack[npcName] = packName;
            else
                this.npcCurrentPack.Add(npcName, packName);

            this.RefreshPortraits();
        }

        public string GetNPCCurrentPack(string npcName)
        {
            return this.npcCurrentPack.ContainsKey(npcName) ? this.npcCurrentPack[npcName] : "vanilla";
        }

        public void RestoreVanillaPortraits()
        {
            this.npcCurrentPack.Clear();
            this.monitor.Log("Vanilla portraits restored", LogLevel.Info);
        }
    }

    public class PortraitPack
    {
        public string Name { get; set; }
        public string NPC { get; set; }
        public Dictionary<string, Portrait> Portraits { get; set; }
    }

    public class Portrait
    {
        public string ID { get; set; }
        public string BaseFile { get; set; }
        public List<string> Resolutions { get; set; }
        public Dictionary<string, object> Conditions { get; set; }
        public int Priority { get; set; }
        public PortraitMetadata Metadata { get; set; }
    }

    public class PortraitMetadata
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public int CropMargin { get; set; }
        public bool PreserveAspectRatio { get; set; }
    }
}
