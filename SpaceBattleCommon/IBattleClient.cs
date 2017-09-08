using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace SpaceBattle.Common
{
    /// <summary>
    /// This is the interface every client should implement!
    /// 
    /// To develop a class:
    /// 1) create a new project in this solution, a class library
    /// In the new class library
    /// 1) add reference to the SpaceBattleCommon project
    /// 2) add reference to System.Drawing
    /// In the new class library's CS file
    /// 1) using SpaceBattle.Common; using System.Drawing;
    /// 2) public class MyClient : IBattleClient { } (please use a unique class name)
    /// 3) right click on the interface name, implement interface
    /// 4) implement the necessary methods
    /// In the SpaceBattleServer
    /// 1) add reference to the new class library
    /// 2) execute (using F5), turn on the toolbox (via the checkbox)
    /// 3) the new class library should show up in the listbox. It should be selected
    /// 4) click start
    /// 5) to quit the game, press ALT+F4
    /// </summary>
    public interface IBattleClient
    {
        /// <summary>
        /// Returns the name for the client . Should be unique!
        /// </summary>
        string ClientName { get; }

        /// <summary>
        /// Returns the color for the client . Should be unique!
        /// </summary>
        Brush ClientBrush { get; }

        /// <summary>
        /// This method is called if the server asks the client for commands
        /// This is called often
        /// </summary>
        /// <returns>The list of commands to be executed.</returns>
        List<BattleCommand> GetCommandsFromClient();

        /// <summary>
        /// This method is called if the server tells the client the gameItems it sees
        /// This is called often
        /// </summary>
        /// <param name="gameItems">The list of gameItems the client currently sees</param>
        void GiveGameItemsToClient(List<GameItemDescriptor> gameItems);

        /// <summary>
        /// This method is called ONCE to tell the client the size of the current map
        /// Use this method to initialize when a new map starts
        /// (you can't expect the constructor to be called again)
        /// </summary>
        /// <param name="sizeX">X size</param>
        /// <param name="sizeY">Y size</param>
        void GiveMapSizeToClient(int sizeX, int sizeY);

        /// <summary>
        /// This method is called if the server tells the client the remaining time
        /// This is called often
        /// </summary>
        /// <param name="seconds">Remaining time, in seconds</param>
        void GiveRemainingTimeToClient(int seconds);

        /// <summary>
        /// This method is called if the server is sending a textual message.
        /// Possible messages:
        /// "GAME OVER"
        /// "REMAINING TIME: " + currentTime
        /// "LOGIN FORBIDDEN: " + newPlayer.PlayerClient.ClientName
        /// "GAME START"
        /// </summary>
        /// <param name="msg">The Message</param>
        void GiveMessageToClient(string msg);

    }
}
