using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpaceBattle.Common
{
    public class Player
    {
        public int ExecNumber { get; set; }
        public long ExecMs { get; set; }
        public int ExecCmds { get; set; }
        public bool ExecRunning { get; set; }
        public Stopwatch ExecSTW { get; set; }

        public IBattleClient PlayerClient { get; set; }
        public int PlayerNumber { get; set; }
        public int FinalShoots { get; set; }
        public int FinalEndUnits { get; set; }
        public int FinalEndPlanets { get; set; }
        public int FinalPlanetsCaptured { get; set; }
        public int FinalPlanetsLost { get; set; }
        public int FinalPoints
        {
            get
            {
                return (int)(0.5 * FinalShoots + 200 * FinalEndPlanets + 1 * FinalEndUnits + 100 * FinalPlanetsCaptured - 150 * FinalPlanetsLost);
            }
        }
        public static string PointPrefix_linebreaks
        {
            get
            {
                return "SHOOTS\nPLANETS\nUNITS\nPLANETSCAPTURED\nPLANETSLOST\nTOTAL";
            }
        }
        public static string PointPrefix
        {
            get
            {
                return "NICK\tSHOOTS\tENDPLANETS\tENDUNITS\tPLANETSCAPTURED\tPLANETSLOST\tTOTAL\r\n";
            }
        }
        public string PointStr
        {
            get
            {
                return PlayerClient.ClientName.PadRight(8, '_') + "\t" + FinalShoots + "\t" + FinalEndPlanets + "\t" + FinalEndUnits + "\t" + FinalPlanetsCaptured + "\t" + FinalPlanetsLost + "\t" + FinalPoints + "\r\n";
            }
        }
        
        public byte[,] MapMask { get; set; }
        int MapXSize;
        int MapYSize;

        public Player(IBattleClient BC)
        {
            PlayerClient = BC;
            ExecRunning = false;
            ExecSTW = new Stopwatch();
        }
        public override string ToString()
        {
            return PlayerClient.ClientName + " as #" + PlayerNumber;
        }
        public void InitMap(int sizeX, int sizeY)
        {
            MapXSize = sizeX; MapYSize = sizeY;
            MapMask = new byte[sizeX, sizeY];
            FinalEndPlanets = 0;
            FinalEndUnits = 0;
            FinalPlanetsCaptured = 0;
            FinalPlanetsLost = 0;
            FinalShoots = 0;
        }
        public void ResetMap()
        {
            Array.Clear(MapMask, 0, MapMask.Length);
        }

        private void SetPixel(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MapXSize || y >= MapYSize) return;
            MapMask[x, y] = 1;
        }

        public bool IsVisible(GameItem item)
        {
            return MapMask[(int)item.PosX, (int)item.PosY] == 1;
        }
        public void DoVisible(GameItem item)
        {
            int radius;
            if (item is Planet)
            {
                radius = 4;
            }
            else
            {
                radius = 2;
            }

            int PosX = (int)item.PosX;
            int PosY = (int)item.PosY;
            for (int y = -radius; y < radius; y++)
            {
                int diff = Math.Abs(y);
                for (int x = -radius + diff; x < radius - diff; x++)
                {
                    SetPixel(x + PosX, y + PosY);
                }
            }
        }

    }
}
