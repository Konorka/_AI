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
        GameItemDescriptor _ownPlanet;
        GameItemDescriptor _ship;

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
            int help = 0;
            if ((_ship.PosX != 0 || _ship.PosY != 0) && help==0)
            {
                help++;
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 0, TargetY = 2 } };    
            }
            if (_ship.PosX ==0 || _ship.PosY==0)
            {
                return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 20, TargetY = 20 } };
            }
            

            return new List<BattleCommand> { new CmdMove { ItemId = _ship.ItemId, TargetX = 39, TargetY = 2 } };
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
