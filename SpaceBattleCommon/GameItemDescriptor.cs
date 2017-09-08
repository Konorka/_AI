using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Common
{
    /// <summary>
    /// This class is used to describe every gameItem in the game.
    /// A list of these is sent to the client from the server.
    /// </summary>
    public class GameItemDescriptor
    {
        /// <summary>
        /// Type of the item: Planet, Ship
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Nickname of the owner team. Empty string if none
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// X position of the item
        /// </summary>
        public float PosX { get; set; }

        /// <summary>
        /// Y position of the item
        /// </summary>
        public float PosY { get; set; }

        /// <summary>
        /// Number of units in the item
        /// </summary>
        public int NumberOfUnits { get; set; }

        /// <summary>
        /// How many seconds are required to increase the power level - only for planets.
        /// </summary>
        public float IncTime { get; set; }

        /// <summary>
        /// Target X coordinate - only for ships. -1 means STOP
        /// </summary>
        public float DestinationX { get; set; }

        /// <summary>
        /// Target Y coordinate - only for ships. -1 means STOP
        /// </summary>
        public float DestinationY { get; set; }
    }
}
