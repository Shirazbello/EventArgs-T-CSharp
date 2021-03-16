using System;
using System.Reflection;

namespace FinalEventsDemo
{
class  Eventer
{
    // Event

    public event EventHandler<CallEventArgs> RaiseEvent;

//EVENT İNVOKER
     protected virtual void onRaiseEvent(CallEventArgs e)
     {
         RaiseEvent?.Invoke(this,e);

     }

     //Publisher

    public void Publish()
    {
        CallEventArgs  args =new CallEventArgs();
        Random rnd = new Random();  
        args.Number= rnd.Next(1, 13);
        Console.WriteLine("I am about to Publish.. You all Subscribers should get ready");
        onRaiseEvent(args);

    }

}

//EventArgs<T> class
public class CallEventArgs:EventArgs
{
 private int number;
 public int Number { get{return number;} set{number=value;} }
 private string name="CallEventArgs Eventer";
 public string Name { get=>name; set=>name=value; }
 
}


    class Program
    {
        static void Main(string[] args)
        {
            Eventer eventer= new Eventer();
            //Subscribing to Events
            eventer.RaiseEvent+=new EventHandler<CallEventArgs>(Subscriber1);
            eventer.RaiseEvent+=new EventHandler<CallEventArgs>(Subscriber2);
            //Anonymous Method Subscriber
            eventer.RaiseEvent+=(s ,e)=>
                {Console.WriteLine($"I am The anonymous Subscriber. I have recieved your signal and your name is {e.Name} and your number is {e.Number}");};
            

            //Events Publisher class
            eventer.Publish();
        }

//Subscriber1
        public static void Subscriber1 (object sender, CallEventArgs e)
        {
            Console.WriteLine($"Iam {MethodBase.GetCurrentMethod().Name}. I have recieved your signal " + e.Number);

        }
//Subscriber2
     public static void Subscriber2(object sender, CallEventArgs e)
     {
         System.Console.WriteLine($"I am {MethodBase.GetCurrentMethod().Name}. I have recieved your signal and your name is {e.Name}");

     }     
    }
}
