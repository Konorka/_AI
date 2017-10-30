using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceBattle.Common;
using System.Drawing;

namespace SomeNewProject
{
    public class SomeAI : IBattleClient
    {
        Random rng = new Random();
        GameItemDescriptor _ownPlanet;
        GameItemDescriptor _ship1;
        GameItemDescriptor _military;
        GameItemDescriptor _enemyPlanet;
        int _nopCounter = 0;
        bool _first = true;
        

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
            int help=0;
            var nlb = new List<BattleCommand>();

            if (_ownPlanet == null)
                return nlb;

            //if (_ship1 == null)
            //    nlb.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 });

            //else if (_nopCounter++ == 10) 
            //{
            //    nlb.Add(new CmdMove { ItemId = _ship1.ItemId, TargetX = rng.Next(0, 40), TargetY = rng.Next(0, 40) });
            //    _nopCounter = 0;
            //}
            
            //return nlb;


            //we have a unit to move with
            if (_ship1 == null)
                return new List<BattleCommand> { new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 } };
            //-----------------------------------------------------------ODA--------------------------------------------------------------//

            if (_ship1.PosX == 0 && _ship1.PosY == 12)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 12 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 12)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 16 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 16)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 0, TargetY = 16 } };
            }
            if (_ship1.PosX == 0 && _ship1.PosY == 16)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 0, TargetY = 20 } };
            }
            if (_ship1.PosX == 0 && _ship1.PosY == 20)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 20 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 20)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 24 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 24)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 0, TargetY = 24 } };
            }
            if (_ship1.PosX == 0 && _ship1.PosY == 24)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 0, TargetY = 28 } };
            }
            if (_ship1.PosX == 0 && _ship1.PosY == 28)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 28 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 28)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 39, TargetY = 32 } };
            }
            if (_ship1.PosX == 39 && _ship1.PosY == 32)
            {

                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 20, TargetY = 36 } };
            }

            //-------------------------------------------------------Vissza-----------------------------------------------------//

            if (_ship1.PosX == 20 && _ship1.PosY == 36)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 2, TargetY = 31 } };
            }
            if (_ship1.PosX == 2 && _ship1.PosY == 31)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 36, TargetY = 31 } };
            }
            if (_ship1.PosX == 36 && _ship1.PosY == 31)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 36, TargetY = 25 } };
            }
            if (_ship1.PosX == 36 && _ship1.PosY == 25)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 5, TargetY = 25 } };
            }
            if (_ship1.PosX == 5 && _ship1.PosY == 25)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 5, TargetY = 19 } };
            }
            if (_ship1.PosX == 5 && _ship1.PosY == 19)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 36, TargetY = 19 } };
            }
            if (_ship1.PosX == 36 && _ship1.PosY == 19)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 36, TargetY = 13 } };
            }
            if (_ship1.PosX == 36 && _ship1.PosY == 13)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 5, TargetY = 13 } };
            }
            if (_ship1.PosX == 5 && _ship1.PosY == 13)
            {
                _first = true;
            }

            if (_first)
            {
                _first = false;
                return new List<BattleCommand> { new CmdMove { ItemId = _ship1.ItemId, TargetX = 0, TargetY = 12 } };
            }
            if (_enemyPlanet == null)
                return nlb;
            //-----------------------------------------------MOZGATÁS VÉGE------------------------------------------------

            if (_military == null)
                nlb.Add(new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1 });

            else if (_military.PosX != _enemyPlanet.PosX && _military.PosY != _enemyPlanet.PosY)
                nlb.Add(new CmdMove { ItemId = _military.ItemId, TargetX = _enemyPlanet.PosX, TargetY = _enemyPlanet.PosY });

            else
                nlb.Add(new CmdShoot { ItemId = _military.ItemId, NumberOfUnits = _enemyPlanet.NumberOfUnits + 1, OtherItemId = _enemyPlanet.ItemId });

            return nlb;
            return new List<BattleCommand> { new CmdNop() };
        }


        //    int help = rng.Next(0, 4);
        //    switch (help)
        //    {
        //        case 1:
        //            return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 0 } };
        //        case 2:
        //            return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 39 } };
        //        case 3:
        //            return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 39 } };
        //        case 4:
        //            return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 0 } };
        //    }
        //    return new List<BattleCommand> { new CmdNop() };
        //}


        public void GiveGameItemsToClient(List<GameItemDescriptor> gameItems)
        {

            //if (_ownPlanet == null)
            //{
            //    _ownPlanet = gameItems.Find(x => x.PlayerName == ClientName); //find base planet
            //}
            //else
            //{
            //    _ship1 = gameItems.Find(x => x.PlayerName == ClientName && x.ItemType == "Ship"); //find our (only) unit
            //}

            _ownPlanet = gameItems.First(x => x.PlayerName == ClientName); //find base planet
            _enemyPlanet = gameItems.FirstOrDefault(x => x.PlayerName != ClientName && x.ItemType == "Planet");
            _ship1 = gameItems.FirstOrDefault(x => x.PlayerName == ClientName && x.ItemType == "Ship" && x.NumberOfUnits == 1); //find our (only) unit
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
