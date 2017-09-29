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
        GameItemDescriptor _ship;
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

            if (_ownPlanet == null)
                return new List<BattleCommand>();
            if (_ship == null)
                return new List<BattleCommand> { new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 } };

            //we have a unit to move with


            //-----------------------------------------------------------ODA--------------------------------------------------------------//

            if (_ship.PosX == 0 && _ship.PosY == 12)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 12 } };
            }
            if (_ship.PosX == 39 && _ship.PosY == 12)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 15 } };
            }
            if (_ship.PosX == 39 && _ship.PosY == 15)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 15 } };
            }
            if (_ship.PosX == 0 && _ship.PosY == 15)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 18 } };
            }
            if (_ship.PosX == 0 && _ship.PosY == 18)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 18 } };
            }
            if (_ship.PosX == 39 && _ship.PosY == 18)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 25 } };
            }
            if (_ship.PosX == 39 && _ship.PosY == 25)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 25 } };
            }
            if (_ship.PosX == 0 && _ship.PosY == 25)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 28 } };
            }
            if (_ship.PosX == 0 && _ship.PosY == 28)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 28 } };
            }
            if (_ship.PosX == 39 && _ship.PosY == 28)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 20, TargetY = 36 } };
            }
            //-------------------------------------------------------Vissza-----------------------------------------------------//

            if (_ship.PosX == 20 && _ship.PosY == 36)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 3, TargetY = 31 } };
            }
            if (_ship.PosX == 3 && _ship.PosY == 31)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 36, TargetY = 32 } };
            }
            if (_ship.PosX == 36 && _ship.PosY == 32)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 36, TargetY = 24 } };
            }
            if (_ship.PosX == 36 && _ship.PosY == 24)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 5, TargetY = 24 } };
            }
            if (_ship.PosX == 5 && _ship.PosY == 24)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 5, TargetY = 19 } };
            }
            if (_ship.PosX == 5 && _ship.PosY == 19)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 36, TargetY = 19 } };
            }
            if (_ship.PosX == 36 && _ship.PosY == 19)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 36, TargetY = 13 } };
            }
            if (_ship.PosX == 36 && _ship.PosY == 13)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 5, TargetY = 13 } };
            }
            if (_ship.PosX == 5 && _ship.PosY == 13)
            {
                _first = true;
            }
            
            if (_first)
            {
                _first = false;
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 12 } };
            }
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
            if (_ownPlanet == null)
            {
                _ownPlanet = gameItems.Find(x => x.PlayerName == ClientName); //find base planet
            }
            else
            {
                _ship = gameItems.Find(x => x.PlayerName == ClientName && x.ItemType == "Ship"); //find our (only) unit
            }
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
//            if (_ship.PosX == 19 && _ship.PosY == 0)
//            {
//                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 19, TargetY = 19 } };
//            }
//            if (_ship.PosX == 19 && _ship.PosY == 19)
//            {
//                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 19 } };
//            }
//            if (_ship.PosX == 0 && _ship.PosY == 19)
//            {
//                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 0 } };
//            }
//            if(_ship.PosX == 0 && _ship.PosY == 0)
//            {
//                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 19, TargetY = 0 } };
//            }
//            if (_first)
//            {
//                _first = false;
//                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 0 } };
//            }
//            return new List<BattleCommand> { new CmdNop() };
//        }
