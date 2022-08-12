using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAssignment
{
    /// <summary>
    /// Observer
    /// </summary>
    public class Window
    {
        Button _clearButton;
        TextBox _searchTextBox;

        public Window()
        {
            _clearButton = new Button();
            _searchTextBox = new TextBox();
            _clearButton.AddObserver(new Action(ClearButton_Click));
        }

        public void Show()
        {
            Console.WriteLine("Window Painted");
        }

        private void ClearButton_Click()
        {
            _searchTextBox.Clear();
        }

        public void ButtonClickSimulation()
        {
            _clearButton.OnClick();
        }
    }

    /// <summary>
    /// Subject
    /// </summary>
    public class Button
    {
        Action Observer;

        public void OnClick()
        {
            Console.WriteLine("Button clicked");
            this.NotifyObserver();
        }

        public void AddObserver(Action observer)
        {
            Observer += observer;
        }

        public void RemoveObserver(Action observer)
        {
            Observer -= observer;
        }

        private void NotifyObserver()
        {
            if (Observer != null)
            {
                Observer.Invoke();
            }
        }
    }

    public class TextBox
    {
        public void Clear()
        {
            Console.WriteLine("TextBox Content Cleared");
        }
    }

    internal class Observer
    {
        static void Main()
        {
            Window _window = new Window();
            _window.Show();
            while (true)
            {
                _window.ButtonClickSimulation();
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

}