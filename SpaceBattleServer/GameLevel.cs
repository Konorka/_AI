using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace SpaceBattle.Common
{
    public class GameLevel
    {
        const int GAMETIME = 5 * 60; // in seconds
        const bool FOGOFWAR = true;
        const bool ALLMAP = false;

        // TRUE: game over if there are two players left, and one is the AI
        // FALSE: game over only if there is only one player left
        const bool ALLOW_FINISH_IF_AI_PLUS_ONE = false;

        // TRUE: continue playing even if the single player alive is the AI
        // FALSE: finish the game based on the other conditions
        const bool FORCE_PLAYING_IF_ONLY_AI = false;

        // TRUE: game over even if there are (unoccupied planets left)
        // FALSE: game over only if all planets are taken - this overrides the other game over conditions
        const bool ALLOW_FINISH_EVEN_IF_EMPTIES = false;

        static Random R = new Random();
        object lockObj = new object();

        int NumPlayers;
        int NumPlanets;
        string ForcePlayers;
        Timer timeMeasurementTimer = new Timer();
        int moveInterval;
        string FileName;

        public event EventHandler<string> SystemMessage;
        public Dictionary<int, GameItem> GameItems { get; private set; }
        public Dictionary<string, Player> GamePlayers { get; private set; }
        public float currentTime { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public List<GameItem> GameItemsCopy
        {
            get
            {
                lock (lockObj)
                {
                    return GameItems.Values.ToList();
                }
            }
        }

        void OnSystemMessage(string what)
        {
            if (SystemMessage != null) SystemMessage(this, what);
            foreach (Player akt in GamePlayers.Values)
            {
                akt.PlayerClient.GiveMessageToClient(what);
            }
        }

        public bool IsRunning
        {
            get { return timeMeasurementTimer.Enabled; }
        }

        public GameLevel(string filename, int moveInterval)
        {
            this.moveInterval = moveInterval;
            FileName = "MAPS\\" + filename;
            timeMeasurementTimer.Interval = 10 * 1000;
            timeMeasurementTimer.Tick += RemainingTime_Tick;
            Reset();
        }
        public void Reset()
        {
            GameItems = new Dictionary<int, GameItem>();
            GamePlayers = new Dictionary<string, Player>();
            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\" + FileName);
            SizeX = ini.IniReadInt("level", "sizex", 0);
            SizeY = ini.IniReadInt("level", "sizey", 0);
            NumPlayers = ini.IniReadInt("level", "players", 0);
            NumPlanets = ini.IniReadInt("level", "planets", 0);
            ForcePlayers = ini.IniReadValue("level", "forceplayers", "");
            for (int i = 1; i <= NumPlanets; i++)
            {
                LoadPlanet(ini, i);
            }
        }

        void CheckEndOfGame()
        {
            var MyList = GameItemsCopy;
            var empties = MyList.Where(x => x.ItemPlayer == null);
            var taken = MyList.Where(x => x.ItemPlayer != null);
            var players = taken.Select(x => x.ItemPlayer).Distinct();

            bool only_one_player_left = players.Count() == 1;
            bool ai_is_alive = players.Count(x => x.PlayerClient.ClientName == "AI") > 0;
            bool ai_is_alone = only_one_player_left && ai_is_alive;
            bool ai_plus_one = players.Count() == 2 && ai_is_alive;
            bool empties_are_left = empties.Count() != 0;

            bool over_empties = !empties_are_left || ALLOW_FINISH_EVEN_IF_EMPTIES;
            bool over_singleplayer = only_one_player_left &&
                            !(FORCE_PLAYING_IF_ONLY_AI && ai_is_alone && empties_are_left);
            bool over_players = over_singleplayer ||
                            (ALLOW_FINISH_IF_AI_PLUS_ONE && ai_plus_one);

            if (over_players && over_empties)
            {
                currentTime = 0;
                RemainingTime_Tick(this, EventArgs.Empty);
            }
        }
        public void CalcEndOfGame(bool save)
        {
            foreach (Player akt in GamePlayers.Values)
            {
                akt.FinalEndPlanets = 0;
                akt.FinalEndUnits = 0;
            }
            foreach (GameItem akt in GameItemsCopy)
            {
                if (akt.ItemPlayer != null)
                {
                    if (akt is Planet) akt.ItemPlayer.FinalEndPlanets++;
                    akt.ItemPlayer.FinalEndUnits += akt.NumberOfUnits;
                }
            }
            if (save)
            {
                string s = "";
                foreach (Player akt in GamePlayers.Values)
                {
                    s += akt.PointStr;
                }
                string prefix = DateTime.Now.ToShortDateString() + " " +
                    DateTime.Now.ToLongTimeString() + "\r\n" +
                    Player.PointPrefix;
                s = prefix + s + "\r\n\r\n";
                File.AppendAllText("results.tsv", s);
            }
        }

        void RemainingTime_Tick(object sender, EventArgs e)
        {
            currentTime = Math.Max(0,
                currentTime - (float)(timeMeasurementTimer.Interval) / 1000);
            OnSystemMessage("REMAINING TIME: " + currentTime);
            if (currentTime <= 0)
            {
                OnSystemMessage("GAME OVER");
                CalcEndOfGame(true);
                timeMeasurementTimer.Enabled = false;
                timeMeasurementTimer.Stop();
                Reset();
            }
        }

        private void LoadPlanet(IniFile ini, int PlanetNum)
        {
            string planet = "planet" + PlanetNum;
            int owner = ini.IniReadInt(planet, "owner", 0);
            int units = ini.IniReadInt(planet, "units", 0);
            int inc = ini.IniReadInt(planet, "inc", 0);
            int posx = ini.IniReadInt(planet, "posx", 0);
            int posy = ini.IniReadInt(planet, "posy", 0);
            AddGameItem(new Planet(null, posx, posy, units, inc, owner, moveInterval));
        }

        void SetPlayerNumbers()
        {
            string[] forcedPlayers = ForcePlayers.Split(',');
            if (forcedPlayers.Length <= 1)
            {
                List<string> playersList = GamePlayers.Keys.ToList();
                foreach (int akt in Enumerable.Range(1, playersList.Count))
                {
                    int playerIdx;
                    do
                    {
                        playerIdx = R.Next(playersList.Count);
                    } while (GamePlayers[playersList[playerIdx]].PlayerNumber > 0);
                    GamePlayers[playersList[playerIdx]].PlayerNumber = akt;
                }
            }
            else
            {
                for (int i = 0; i < forcedPlayers.Length; i++)
                {
                    if (GamePlayers.ContainsKey(forcedPlayers[i]))
                    {
                        GamePlayers[forcedPlayers[i]].PlayerNumber = i + 1;
                    }
                }
            }
            foreach (Player aktPlayer in GamePlayers.Values)
            {
                TakePlanets(aktPlayer);
            }
        }

        void TakePlanets(Player p)
        {
            foreach (GameItem aktItem in GameItemsCopy)
            {
                if (aktItem is Planet &&
                    (aktItem as Planet).FutureOwnerId == p.PlayerNumber)
                {
                    aktItem.ItemPlayer = p;
                }
            }
            //if (p.PlayerClient.ClientName == "Geri") //22as
            //{
            //    int i = 0;
            //    while (GameItemsCopy[i].ItemId != 22)
            //        i++;

            //    GameItemsCopy[i].ItemPlayer = p;
            //    (GameItemsCopy[i] as Planet).FutureOwnerId = 2;
            //}
            //else //17es
            //{
            //    int i = 0;
            //    while (GameItemsCopy[i].ItemId != 17)
            //        i++;

            //    GameItemsCopy[i].ItemPlayer = p;
            //    (GameItemsCopy[i] as Planet).FutureOwnerId = 1;
            //}
        }

        public void AddPlayer(Player newPlayer)
        {
            if (newPlayer != null && newPlayer.PlayerClient != null)
            {
                newPlayer.InitMap(SizeX, SizeY);
                newPlayer.PlayerClient.GiveMapSizeToClient(SizeX, SizeY);
                GamePlayers.Add(newPlayer.PlayerClient.ClientName, newPlayer);
            }
        }

        public void Start()
        {
            SetPlayerNumbers();
            currentTime = GAMETIME;
            timeMeasurementTimer.Enabled = true;
            timeMeasurementTimer.Start();
            OnSystemMessage("GAME START");
        }

        public void AddGameItem(GameItem newItem)
        {
            if (newItem != null)
            {
                lock (lockObj)
                {
                    GameItems.Add(newItem.ItemId, newItem);
                }
            }
        }

        public List<GameItemDescriptor> GetItemList(Player aktPlayer, List<GameItem> taskLocalList)
        {
            List<GameItemDescriptor> result = new List<GameItemDescriptor>();

            if (FOGOFWAR) aktPlayer.ResetMap();
            foreach (GameItem aktItem in taskLocalList)
                if (aktItem.ItemPlayer == aktPlayer)
                    aktPlayer.DoVisible(aktItem);

            lock (lockObj)
            {
                foreach (GameItem aktItem in taskLocalList)
                {
                    if ((ALLMAP || aktPlayer.IsVisible(aktItem)) &&
                         !(aktItem is Ship && aktItem.NumberOfUnits <= 0))
                    {
                        result.Add(aktItem.GetDesc());
                    }
                }
            }
            return result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public void OneTick()
        {
            if (IsRunning)
            {
                List<GameItem> items = GameItemsCopy;
                foreach (var akt in items)
                {
                    akt.OneTick();
                }
                foreach (Player aktPlayer in GamePlayers.Values)
                {
                    string nick = aktPlayer.PlayerClient.ClientName;
                    if (!aktPlayer.ExecRunning)
                    {
                        aktPlayer.ExecRunning = true;
                        aktPlayer.ExecSTW.Reset();
                        aktPlayer.ExecSTW.Start();
                        Task.Run(() =>
                        {
                            aktPlayer.PlayerClient.GiveRemainingTimeToClient((int)currentTime);
                            List<GameItemDescriptor> SeenByPlayer = GetItemList(aktPlayer, GameItemsCopy);
                            aktPlayer.PlayerClient.GiveGameItemsToClient(SeenByPlayer);
                            var cmds = aktPlayer.PlayerClient.GetCommandsFromClient();
                            foreach (var aktCmd in cmds)
                            {
                                if (aktCmd is CmdMove)
                                {
                                    CmdMove m = aktCmd as CmdMove;
                                    Move(nick, m.ItemId, m.TargetX, m.TargetY);
                                }
                                else if (aktCmd is CmdShoot)
                                {
                                    CmdShoot s = aktCmd as CmdShoot;
                                    Shoot(nick, s.ItemId, s.OtherItemId, s.NumberOfUnits);
                                }
                                else if (aktCmd is CmdSplit)
                                {
                                    CmdSplit sp = aktCmd as CmdSplit;
                                    Split(nick, sp.ItemId, sp.NumberOfUnits);
                                }
                            }
                            aktPlayer.ExecCmds += cmds.Count;
                            aktPlayer.ExecNumber++;
                            aktPlayer.ExecSTW.Stop();
                            aktPlayer.ExecMs += aktPlayer.ExecSTW.ElapsedMilliseconds;
                            aktPlayer.ExecRunning = false;
                        });
                    }
                }
            }
        }

        void RealShoot(GameItem attacker, GameItem target, int num)
        {
            if (!GameItem.CanShoot(attacker, target)) 
                return;
            if (attacker.NumberOfUnits < num) 
                return;

            attacker.ChangeNumberOfUnits(-num);
            if (attacker.ItemPlayer == target.ItemPlayer)
            {
                target.ChangeNumberOfUnits(num);
            }
            else
            {
                target.ChangeNumberOfUnits(-num);
                attacker.ItemPlayer.FinalShoots += num;
                if (target.NumberOfUnits == 0) target.ItemPlayer = null;
                if (target.NumberOfUnits < 0)
                {
                    if (target is Planet)
                    {
                        attacker.ItemPlayer.FinalPlanetsCaptured++;
                        if (target.ItemPlayer != null)
                        {
                            target.ItemPlayer.FinalPlanetsLost++;
                        }
                    }
                    target.ItemPlayer = attacker.ItemPlayer;
                    target.NumberOfUnits = -target.NumberOfUnits;
                }
            }
            if (attacker.NumberOfUnits <= 0 && attacker is Ship)
            {
                GameItems.Remove(attacker.ItemId);
            }
            if (target.NumberOfUnits <= 0 && target is Ship)
            {
                GameItems.Remove(target.ItemId);
            }
            CheckEndOfGame();
        }

        private GameItem GetItem(string nick, int itemId)
        {
            if (!GameItems.ContainsKey(itemId) || !IsRunning) return null;
            GameItem akt = GameItems[itemId];
            if (akt.ItemPlayer == null || akt.ItemPlayer.PlayerClient.ClientName != nick) return null;
            return akt;
        }

        public void Split(string nick, int itemId, int numUnits)
        {
            GameItem akt = GetItem(nick, itemId);
            if (akt != null)
            {
                GameItem newItem = akt.Split(numUnits);
                AddGameItem(newItem);
            }
        }

        public void Move(string nick, int itemId, float dx, float dy)
        {
            GameItem akt = GetItem(nick, itemId);
            if (akt != null && akt is Ship && dx < SizeX && dy < SizeY)
            { // negatív irányban nem tudnak kimenni Ship.OneTick() miatt
                (akt as Ship).Move(dx, dy);
            }
        }

        public void Shoot(string nick, int itemId, int otherId, int numUnits)
        {
            lock (lockObj)
            {
                GameItem akt = GetItem(nick, itemId);
                if (akt != null && GameItems.ContainsKey(otherId))
                {
                    RealShoot(GameItems[itemId], GameItems[otherId], numUnits);
                }
            }
        }
    }
}
