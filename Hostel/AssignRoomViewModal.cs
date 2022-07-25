using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hostel
{
    public class AssignRoomViewModal
    {
        public int RoomId { get; set; }
        public int MemberId { get; set; }
    }
}