using SpaceBattle.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeNewProject
{

    public class Scout
    {
        int id { get; set; }
        float tX { get; set; }
        float tY { get; set; }



        public Scout(GameItemDescriptor scout)
        {
            id = scout.ItemId;
            tX = scout.PosX;
            tY = scout.PosY;
        }

    }
}

