using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Teemo.CoolQ.Plugins
{
    public partial class FormSettings : Form
    {
        public Thread RoomTask;
        public Thread LiveTask;
        public ListenConfig ListenConfig;
        public FormSettings(ref Thread _roomTask,ref Thread _liveTask,ref ListenConfig _listenConfig)
        {
            InitializeComponent();
            RoomTask = _roomTask;
            LiveTask = _liveTask;
            ListenConfig = _listenConfig;
            this.Text = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Name + " 参数设置";
            Get();
        }

        public void Get()
        {
            lab_thread_id.Text = "房间线程ID：" + RoomTask.ManagedThreadId;
            lab_thread2_id.Text = "直播线程ID：" + LiveTask.ManagedThreadId;
            lab_thread_status.Text = "房间线程状态：" + RoomTask.IsAlive;
            lab_thread2_status.Text = "直播线程状态：" + LiveTask.IsAlive;
            lab_roommsg_count.Text = "房间信息获取次数：" + ListenConfig.GetRoomMsgCount;
            lab_livemsg_count.Text = "直播信息获取次数：" + ListenConfig.GetLiveCount;

            txt_fanpai.Text = ListenConfig.HitYouText;
            txt_roommsg_delay.Text = ListenConfig.GetRoomMsgDelay.ToString();
            txt_livemsg_delay.Text = ListenConfig.GetLiveDelay.ToString();
        }

        /// <summary>
        /// 退出按钮事件处理方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存按钮事件处理方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int RoomDelay;
            int LiveDelay;
            try
            {
                RoomDelay = int.Parse(txt_roommsg_delay.Text);
                LiveDelay = int.Parse(txt_livemsg_delay.Text);
            }
            catch
            {
                MessageBox.Show("延时设定出错，请确认是否正确！");
                return;
            }
            ListenConfig.GetRoomMsgDelay = RoomDelay;
            ListenConfig.GetLiveDelay = LiveDelay;
            ListenConfig.HitYouText = txt_fanpai.Text;
            this.btnExit_Click(null, null);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            RoomTask.Abort();
            LiveTask.Abort();
        }

        private void btn_reget_Click(object sender, EventArgs e)
        {
            Get();
        }

    }
}
