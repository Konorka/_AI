using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceBattle.Common;
using System.Drawing;
using System.Collections.ObjectModel;

namespace SomeNewProject
{
    public class SomeAI : IBattleClient
    {
        Random rng = new Random();
        GameItemDescriptor _ownPlanet;
        GameItemDescriptor _scoutShip;
        GameItemDescriptor _military;
        GameItemDescriptor _enemyPlanet;
        Search search = new Search();
        List<int> LeftScoutList = new List<int>();
        List<int> RightScoutList = new List<int>();
        bool BnF = true;
        bool isBack = true;
        int i = 0;
        int j = 1;
        public void AddScoutLists()
        {
            LeftScoutList = search.GetScoutLeftList();  
            RightScoutList = search.GetScoutRightList();
        }

        public void _scout(int id, float tX, float tY)
        {
           nlb.Add( new CmdMove { ItemId = id, TargetX = tX, TargetY = tY }); 
        }List<BattleCommand> nlb = new List<BattleCommand>();


        public string ClientName
        {
            get { return "SomeAI"; }
        }

        public Brush ClientBrush
        {
            get { return Brushes.Green; }
        }

        public List<BattleCommand> GetCommandsFromClient()
        {
            AddScoutLists();



            //if (_ownPlanet == null)
            //    return nlb;

            //if (_scoutShip == null)
            //    nlb.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 });

            //else if (_nopCounter++ == 10)
            //{
            //    nlb.Add(new CmdMove { ItemId = _ship1.ItemId, TargetX = rng.Next(0, 40), TargetY = rng.Next(0, 40) });
            //    _nopCounter = 0;
            //}

           // return nlb;

            //we have a unit to move with
            if (_scoutShip == null)
                return new List<BattleCommand> { new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 } };
            
            //-----------------------------------------------------------ODA--------------------------------------------------------------//     
            if (BnF==true)
            {
                while (i < LeftScoutList.Count-2)
                {
                    if (_scoutShip.PosX == LeftScoutList[i] && _scoutShip.PosY == LeftScoutList[j])
                    {
                        j += 2; i += 2;
                        _scout(_scoutShip.ItemId, LeftScoutList[i], LeftScoutList[j]);
                    }

                    return nlb;
                }
                BnF = false;
            }else
            //-----------------------------------------------------------VISSZA--------------------------------------------------------------//     
            {
                if (isBack==true)
                {
                    i = LeftScoutList.Count-2;
                    j = LeftScoutList.Count-1;
                    isBack = false;
                }
                while (i > 1)
                {
                    if (_scoutShip.PosX == LeftScoutList[i] && _scoutShip.PosY == LeftScoutList[j])
                    {
                        j -= 2; i -= 2;
                        _scout(_scoutShip.ItemId, LeftScoutList[i], LeftScoutList[j]);
                    }
                    return nlb;
                }
                BnF = true;
            }

            //-----------------------------------------------MOZGATÁS VÉGE------------------------------------------------
            if (_military == null)
                nlb.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1 });

            else if (_military.PosX != _enemyPlanet.PosX && _military.PosY != _enemyPlanet.PosY)
                nlb.Add(new CmdMove { ItemId = _military.ItemId, TargetX = _enemyPlanet.PosX, TargetY = _enemyPlanet.PosY });

            else
                nlb.Add(new CmdShoot { ItemId = _military.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1, OtherItemId = _enemyPlanet.ItemId });

            return new List<BattleCommand> { new CmdNop() };
        }


       


        public void GiveGameItemsToClient(List<GameItemDescriptor> gameItems)
        {

            if (_ownPlanet == null)
            {
                _ownPlanet = gameItems.Find(x => x.PlayerName == ClientName); //find base planet
            }
            else
            {
                _scoutShip = gameItems.Find(x => x.PlayerName == ClientName && x.ItemType == "Ship"); //find our (only) unit
            }

            _ownPlanet = gameItems.First(x => x.PlayerName == ClientName); //find base planet
            _enemyPlanet = gameItems.FirstOrDefault(x => x.PlayerName != ClientName && x.ItemType == "Planet");
            _scoutShip = gameItems.FirstOrDefault(x => x.PlayerName == ClientName && x.ItemType == "Ship" && x.NumberOfUnits == 1); //find our (only) unit
            _military = gameItems.FirstOrDefault(x => x.PlayerName == ClientName && x.ItemType == "Ship" && x.NumberOfUnits > 1);
        }

        public void GiveMapSizeToClient(int sizeX, int sizeY)
        {
        }

        public void GiveMessageToClient(string msg)
        {

        }

        public void GiveRemainingTimeToClient(int seconds)
        {

        }
    }
}

//namespace SomeNewProject
//{
//    public class SomeAI : IBattleClient
//    {
//        static Random Rand = new Random();
//        GameItemDescriptor _ownPlanet;
//        GameItemDescriptor _scoot;
//        GameItemDescriptor _military;
//        GameItemDescriptor _enemyPlanet;
//        int _nopCounter = 0;
//        public List<BattleCommand> GetCommandsFromClient()
//        {
//            var cmds = new List<BattleCommand>();

//            if (_ownPlanet == null)
//                return cmds;
//            if (_scoot == null)
//                cmds.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 });
//            else if (_nopCounter++ == 10)
//            {
//                cmds.Add(new CmdMove { ItemId = _scoot.ItemId, TargetX = Rand.Next(0, 40), TargetY = Rand.Next(0, 40) });
//                _nopCounter = 0;
//            }

//            if (_enemyPlanet == null)
//                return cmds;

//            if (_military == null)
//                cmds.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1 });
//            else if (_military.PosX != _enemyPlanet.PosX && _military.PosY != _enemyPlanet.PosY)
//                cmds.Add(new CmdMove { ItemId = _military.ItemId, TargetX = _enemyPlanet.PosX, TargetY = _enemyPlanet.PosY });
//            else
//                cmds.Add(new CmdShoot { ItemId = _military.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1, OtherItemId = _enemyPlanet.ItemId });
//            return cmds;
//        }
//        public void GiveGameItemsToClient(List<GameItemDescriptor> gameItems)
//        {
//            _ownPlanet = gameItems.First(x => x.PlayerName == ClientName); //find base planet
//            _enemyPlanet = gameItems.FirstOrDefault(x => x.PlayerName != ClientName && x.ItemType == "Planet");
//            _scoot = gameItems.FirstOrDefault(x => x.PlayerName == ClientName && x.ItemType == "Ship" && x.NumberOfUnits == 1); //find our (only) unit
//            _military = gameItems.FirstOrDefault(x => x.PlayerName == ClientName && x.ItemType == "Ship" && x.NumberOfUnits > 1);
//        }
