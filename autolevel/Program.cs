//Thanks to iSnorflake for tutorial and Trelli for updater


using System;
using LeagueSharp;
using LeagueSharp.Common;


namespace AutoLevelup
{
    class Program
    {
        public static int[] abilitySequence;
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        public static string tipo = "";
        private static SpellSlot Smite;
        private static SpellSlot Heal;
        public static Obj_AI_Hero Player = ObjectManager.Player; // Instead of typing ObjectManager.Player you can just type Player
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            AutoLevelupUpdater.InitializeAutoLevelup();
            Smite = ObjectManager.Player.GetSpellSlot("SummonerSmite");
            Heal = ObjectManager.Player.GetSpellSlot("SummonerHeal");
            if (Player.ChampionName  == "Aatrox") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Ahri") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Akali") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Alistar") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Amumu") abilitySequence = new int[] { 3, 1, 2, 3, 2, 4, 3, 3, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Anivia") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Annie") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Ashe") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Azir") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 1, 2, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Blitzcrank") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Brand") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Braum") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Caitlyn") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Cassiopeia") abilitySequence = new int[] { 1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Chogath") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Corki")
            {
                if (Player.PercentMagicDamageMod > Player.PercentPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " AD";
                }
            }
            else if (Player.ChampionName  == "Darius") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Diana") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "DrMundo") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Draven") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Elise")
            {
                rOff = -1;
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Evelynn") abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Ezreal")
            {
                if (Heal != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " AD";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " AP";
                }
            }
            else if (Player.ChampionName  == "Ekko") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "FiddleSticks") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Fiora") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Fizz") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Galio") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Gangplank") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Garen") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Gnar") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Gragas") abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Graves") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Hecarim") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Heimerdinger") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Irelia") abilitySequence = new int[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Janna") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "JarvanIV")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Jax") abilitySequence = new int[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Jayce") { abilitySequence = new int[] { 1, 3, 2, 1, 1, 2, 1, 3, 1, 3, 1, 3, 3, 2, 2, 3, 2, 2 }; rOff = -1; }
            else if (Player.ChampionName  == "Jinx") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Karma") { abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 }; rOff = -1; }
            else if (Player.ChampionName  == "Karthus") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Kassadin") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Katarina") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Kalista") abilitySequence = new int[] { 3, 1, 3, 2, 3, 4, 1, 3, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Kayle")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Kennen") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Khazix")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "KogMaw")
            {
                if (Heal != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " AD";
                }
                else
                {
                    abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    tipo = " AP";
                }
            }
            else if (Player.ChampionName  == "Leblanc") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 3, 4, 3, 3, 3, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "LeeSin")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Leona") abilitySequence = new int[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Lissandra") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Lucian") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Lulu") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Lux") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Malphite") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Malzahar") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Maokai")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 2, 1, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "MasterYi") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "MissFortune") abilitySequence = new int[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Mordekaiser") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Morgana") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Nami") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Nasus")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 1, 2, 1, 4, 1, 1 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Nautilus") abilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Nidalee") { abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 }; rOff = -1; }
            else if (Player.ChampionName  == "Nocturne")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Nunu")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Olaf") abilitySequence = new int[] { 2, 1, 3, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Orianna") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Pantheon") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Poppy") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Quinn") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Rammus") abilitySequence = new int[] { 2, 1, 3, 2, 3, 4, 2, 3, 3, 3, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Renekton") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Rengar")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Riven")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Rumble") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "RekSai")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Ryze") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 2, 4, 2, 2, 2, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Sejuani") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 3, 4, 3, 3, 3, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Shaco")
            {
                if (Player.PercentMagicDamageMod > Player.PercentPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " AD";
                }
            }
            else if (Player.ChampionName  == "Shen") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Shyvana") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Singed") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Sion") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Sivir") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Skarner")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 3, 4, 3, 2, 2, 3, 4, 3, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 3, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Sona") abilitySequence = new int[] { 1, 2, 3, 1, 2, 4, 1, 2, 1, 2, 4, 1, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Soraka") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Swain") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Syndra") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Talon") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Taric") abilitySequence = new int[] { 3, 2, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "TahmKench") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Teemo") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Thresh") abilitySequence = new int[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Tristana") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Trundle")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 3, 4, 2, 2, 2, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Tryndamere") abilitySequence = new int[] { 3, 2, 1, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "TwistedFate") 
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 3, 3, 2, 3, 4, 3, 2, 3, 2, 4, 2, 4, 4, 4, 4, 4, 4 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Twitch") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Udyr")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 4, 1, 3, 4, 4, 3, 4, 3, 4, 3, 3, 1, 1, 1, 1, 2, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 3, 1, 2, 1, 2, 3, 2, 3, 3, 2, 4, 4, 4 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Urgot") abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Varus") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Vayne")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[]  { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Veigar") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Velkoz") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "Vi")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Viktor") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Vladimir") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Volibear") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.ChampionName  == "Warwick") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "MonkeyKing") abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 3, 1, 3, 1, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Xerath") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.ChampionName  == "XinZhao")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 2, 4, 2, 3, 2, 3, 4, 2, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Yasuo") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Yorick") abilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 1, 4, 2, 1, 2, 1, 4, 2, 1 };
            else if (Player.ChampionName  == "Zac")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 3, 1, 4, 3, 1, 3, 1, 4, 3, 1, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    tipo = " Lane";
                }
            }
            else if (Player.ChampionName  == "Zed") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 3, 1, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Ziggs") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Zilean") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.ChampionName  == "Zyra") abilitySequence = new int[] { 3, 2, 1, 3, 1, 4, 3, 1, 3, 1, 4, 3, 1, 2, 2, 4, 2, 2 };

            Game.OnUpdate += Game_OnUpdate;
            Game.PrintChat("<font color='#C80046'>AutoLevelup Updated by AAc</font>");
            Game.PrintChat(Player.ChampionName  + tipo + " Loaded");
        }

        static void Game_OnUpdate(EventArgs args)
        { //AutoLevelup
            int qL = Player.Spellbook.GetSpell(SpellSlot.Q).Level + qOff;
            int wL = Player.Spellbook.GetSpell(SpellSlot.W).Level + wOff;
            int eL = Player.Spellbook.GetSpell(SpellSlot.E).Level + eOff;
            int rL = Player.Spellbook.GetSpell(SpellSlot.R).Level + rOff;
            if (qL + wL + eL + rL < ObjectManager.Player.Level)
            {
                int[] level = new int[] { 0, 0, 0, 0 };
                for (int i = 0; i < ObjectManager.Player.Level; i++)
                {
                    level[abilitySequence[i] - 1] = level[abilitySequence[i] - 1] + 1;
                }
                if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);

            }
        }
    }
}