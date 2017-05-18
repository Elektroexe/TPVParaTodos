using Desktop.UserControls;
using Desktop.View;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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
        const string URI = "http://172.16.10.20/TPVParaTodos/signalr";
        //const string URI = "http://172.16.100.19/TPVParaTodos/signalr";

        private static HubConnection WebServiceConnection { get; set; }
        private static IHubProxy HubProxy { get; set; }

        private const int DEFAULT_TIME = 5;
        public static List<Notification> activeNotifications;

        public static PictureBox addNotificationToList(NotificationsV2UC notification)
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
            //t.Tick += (s, e) =>
            //{
            //    n.remainingTime--;
            //    MessageBox.Show("hola" + n.remainingTime);
            //    if (n.remainingTime <= 0)
            //    {
            //        t.Stop();
            //        t.Dispose();
            //        deleteNotificationFromList(n);
            //    }
            //};
            t.Elapsed += (s, e) =>
            {
                n.remainingTime--;
                if (n.remainingTime <= 0)
                {
                    //MessageBox.Show("hola" + n.remainingTime);
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

        public static void deleteNotificationFromList(Notification n)
        {
            (n.activeNotification.Parent as Form).Invoke(new MethodInvoker(delegate ()
            {
                fadeOut(n);
            }));
        }

        private static void fadeOut(Notification n)
        {
            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(n.activeNotification, "Left", -n.activeNotification.Width);

            t.run();
            t.TransitionCompletedEvent += (s, e) =>
            {
                (n.activeNotification.Parent as Form).Invoke(new MethodInvoker(delegate ()
                {
                    (n.activeNotification.Parent as Form).Controls.Remove(n.activeNotification);
                    activeNotifications.RemoveAt(0);
                }));
            };
        }

        private static void fadeIn(Notification n)
        {
            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(n.activeNotification, "Left", 30);
            t.run();
        }


        public static void displaceNotifications()
        {
            PictureBox pAux = activeNotifications.First().activeNotification;
            (pAux.Parent as Form).Invoke(new MethodInvoker(delegate ()
            {
                pAux.Location = new Point(pAux.Location.X, pAux.Location.Y - pAux.Height - 20);
            }));

            int initialPos = pAux.Location.Y;

            foreach (Notification n in activeNotifications.Where(n => !n.Equals(activeNotifications.First())))
            {
                PictureBox pb = n.activeNotification;
                initialPos = initialPos + pAux.Height + 20;
                (pb.Parent as Form).Invoke(new MethodInvoker(delegate ()
                {
                    pb.Location = new Point(pb.Location.X, initialPos);
                }));
            }
        }

        public async static void startListeningNotifications()
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

        private static void newNotification(int id, string title, string subtitle)
        {
            Form f = Application.OpenForms.Cast<Form>().Where(x => !x.GetType().Equals(typeof(FormOpacity)) && !x.GetType().Equals(typeof(Form))).Last();
            NotificationsV2UC not = new NotificationsV2UC(title, subtitle);
            PictureBox pb = Notifications.addNotificationToList(not);
            f.Invoke(new MethodInvoker(delegate ()
            {
                pb.Parent = f;
                f.Controls.Add(pb);
                pb.BringToFront();
            }));


        }
    }
}
