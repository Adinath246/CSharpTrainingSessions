using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeartRateVitalManagementUsingObserver
{
    public class HeartRateSensor
    {
        public event Action<int> OnHeartRateValueUpdated;

        public void StartMonitoringHeartRate()
        {
            while (true)
            {
                if (OnHeartRateValueUpdated != null)
                {
                    OnHeartRateValueUpdated.Invoke(new Random().Next(200));
                    Thread.Sleep(3000);
                }
            }
        }
    }

    /// <summary>
    /// Subject
    /// </summary>
    public class HeartRateVitalManagementApp
    {
        public event Action<int> OnHeartRateValueUpdated;
        public event Action OnVitalCritical;
        private HeartRateSensor _heartRateSensor;
        int _lowHeartRate = 80, _highHeartRate = 160;

        public HeartRateVitalManagementApp()
        {
            _heartRateSensor = new HeartRateSensor();
            _heartRateSensor.OnHeartRateValueUpdated += NotifyObserverWhenHeartRateUpdated;
        }

        public void StartMonitoringHeartRate()
        {
            _heartRateSensor.StartMonitoringHeartRate();
        }

        public void NotifyObserverWhenHeartRateUpdated(int newHeartRateValue)
        {
            if (OnHeartRateValueUpdated != null)
            {
                OnHeartRateValueUpdated.Invoke(newHeartRateValue);
            }
            if (OnVitalCritical != null && (newHeartRateValue < _lowHeartRate || newHeartRateValue > _highHeartRate))
            {
                OnVitalCritical.Invoke();
            }
        }
    }

    /// <summary>
    /// Observer
    /// </summary>
    public class HeartRateVitalWatch
    {
        HeartRateVitalManagementApp _heartRateVitalManagement;

        public HeartRateVitalWatch()
        {
            _heartRateVitalManagement = new HeartRateVitalManagementApp();
            _heartRateVitalManagement.OnHeartRateValueUpdated += DisplayHeartRate;
            _heartRateVitalManagement.OnVitalCritical += VibrateDevice;
            _heartRateVitalManagement.OnVitalCritical += BeepDevice;
        }

        public void StartMontoringHeartRate()
        {
            _heartRateVitalManagement.StartMonitoringHeartRate();
        }

        public void DisplayHeartRate(int currentHeartRateValue)
        {
            Console.WriteLine($"Heart Rate is {currentHeartRateValue} BPM");
        }

        public void VibrateDevice()
        {
            Console.WriteLine($"Watch is vibrating");
        }

        public void BeepDevice()
        {
            Console.WriteLine($"Watch is beeping");
        }
    }

    internal class Observer
    {
        static void Main()
        {
            HeartRateVitalWatch vitalWatch = new HeartRateVitalWatch();
            vitalWatch.StartMontoringHeartRate();
        }
    }

}