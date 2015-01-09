using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace LittleRedSharpie
{
    class SKOJarvanIV
    {
        public const string ChampionName = "JarvanIV";
        public static Orbwalking.Orbwalker Orbwalker;
        public static Spell Q, W, E, R;
        public static List<Spell> SpellList = new List<Spell>();
        public static Items.Item HDR, BKR, BWC, YOU, DFG, SOD, RO, TMT;
        public static Menu Config;
        private static Obj_AI_Hero Player;
        public static SpellSlot smiteSlot = SpellSlot.Unknown;
        public static SpellSlot igniteSlot = SpellSlot.Unknown;
        public static Spell smite;
        public static Spell ignite;
        public static bool ult;
        public SKOJarvanIV()
        {
            Player = ObjectManager.Player;
            if (Player.BaseSkinName != ChampionName) return;


            Q = new Spell(SpellSlot.Q, 770f);
            W = new Spell(SpellSlot.W, 525f);
            E = new Spell(SpellSlot.E, 800f);
            R = new Spell(SpellSlot.R, 650f);

            Q.SetSkillshot(0.5f, 70, float.MaxValue, false, SkillshotType.SkillshotLine);
            E.SetSkillshot(0.5f, 75, float.MaxValue, false, SkillshotType.SkillshotCircle);
            //R.SetSkillshot(0.5f, 325, 0, false, SkillshotType.SkillshotCircle);

            SpellList.Add(Q);
            SpellList.Add(W);
            SpellList.Add(E);
            SpellList.Add(R);

            BKR = new Items.Item(3153, 450f);
            BWC = new Items.Item(3144, 450f);
            YOU = new Items.Item(3142, 300f);
            DFG = new Items.Item(3128, 750f);
            RO = new Items.Item(3143, 500f);

            igniteSlot = Player.GetSpellSlot("SummonerDot");

            Config = new Menu(ChampionName, "SKOJarvanIV", true);

            //TargetSelector
            var targetSelector = new Menu("Target Selector", "Target Selector");
            TargetSelector.AddToMenu(targetSelector);
            Config.AddSubMenu(targetSelector);

            //Orbwalker
            Config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalking"));

            //Combo
            Config.AddSubMenu(new Menu("Combo", "Combo"));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseQCombo", "Use Q")).SetValue(true);
            Config.SubMenu("Combo").AddItem(new MenuItem("UseWCombo", "Use W")).SetValue(true);
            Config.SubMenu("Combo").AddItem(new MenuItem("UseECombo", "Use E")).SetValue(true);
            Config.SubMenu("Combo").AddItem(new MenuItem("UseRCombo", "Use R")).SetValue(true);
            Config.SubMenu("Combo").AddItem(new MenuItem("MinEnemys", "Enemys R").SetValue(new Slider(2, 5, 1)));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseItems", "Use Items")).SetValue(true);
            Config.SubMenu("Combo").AddItem(new MenuItem("ActiveCombo", "Combo!").SetValue(new KeyBind(32, KeyBindType.Press)));

            //Harass
            Config.AddSubMenu(new Menu("Harass", "Harass"));
            Config.SubMenu("Harass").AddItem(new MenuItem("UseQHarass", "Use Q")).SetValue(true);
            Config.SubMenu("Harass").AddItem(new MenuItem("UseWHarass", "Use W")).SetValue(true);
            Config.SubMenu("Harass").AddItem(new MenuItem("UseEHarass", "Use E")).SetValue(true);
            Config.SubMenu("Harass").AddItem(new MenuItem("ActiveHarass", "Harass key").SetValue(new KeyBind("X".ToCharArray()[0], KeyBindType.Press)));

            //Farm
            Config.AddSubMenu(new Menu("Lane Clear", "Lane"));
            Config.SubMenu("Lane").AddItem(new MenuItem("UseQLane", "Use Q")).SetValue(true);
            Config.SubMenu("Lane").AddItem(new MenuItem("UseELane", "Use E")).SetValue(true);
            Config.SubMenu("Lane").AddItem(new MenuItem("ActiveLane", "Lane Key").SetValue(new KeyBind(Config.Item("LaneClear").GetValue<KeyBind>().Key, KeyBindType.Press)));

            //Kill Steal
            Config.AddSubMenu(new Menu("KillSteal", "Ks"));
            Config.SubMenu("Ks").AddItem(new MenuItem("ActiveKs", "Use KillSteal")).SetValue(true);
            Config.SubMenu("Ks").AddItem(new MenuItem("UseQKs", "Use Q")).SetValue(true);
            Config.SubMenu("Ks").AddItem(new MenuItem("UseEKs", "Swagflag Enemy?")).SetValue(true);
            Config.SubMenu("Ks").AddItem(new MenuItem("UseIgnite", "Use Ignite")).SetValue(true);

            //Drawings
            Config.AddSubMenu(new Menu("Drawings", "Drawings"));
            Config.SubMenu("Drawings").AddItem(new MenuItem("DrawQ", "Draw Q")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("DrawW", "Draw W")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("DrawE", "Draw E")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("DrawR", "Draw R")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("CircleLag", "Lag Free Circles").SetValue(true));
            Config.SubMenu("Drawings").AddItem(new MenuItem("CircleQuality", "Circles Quality").SetValue(new Slider(100, 100, 10)));
            Config.SubMenu("Drawings").AddItem(new MenuItem("CircleThickness", "Circles Thickness").SetValue(new Slider(1, 10, 1)));

            Config.AddToMainMenu();

            Game.OnGameUpdate += Game_OnGameUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Hero.OnCreate += OnCreateObj;
            Obj_AI_Hero.OnDelete += OnDeleteObj;

            Game.PrintChat(string.Format("<font color='#F7A100'>{0} - {1} loaded (FIXED).</font>", Assembly.GetExecutingAssembly().GetName().Name, Program.ChampionName));

        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            Orbwalker.SetAttack(true);
            HDR = new Items.Item(3074, Player.AttackRange + 50);
            TMT = new Items.Item(3077, Player.AttackRange + 50);
            var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Physical);

            if (Config.Item("ActiveCombo").GetValue<KeyBind>().Active)
            {
                Combo(target);
            }
            if (Config.Item("ActiveHarass").GetValue<KeyBind>().Active)
            {
                Harass(target);
            }
            if (Config.Item("ActiveKs").GetValue<bool>())
            {
                KillSteal(target);
            }
            if (Config.Item("ActiveLane").GetValue<KeyBind>().Active)
            {
                Farm();
            }
        }

        static void Combo(Obj_AI_Hero target)
        {
            if (target != null)
            {
                if (Config.Item("UseECombo").GetValue<bool>() && E.IsReady() && Player.Distance(target.Position) <= Q.Range)
                {
                    E.Cast(target, true);
                }
                if (Config.Item("UseQCombo").GetValue<bool>() && Q.IsReady())
                {
                    Q.Cast(target, true);
                }
                if (Config.Item("UseWCombo").GetValue<bool>() && W.IsReady())
                {
                    W.Cast();
                }

                if (Config.Item("UseRCombo").GetValue<bool>() && R.IsReady() && !ult)
                {
                    if (GetEnemys(target) >= Config.Item("MinEnemys").GetValue<Slider>().Value)
                    {
                        R.Cast(target);
                    }
                }
                if (Config.Item("UseItems").GetValue<bool>())
                {
                    BKR.Cast(target);
                    YOU.Cast(target);
                    BWC.Cast(target);
                    DFG.Cast(target);
                    SOD.Cast(target);
                    RO.Cast(target);
                    HDR.Cast(target);
                    TMT.Cast(target);
                }
            }

        }

        private static void Harass(Obj_AI_Hero target)
        {
            if (target != null)
            {
                if (Config.Item("UseEHarass").GetValue<bool>() && E.IsReady() && Player.Distance(target.Position) <= Q.Range)
                {
                    E.Cast(target, true);
                }
                if (Config.Item("UseQHarass").GetValue<bool>() && Q.IsReady())
                {
                    Q.Cast(target, true);
                }
                if (Config.Item("UseWHarass").GetValue<bool>() && W.IsReady())
                {
                    W.Cast(target);
                }
            }

        }

        private static void KillSteal(Obj_AI_Hero target)
        {
            //var Qdmg = Q.GetDamage(target, DamageLib.StageType.Default);
            var Qdmg = ObjectManager.Player.GetSpellDamage(target, SpellSlot.Q);
            var Edmg = ObjectManager.Player.GetSpellDamage(target, SpellSlot.E);
            var igniteDmg = ObjectManager.Player.GetSummonerSpellDamage(target, Damage.SummonerSpell.Ignite);
            //var Edmg = E.GetDamage(target, DamageLib.StageType.Default);
            //var igniteDmg = DamageLib.getDmg(target, DamageLib.SpellType.IGNITE);

            if (target != null)
            {
                if (Config.Item("UseQKs").GetValue<bool>() && Q.IsReady())
                {
                    if (target.Health <= Qdmg)
                        Q.Cast(target);
                }

                if (Config.Item("UseEKs").GetValue<bool>() && E.IsReady())
                {
                    if (target.Health <= Edmg)
                        E.Cast(target);
                }
            }
        }
            
        private static int GetEnemys(Obj_AI_Hero target)
        {
            int Enemys = 0;
            foreach (Obj_AI_Hero enemys in ObjectManager.Get<Obj_AI_Hero>())
            {

                var pred = R.GetPrediction(enemys, true);
                if (pred.Hitchance >= HitChance.High && !enemys.IsMe && enemys.IsEnemy && Vector3.Distance(Player.Position, pred.UnitPosition) <= R.Range)
                {
                    Enemys = Enemys + 1;
                }
            }
            return Enemys;
        }

        private static void Farm()
        {
            var allMinionsQ = MinionManager.GetMinions(ObjectManager.Player.ServerPosition, Q.Range);
            var useQl = Config.Item("UseQLane").GetValue<bool>();
            if (Q.IsReady() && useQl)
            {
                var fl2 = Q.GetLineFarmLocation(allMinionsQ, Q.Width);

                if (fl2.MinionsHit >= 3)
                {
                    Q.Cast(fl2.Position);
                }
                else
                    foreach (var minion in allMinionsQ)
                        if (!Orbwalking.InAutoAttackRange(minion) &&
                            minion.Health < 0.75 * Player.GetSpellDamage(minion, SpellSlot.Q))
                            Q.Cast(minion);
            }
        }

        static void Drawing_OnDraw(EventArgs args)
        {
            if (Config.Item("CircleLag").GetValue<bool>())
            {
                if (Config.Item("DrawQ").GetValue<bool>())
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, Q.Range, System.Drawing.Color.Green,
                        Config.Item("CircleThickness").GetValue<Slider>().Value,
                        Config.Item("CircleQuality").GetValue<Slider>().Value);
                }
                if (Config.Item("DrawW").GetValue<bool>())
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, W.Range, System.Drawing.Color.Blue,
                        Config.Item("CircleThickness").GetValue<Slider>().Value,
                        Config.Item("CircleQuality").GetValue<Slider>().Value);
                }
                if (Config.Item("DrawE").GetValue<bool>())
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, E.Range, System.Drawing.Color.Red,
                        Config.Item("CircleThickness").GetValue<Slider>().Value,
                        Config.Item("CircleQuality").GetValue<Slider>().Value);
                }
                if (Config.Item("DrawR").GetValue<bool>())
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, R.Range, System.Drawing.Color.White,
                        Config.Item("CircleThickness").GetValue<Slider>().Value,
                        Config.Item("CircleQuality").GetValue<Slider>().Value);
                }
            }
            else
            {
                if (Config.Item("DrawQ").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, Q.Range, System.Drawing.Color.White);
                }
                if (Config.Item("DrawW").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, W.Range, System.Drawing.Color.White);
                }
                if (Config.Item("DrawE").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, E.Range, System.Drawing.Color.White);
                }
                if (Config.Item("DrawR").GetValue<bool>())
                {
                    Drawing.DrawCircle(ObjectManager.Player.Position, R.Range, System.Drawing.Color.White);
                }

            }
        }

        private static void OnCreateObj(GameObject sender, EventArgs args)
        {
            if (!(sender is Obj_GeneralParticleEmitter)) return;
            var obj = (Obj_GeneralParticleEmitter)sender;
            if (obj != null && obj.IsMe && obj.Name == "JarvanCataclysm_tar")
            {
                ult = true;
            }

        }

        private static void OnDeleteObj(GameObject sender, EventArgs args)
        {
            if (!(sender is Obj_GeneralParticleEmitter)) return;
            var obj = (Obj_GeneralParticleEmitter)sender;
            if (obj != null && obj.IsMe && obj.Name == "JarvanCataclysm_tar")
            {
                ult = false;
            }

        }
    }
}
