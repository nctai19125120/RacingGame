using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocketProgramming.Assets.Scripts.handlers
{
    public class InfoHandlers:MonoBehaviour
    {
        private string[] nickNames;
        InfoHandlers(string nameList){
            nickNames = nameList.Split(',');
        }
    }
}