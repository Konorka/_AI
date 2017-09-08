using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Common
{
    /// <summary>
    /// Abstract base class for the commands
    /// </summary>
    public abstract class BattleCommand
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int ItemId { get; set; }
    }

    /// <summary>
    /// NOP command: do nothing
    /// </summary>
    public class CmdNop : BattleCommand
    {
    }

    /// <summary>
    /// SPLIT command: split a ship into two, or split a ship off a planet
    /// </summary>
    public class CmdSplit : BattleCommand
    {
        /// <summary>
        /// Number of units to split
        /// </summary>
        public int NumberOfUnits { get; set; }
        
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("SPLIT {0} {1}", ItemId, NumberOfUnits);
        }
    }

    /// <summary>
    /// MOVE command: move a ship around the map
    /// </summary>
    public class CmdMove : BattleCommand
    {
        /// <summary>
        /// Target X coordinate
        /// </summary>
        public float TargetX { get; set; }

        /// <summary>
        /// Target Y coordinate
        /// </summary>
        public float TargetY { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("MOVE {0} {1};{2}", ItemId, TargetX, TargetY);
        }
    }

    /// <summary>
    /// SHOOT command: a ship or planet should shoot another ship or planet.
    /// If the target ship/planet is friendly, then "transfer" units.
    /// If the target ship/planet is neutral or enemy, then actually shoot
    /// </summary>
    public class CmdShoot : CmdSplit
    {
        /// <summary>
        /// Unique identifier of the other ship/planet
        /// </summary>
        public int OtherItemId { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("SHOOT {0}->{1} {2}", ItemId, OtherItemId, NumberOfUnits);
        }
    }
}
