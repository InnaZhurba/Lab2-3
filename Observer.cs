using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
    class ObserverMafia:IObserver
    {
        public void Update(ISubject subject)
        {
            if((subject as Subject).State==1)
            {
                Console.WriteLine("ObserverOne: Subscribers reacted - MAFIA won...");
            }
            else if ((subject as Subject).State == 2)
            {
                Console.WriteLine("ObserverMafia: Subscribers reacted - SIMPLE won...");
            }
        }
    }
    class ObserverMonopoly:IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 1)
            {
                Console.WriteLine("ObserverMonopoly: Subscribers reacted...");
            }
        }
    }
    class ObserverAlias : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 1)
            {
                Console.WriteLine("ObserverAlias: Subscribers reacted...");
            }
        }
    }
    public class Subject : ISubject
    {
        public int State { get; set; }
        public Subject(int number)
        {
            this.State = number;
        }

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important...");
            //this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            //Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }
}
