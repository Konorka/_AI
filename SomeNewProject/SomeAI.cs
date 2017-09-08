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
        public string ClientName => throw new NotImplementedException();

        public Brush ClientBrush => throw new NotImplementedException();

        public List<BattleCommand> GetCommandsFromClient()
        {
            throw new NotImplementedException();
        }

        public void GiveGameItemsToClient(List<GameItemDescriptor> gameItems)
        {
            throw new NotImplementedException();
        }

        public void GiveMapSizeToClient(int sizeX, int sizeY)
        {
            throw new NotImplementedException();
        }

        public void GiveMessageToClient(string msg)
        {
            throw new NotImplementedException();
        }

        public void GiveRemainingTimeToClient(int seconds)
        {
            throw new NotImplementedException();
        }
    }
}
