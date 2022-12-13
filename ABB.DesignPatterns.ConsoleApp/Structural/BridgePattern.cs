using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace ABB.DesignPatterns.ConsoleApp.Structural
{
    namespace BridgeOriginal
    {
        //Original Application which sends Email and SMS msgs using WebService 
        public interface IWebServiceNotifier
        {
            void Send(string message); 
        }
        //An implementation of the interface with details for sending Emails via a web service. 
        public class SendEmailWS : IWebServiceNotifier
        {
            public void Send(string message) => WriteLine($"EMAIL Web Service sent with \"{message}\" payload.");
        }
        //An implementation of the interface with details for sending SMS via a web service. 
        public class SendSMSWS : IWebServiceNotifier
        {
            public void Send(string message) => WriteLine($"SMS Web Service sent with \"{message}\" payload.");
        }
        public interface IWCFServiceNotifier
        {
            void Send(string message, string subject, string otherParameters);
        }
        //An implementation of the interface with details for sending Emails via a web service. 
        public class SendEmailWCF : IWCFServiceNotifier
        {
            public void Send(string message, string subject, string otherParameters) => 
                WriteLine($"EMAIL WCF Service Message:{message}, Subject: {subject}, Extended Props:{otherParameters}");
        }
        //An implementation of the interface with details for sending SMS via a web service. 
        public class SendSMSWCF : IWCFServiceNotifier
        {
            public void Send(string message, string subject, string otherParameters) => 
                WriteLine($"SMS WCF Service Message:{message}, Subject: {subject}, Extended Props:{otherParameters}");
        }
        public interface ITPServiceNotifier
        {
            void Send(object parameters);
        }
        //An implementation of the interface with details for sending Emails via a web service. 
        public class SendEmailTP : ITPServiceNotifier
        {
            public void Send(object parameters) =>
                WriteLine($"EMAIL TP Service Message:{parameters.ToString()}");
        }
        //An implementation of the interface with details for sending SMS via a web service. 
        public class SendSMSWTP : ITPServiceNotifier
        {
            public void Send(object parameters) =>
                WriteLine($"SMS WCF Service Message:{parameters.ToString()}");
        }
    }
    namespace BridgeNew
    {
        public interface IIBridgeNotifierService
        {
            void Send(string message); 
        }
        public class WebServiceAPI : IIBridgeNotifierService
        {
            public void Send(string message)
                => WriteLine($"{message} Sent using {nameof(WebServiceAPI)} service.");
        }
        public class WCFSerivceAPI : IIBridgeNotifierService
        {
            public void Send(string message)
                => WriteLine($"{message} Sent using {nameof(WCFSerivceAPI)} service.");
        }
        public class TPserviceAPI : IIBridgeNotifierService
        {
            public void Send(string message)
                => WriteLine($"{message} Sent using {nameof(TPserviceAPI)} service.");
        }

        public abstract class MessageComposer
        {
            public IIBridgeNotifierService MailerService;
            public abstract void Send();
        }
        public class EmailComposer : MessageComposer
        {
            public override void Send()
            {
                //Some Message Composing Instructions.... 
                MailerService.Send("Constructed Email Message Object");
            }
        }
        public class SMSComposer : MessageComposer
        {
            public override void Send()
            {
                //Some Message Composing Instructions.... 
                MailerService.Send("Constructed SMS Message Object");
            }
        }
        public class SlackMessageComposer : MessageComposer
        {
            public override void Send()
            {
                //Some Message Composing Instructions.... 
                MailerService.Send("Constructed Slack Message Object");
            }
        }

    }

    class BridgePattern
    {
        static void TestTraditional()
        {
            BridgeOriginal.IWebServiceNotifier notifier = new BridgeOriginal.SendEmailWS();
            notifier.Send("Email from:anon, To:anon, Subject: subject");
            notifier = new BridgeOriginal.SendSMSWS();
            notifier.Send("SMS: 9000090000, Subject: Hi");

        }
        static void TestBridgePatterns()
        {
            BridgeNew.IIBridgeNotifierService service = new BridgeNew.WebServiceAPI();
            BridgeNew.MessageComposer composer = new BridgeNew.SMSComposer();
            composer.MailerService = service;
            composer.Send();
            composer = new BridgeNew.EmailComposer();
            composer.MailerService = new BridgeNew.WCFSerivceAPI();
            composer.Send();
            composer.MailerService = new BridgeNew.TPserviceAPI();
            composer.Send();
        }
        internal static void Test()
        {
            TestTraditional();
            TestBridgePatterns();

        }
    }
}
