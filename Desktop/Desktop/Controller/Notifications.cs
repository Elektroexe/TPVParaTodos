using Desktop.UserControls;
using Desktop.View;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Transitions;

namespace Desktop.Controller
{

    public struct Notification
    {
        public int remainingTime;
        public PictureBox activeNotification;
    }
    public class Notifications
    {
        const string URI = "http://tpvpt.azurewebsites.net/signalr";
        //private const string URI = "http://172.16.100.19/TPVParaTodos/signalr";

        private HubConnection WebServiceConnection { get; set; }
        private IHubProxy HubProxy { get; set; }

        private const int DEFAULT_TIME = 5;
        public List<Notification> activeNotifications;
        private Form activeForm;

        public Notifications(Form form)
        {
            activeForm = form;
        }

        public PictureBox addNotificationToList(NotificationsV2UC notification)
        {
            if (activeNotifications == null)
            {
                activeNotifications = new List<Notification>();
            }
            if (activeNotifications.Count > 0)
            {
                displaceNotifications();
            }

            Bitmap bmp = new Bitmap(notification.Width, notification.Height);
            notification.DrawToBitmap(bmp, notification.ClientRectangle);

            PictureBox pb = new PictureBox();
            pb.Location = new Point(-notification.Width, 620);
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.Image = bmp;

            Notification n = new Notification();
            n.activeNotification = pb;
            n.remainingTime = DEFAULT_TIME;

            activeNotifications.Add(n);

            fadeIn(n);


            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += (s, e) =>
            {
                n.remainingTime--;
                if (activeForm == null)
                {
                    t.Stop();
                    t.Dispose();
                    activeNotifications.Remove(n);
                    t.Enabled = false;
                }
                else if (n.remainingTime <= 0)
                {
                    t.Stop();
                    t.Dispose();
                    deleteNotificationFromList(n);
                    t.Enabled = false;
                }
            };

            t.Enabled = true;

            //t.Start();

            return pb;
        }

        public void deleteNotificationFromList(Notification n)
        {
            (activeForm as Form).Invoke(new MethodInvoker(delegate ()
            {
                fadeOut(n);
            }));
        }

        private void fadeOut(Notification n)
        {
            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(n.activeNotification, "Left", -n.activeNotification.Width);

            t.run();
            t.TransitionCompletedEvent += (s, e) =>
            {
                if (activeForm != null)
                {
                    (activeForm as Form).Invoke(new MethodInvoker(delegate ()
                    {
                        (activeForm as Form).Controls.Remove(n.activeNotification);
                        activeNotifications.RemoveAt(0);
                    }));
                }
            };
        }

        private void fadeIn(Notification n)
        {
            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(n.activeNotification, "Left", 30);
            t.run();
        }


        public void displaceNotifications()
        {
            PictureBox pAux = activeNotifications.First().activeNotification;
            (activeForm as Form).Invoke(new MethodInvoker(delegate ()
            {
                pAux.Location = new Point(pAux.Location.X, pAux.Location.Y - pAux.Height - 20);
            }));

            int initialPos = pAux.Location.Y;

            foreach (Notification n in activeNotifications.Where(n => !n.Equals(activeNotifications.First())))
            {
                PictureBox pb = n.activeNotification;
                initialPos = initialPos + pAux.Height + 20;
                (activeForm as Form).Invoke(new MethodInvoker(delegate ()
                {
                    pb.Location = new Point(pb.Location.X, initialPos);
                }));
            }
        }

        public async void startListeningNotifications()
        {
            WebServiceConnection = new HubConnection(URI);
            HubProxy = WebServiceConnection.CreateHubProxy("notificationHub");
            HubProxy.On<string, string, int>("notify", (title, subtitle, id) => newNotification(id, title, subtitle));
            try
            {
                await WebServiceConnection.Start();

            }
            catch (HttpRequestException)
            {
                //StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                return;
            }
        }

        private void newNotification(int id, string title, string subtitle)
        {
            if (activeForm != null)
            {
                NotificationsV2UC not = new NotificationsV2UC(title, subtitle, (id == 0));
                PictureBox pb = addNotificationToList(not);
                activeForm.Invoke(new MethodInvoker(delegate ()
                {
                    pb.Parent = activeForm;
                    activeForm.Controls.Add(pb);
                    pb.BringToFront();
                }));
            }
            else
            {
                WebServiceConnection.Dispose();
            }


        }
        
        public void finish()
        {
            if (activeNotifications != null)
            {
                activeNotifications.Clear();
            }
            activeForm = null;
        }
    }
}
