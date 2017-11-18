using SpaceBattle.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeNewProject
{
    public class Search
    {
        List<int> scoutLeft = new List<int>
        {
            20, 2,
            0,  2,
            0,  10,
            35, 10,
            35, 13,
            0,  13,
            0,  22,
            35, 22,
            35, 25,
            0,  25,
            0,  34,
            35, 34,
            35, 37,
            0,  37
        };
        public List<int> GetScoutLeftList()
        {
            return scoutLeft;
        }
        List<int> scouRight = new List<int>
        {
            20,2,
            38,2,
            38,5,
            4,5,
            4,8,
            38,8,
            38,16,
            4,16,
            4,19,
            38,19,
            38,29,
            4,29,
            4,32,
            38,32,
            38,38
        };

        public List<int> GetScoutRightList()
        {
            return scouRight;
        }
    }
}
