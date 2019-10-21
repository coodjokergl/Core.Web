using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Web.AOP;
using Core.Web.Core;

namespace Core.Web.CustomControl
{
    public class RoomAppService :IService
    {
        public IAppContext Context => null;

        public string GetRoomCount()
        {
            return new Random(DateTime.Now.Second).Next(1,1000).ToString();
        }

        public string GetRoom(string roomName, string roomNo)
        {
            return $@"房间名称：{roomName}；房号：{roomNo}";
        }
    }
}
