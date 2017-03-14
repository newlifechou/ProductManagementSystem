﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;

namespace PMSClient
{
    public static class NavigationService
    {
        public static void GoTo(MsgObject obj)
        {
            Messenger.Default.Send<MsgObject>(obj,MainNavigationToken.Navigate);
        }

        public static void ShowStateMessage(string msg = "状态信息")
        {
            Messenger.Default.Send<string>(msg,MainNavigationToken.StateMessage);
        }

        public static void Refresh(VToken refreshtoken)
        {
            Messenger.Default.Send<MsgObject>(null, refreshtoken);
        }

    }
}
