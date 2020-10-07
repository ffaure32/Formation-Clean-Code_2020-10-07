using System;

namespace SOLID.OpenClosePrinciple
{

    public class Itinerary
    {

        private readonly Trip trip;
        private readonly string label;
        private readonly TimeSpan duration;
        private readonly int cost;

        public Itinerary(Trip trip, string label, TimeSpan duration, int cost)
        {
            this.trip = trip;
            this.label = label;
            this.duration = duration;
            this.cost = cost;
        }

        public TimeSpan Duration => this.duration;

        public int Cost => this.cost;

        public string Label => this.label;


        public static ItineraryBuilder Of(Trip trip)
        {
            return new ItineraryBuilder(trip);
        }

        public class ItineraryBuilder
        {

            private Trip trip;
            private string label;
            private TimeSpan duration;
            private int cost;

            public ItineraryBuilder(Trip trip)
            {
                this.trip = trip;
            }

            public ItineraryBuilder Lasting(TimeSpan duration)
            {
                this.duration = duration;
                return this;
            }

            public ItineraryBuilder Labelled(string label)
            {
                this.label = label;
                return this;
            }

            //TODO use Value Object MonetaryAmount
            public Itinerary Costing(int euros)
            {
                this.cost = euros;
                return new Itinerary(this.trip, this.label, this.duration, this.cost);
            }

        }
    }
}
