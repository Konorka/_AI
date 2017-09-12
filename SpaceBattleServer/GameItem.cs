// http://www.kongregate.com/games/badben/nano-war
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Common
{
    public abstract class GameItem
    {
        public const float SHOOTDISTANCE = 2f;
        public const float SPEED = 1f;

        static int ItemIdGenerator = 0;
        static object lockObj = new object();

        public int ItemId { get; set; }
        public Player ItemPlayer { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public int NumberOfUnits
        {
            get { return numberOfUnits; }
            set { numberOfUnits = value; }
        }
        protected int numberOfUnits;

        public override string ToString()
        {
            return string.Format("What={0};ItemId={1};ItemPlayer={2};PosX={3};PosY={4};Units={5}", GetType().ToString(), ItemId, ItemPlayer, PosX.ToString("F"), PosY.ToString("F"), numberOfUnits);
        }

        public void ChangeNumberOfUnits(int diff)
        {
            Interlocked.Add(ref numberOfUnits, diff);
        }

        public GameItem(Player newPlayer, float newX, float newY, int newNum)
        {
            lock (lockObj)
            {
                ItemIdGenerator++;
                ItemId = ItemIdGenerator;
            }
            ItemPlayer = newPlayer;
            PosX = newX; PosY = newY;
            numberOfUnits = newNum;
        }
        public abstract void OneTick();

        public GameItem Split(int newNum)
        {
            if (numberOfUnits > newNum)
            {
                ChangeNumberOfUnits(-newNum);
                //NumberOfUnits -= newNum;
                return new Ship(ItemPlayer, PosX, PosY, newNum);
            }
            return null;
        }

        public static bool CanShoot(GameItem attacker, GameItem target)
        {
            float dx = attacker.PosX - target.PosX;
            float dy = attacker.PosY - target.PosY;
            return Math.Sqrt(dx * dx + dy * dy) <= SHOOTDISTANCE;
        }

        public GameItemDescriptor GetDesc()
        {
            GameItemDescriptor ret = new GameItemDescriptor();
            string tmp = this.GetType().ToString();
            ret.ItemType = tmp.Substring(tmp.LastIndexOf(".") + 1);

            ret.ItemId = this.ItemId;
            ret.NumberOfUnits = this.numberOfUnits;
            ret.PlayerName = this.ItemPlayer == null ? "" : this.ItemPlayer.PlayerClient.ClientName;
            ret.PosX = this.PosX;
            ret.PosY = this.PosY;
            if (this is Ship)
            {
                ret.DestinationX = (this as Ship).DestinationX;
                ret.DestinationY = (this as Ship).DestinationY;
            }
            if (this is Planet)
            {
                ret.IncTime = (this as Planet).IncTime;
            }
            return ret;
        }
    }

    public class Planet : GameItem
    {
        DateTime lastInc;



        public int FutureOwnerId { get; set; }
        public float IncTime { get; set; }
        
        
        public Planet(Player newPlayer, float newX, float newY, int newNum, int newInc, int newOwner, int MoveInterval)
            : base(newPlayer, newX, newY, newNum)
        {
            FutureOwnerId = newOwner;
            IncTime = newInc;
            IncTime /= (float)500 / MoveInterval;
            lastInc = DateTime.Now;
        }


       
        public override void OneTick()
        {
            if (ItemPlayer != null)
            {
                if ((DateTime.Now - lastInc).TotalSeconds >
                    IncTime &&
                    NumberOfUnits < 99)
                {
                    ChangeNumberOfUnits(1);
                    lastInc = DateTime.Now;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString() + ";IncTime=" + IncTime;
        }
    }

    public class Ship : GameItem
    {
        public float DestinationX { get; set; }
        public float DestinationY { get; set; }

        public Ship(Player newPlayer, float newX, float newY, int newNum)
            : base(newPlayer, newX, newY, newNum)
        {
            DestinationX = -1; DestinationY = -1;
        }

        public void Move(float newX, float newY)
        {
            DestinationX = newX; DestinationY = newY;
        }

        public override void OneTick()
        {
            if (DestinationX < 0 || DestinationY < 0) return;
            float vecX = DestinationX - PosX;
            float vecY = DestinationY - PosY;
            float rat = SPEED / (float)Math.Sqrt(vecX * vecX + vecY * vecY);
            if (rat < 1)
            {
                PosX += vecX * rat;
                PosY += vecY * rat;
            }
            else
            {
                PosX += vecX;
                PosY += vecY;
                DestinationX = -1; DestinationY = -1;
            }
        }
    }


}
