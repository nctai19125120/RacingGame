using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocketProgramming.Assets.Scripts
{
    public class ResponseHandler
    {
        private string responseCode;
        public ResponseHandler(string response){
            string[] array = response.Text.Split(new char[]{' '},2);
            switch(words[0]){
                case "INFO":

                break;
                case "STARTGAME":
                    break;
                case "QUESTION":
                    break;
                case "RESULT":
                    break;
                case "RANKING":
                    break;
                case "ELIMINATE":
                    break;
                case "END":
                    break;
            }
        }

    }
}