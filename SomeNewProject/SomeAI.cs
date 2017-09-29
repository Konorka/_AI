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
            int help = rng.Next(0, 4);


            //if (_ship.PosX != 0 && _ship.PosY != 0)
            //{
            //    help+=10;
            //    return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0+help, TargetY = 2+help } };
            //}
            //return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 2 } };


            switch (help)
            {
                case 0:
                    return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 0 } };
                case 1:
                    return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 39 } };
                case 2:
                    return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 39 } };
                case 3:
                    return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 0 } };
            }

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
