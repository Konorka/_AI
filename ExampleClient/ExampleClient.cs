using System.Collections.Generic;
using System.Drawing;
using SpaceBattle.Common;
using System;

namespace GeriClient
{
    public class ExampleClient : IBattleClient
    {
        public string ClientName
        {
            get { return "ExampleAI"; }
        }
        public Brush ClientBrush
        {
            get { return Brushes.YellowGreen; }
        }

        GameItemDescriptor _ownPlanet;
        GameItemDescriptor _unit;
        readonly static Random _rand = new Random();

        public List<BattleCommand> GetCommandsFromClient()
        {
            if (_ownPlanet == null)
                return new List<BattleCommand>();
            if (_unit == null)
                return new List<BattleCommand> { new CmdSplit { ItemId = _ownPlanet.ItemId, NumberOfUnits = 1 } };
            //we have a unit to move with

           
            if(_unit.PosX != 0 || _unit.PosY != 0)
                return new List<BattleCommand> { new CmdMove { ItemId = _unit.ItemId, TargetX = 0, TargetY = 0 } };
            return new List<BattleCommand> { new CmdMove { ItemId = _unit.ItemId, TargetX = 30, TargetY = 30 } };
        }
        public void GiveGameItemsToClient(List<GameItemDescriptor> gameItems)
        {
            if(_ownPlanet == null)
            {
                _ownPlanet = gameItems.Find(x => x.PlayerName == ClientName); //find base planet
            }
            else
            {
                _unit = gameItems.Find(x => x.PlayerName == ClientName && x.ItemType == "Ship"); //find our (only) unit
            }
        }
        public void GiveMapSizeToClient(int sizeX, int sizeY)
        {
            
        }
        public void GiveRemainingTimeToClient(int seconds)
        {
            
        }
        public void GiveMessageToClient(string msg)
        {
            
        }
    }
}
