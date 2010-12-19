	/*
	* Contents of this file may be used in whatever manner pleases you the most. -Naman Kumar
	*/

using System;
using System.Threading;

namespace com.nk.lib
{

    class TimedEvent
    {
        /*
         * A stand alone class that fires an event at a particular interval
         * 
         * usage:
         *
         * -----instantiate 
         * TimedEvent event = new TimedEvent(int timeDelay, int fireInterval, object parameters)
         * 
         * -----enable firing events 
         * event.eventTrigger += new TimedEvent.fireEvent(methodToBeCalledWhenTimerFired);
         * 
         * public void methodToBeCalledWhenTimerFired(object sender, EventArgs e)
         * {
         *   //Do something 
         *   //Note the method signature...the method and delegate (fireEvent) signature MUST match
         *   //"object sender" can be used to pass any sort of information
         * }
         * 
         * -----stop timer
         * event.disposeTimer();
         * 
         * Version 1.0 by Naman Kumar, August 25,2009
         */

        public delegate void fireEvent(object sender, EventArgs e);
        public event fireEvent eventTrigger;

        Timer eventFiringTimer;

        public TimedEvent(int timeDelay, int fireInterval, object parameters)
        {
            eventFiringTimer = eventTimer(timeDelay, fireInterval, parameters);
        }

        private Timer eventTimer(int timeDelay, int fireInterval, object parameters)
        {
            return new Timer(new TimerCallback(callBack), parameters, timeDelay, fireInterval);
        }

        protected void OneventTrigger(object parameters)
        {
            if (eventTrigger != null)
                eventTrigger(parameters, EventArgs.Empty);
        }

        private void callBack(object parameters)
        {
            OneventTrigger(parameters);
        }

        public void disposeTimer()
        {
            eventFiringTimer.Dispose();
        }
    }
}
