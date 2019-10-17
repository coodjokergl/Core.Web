using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.CustomControl
{
    public class RoomAppService :IService
    {
        public string GetRoom(string roomName, string roomNo)
        {
            return $@"房间名称：{roomName}；房号：{roomNo}";
        }
    }
}
